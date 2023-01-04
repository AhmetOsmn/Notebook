# Introduction

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

- Bazı keyword'ler:

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