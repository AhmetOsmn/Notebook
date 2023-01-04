# Notes

- RabbitMQ'yu bi posta ofisi olarak düşünebiliriz. Yaptıkları işlemler hemen hemen aynıdır. Sadece kağıt yerine byte array'leri kullanır.

- Normal hayatta posta kutusu olarak adlandırdığımız şey RabbitMQ'da karşımıza `queue` olarak çıkıyor.

    Queue yapısı sadece host yapılan cihazın bellek ve disk limitleri ile sınırlıdır.

- `Producing:` Kuyruğa mesaj gönderme işlemi demektir. Mesajları gönderen programa ise `Producer` deriz.

- `Consuming / Receiving:` Kuyruktaki bir mesajı alma işlemi demektir. Mesajı alan programa ise `Consumer` deriz.

- Producer, Consumer ve Broker'ın aynı host üzerinde olması gerekmez. Çoğu uygulama da bu şekildedir.

- Bir uygulama hem producer hem de consumer olabilir.

- Connection olarak belirtilen şey aslında soket bağlantısı kurma işleminin soyutlaştırılmış halidir.

- RabbitMQ üzerinde bir mesaj yayınlayacaksak bu mesajın formatı `byte array (byte[]?)` olmalıdır.

- Queue'dan okuma işlemini yapabilmek için queue'nun var olduğundan emin olmalıyız.

- Okuma işlemi sırasında alacağımız mesajlar `byte array (byte[]?)` olarak geleceği için onları tekrar `string` tipe çevirmemiz gerekecektir.

# Work Queue

- `Work Queue` olarak adlandırılan kuyruk yapılarının amacı ağır işleri bir anda yapmaktan ve bu işin tamamlanmasını beklemekten kaçınmaktır.

    Örnek olarak 2 adet consumer servis olsun ve 1 adet producer servis olsun. Producer 6 adet mesaj yayınladığında, RabbitMQ bu 2 consumer'ın ortalama olarak aynı sayıda mesaj tüketmesini sağlamaya çalışır. Yani beklenen durum 2 consumer'ın da 3'er adet mesajı okumalarıdır.

# Message acknowledgment

- Eğer bir consumer aldığı task'ı tamamlamadan yok olursa, normal şartlarda üzerinde çalıştığı task ve o consumer'a atanmış olan task'ler silinecektir.

    Yukarıdaki durum istenmeyen bir durumdur. Bir consumer yok olduğunda-öldüğünde, onun task'larını başka bir consumer'ın devam ettirmesini isteriz. Bunu sağlayabilmek için `message acknowledgments` dediğimiz sistem vardır. RabbitMQ bir mesajın kaybolmayacağından emin olmak için, mesajı alan consumer'dan bir yanıt bekler. Gelen yanıta göre task'i siler veya silmez.

- Eğer bir consumer geriye ackn. bilgisi döndürmeden ölürse (kanal kapanabilir, bağlantı kapanabilir vb.) RMQ bu mesajın doğru bir şekilde tam olarak tamamlanamadığını anlar ve mesajı tekrar kuyrağa alır. Tam bu sırada uygun (boşta olan) başka bir consumer varsa bu mesajı o consumer'a gönderir.

    Yukarıdaki mekanizma, consumer'lar öldüğünde bile mesajların kaybolmayacağından emin olmamızı sağlar.

- Bazı durumlarda bazı consumer'lar sıkışabilir (ölmezler) ve mesajları alamıyor durumda olabilir. Böyle durumları tespit edebilmek için message ackn.'larda bir zaman aşımı sınırı vardır. Bu süre default olarak 30 dakikadır. 

    Zaman aşımı meydana geldiğinde bu consumer'ın kanalı kapatılır ve özel bir exception fırlatılır ve bu exception kayıt altına alınır (loglanır). Sonrasında bu consumer üzerindeki bütün mesajlar tekrar kuyruğa taşınır.

    Message ackn. bilgisi otomatik olarka veya manuel olarak verilebilir. 
    
    Message ackn. bilgisi mesaj alınan aynı kanal üzerinden geri gönderilmelidir. Eğer farklı bir kanal üzerinden göndermeye çalışırsak ilgili exception fırlatılacaktır (channel-level protocol exception).

# Message durability

- Aksini belirtmediğimiz sürece, RMQ suncusu çöktüğünde veya kapandığında mesajlarımız ve kuyruklarımız kaybolacaktır. Bunu engellemek için iki yapının da ``durable = true` olarak işaretlendiğinden emin olmalıyız. 

    Ayrıca ekstra olarak mesajı göndermeden önce `channel.CreateBasicProperties()` ile aldığımız property'lerden `Persistend = true` olarak ayarlamalıyız (Bu durumda mesajın kesinlikle kaybolmayacağını garanti edemez. RMQ'ya mesajı diske kaydetmesini söylese bile, bu işlem gerçekleşene kadar bir açıklık vardır).

# Fair Dispatch

- Mesajların her zaman consumer'lara dengeli olarak (iş yükü bakımından) dağıtılacağından emin olamayız. 

    Örnek olarak 1 queue ve 2 consumer olsun. Bu iki consumer eşit sayıda mesajı tüketse bile (örnek olarak ikisi de 10 - 10 mesaj tüketsinler) bir tanesindeki mesajların iş yükü çok fazla olabilir, diğerinin iş yükü çok az olabilir. Böyle bir durumda şöyle bir şey oluşur, bir consumer çok fazla çalışırken diğer consumer çok çok az çalışabilir. Eğer buraya müdahele edilmez ise RMQ mesajalrı bu iki consumer'a eşit şekilde vermeye devam eder ve yoğun olarak çalılan consumer tamamen yük altında kalmış olur.

    Burada RMQ'nun böyle çalışmasının nedeni default çalışma şeklinde, kuyruğa bir mesaj geldiğinde o mesajı direkt olarak consumer'a atamasıdır. Burada atama yaparken hangi consumer kaç adet ackn. bilgisi göndermiş, mesajı ona göre göndereyim kontrolünü yapmaz. 

    Bu durumu şu şekilde değiştirebiliriz:

    `channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);`

    Burada `BasicQos` metodunun `prefetchCount` parametresini `1` olarak set ettiğimizde aslında RMQ'ya şunu söylemiş oluyoruz: Bir consumer işini bitirmeden ona başka bir iş verme. O işi, şuanda meşgul olmayan bir sonraki consumer'a ver.

    Şuna dikkat etmeyi unutmamalıyız. Eğer tüm consumer'lar meşgulse kuyruk dolmaya başlar, hatta dolabilir. Bu süreci güzel yönetmek için farklı stratejiler uygulanabilir. Bunlardan bir tanesi consumer sayısını arttırmaktır.


# Keywords

- `queue`: Oluşturulacak olan queue'nun adını belirtir. Eğer boş bir metin gönderirsek, sunucu kendisi bir isim üretir.

- `durable`: Oluşturulan queue nun, broker restart edildiğinde kurtarılması gerekip gerekmediğini belirtir. 
- `exclusive`: Oluşturulan queue içerisinde bulunduğu bağlantı ile sınırlı mıdır bunu belirtir. Sınırlı olan queue bağlantı kapandığında silinir.
- `autoDelete`: Bu queue'nun son consumer'ı abonelikten çıktığında otomatik olarak silinip silinmemesi gerektiğini belirtir.
- `arguments`: Opsiyonel olarak verilmek istenen argumanlar burada belirtilir.
- `exchange`: Mesajın hangi şekilde alınıp yönlendirilmesini belirtir. Boş bırakılırsa default type kullanılır.
- `routingKey`: Mesajın nereye gideceğini belirten adrestir.
- `basicProperties`: Ekstra parametreleri içerir.
- `body`: Göndermek istediğimiz mesajı alır (byte array olarak) alır.
- `autoAck`: Mesajı okuma durumu.
- `consumer`: Mesajı okuyan consumer bilgisi.
