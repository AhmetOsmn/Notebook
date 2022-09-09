# Kaynaklar

- [Github Nedir?](https://docs.microsoft.com/tr-tr/training/modules/introduction-to-github/2-what-is-github)
- [GitHub'a Giriş](https://github.com/skills/introduction-to-github)

# İçincekiler

- [Git Nedir?](#git-nedir)
- [GitHub Nedir?](#github-nedir)
- [GitHub Üzerinden Neler Yapılabilir?](#github-üzerinden-neler-yapılabilir)
- [GitHub'ı Kullanalım](#githubı-kullanalım)

<br>

# Git Nedir?

- Git bir versiyon kontrol sistemidir. Yani basit olarak açacak olursak: Projenin geliştirilme sürecinde işaretlenen tarihteki haline erişme ihtiyacımız olabilir. Örnek olarak bir proje geliştirdiğimizi düşünelim. Proje zaman ilerledikçe büyüyecek, değişecek ve daha karmaşık hale gelecektir. Bu süreç içerisinde bazı nedenlerden dolayı projenin `x` gün önceki haline dönmemiz veya bir kod parçasının `x` gün önceki haline erişmemiz gerekebilir. Git teknolojisi aslında bizim bu ihtiyacımız karşılamak için çok uygun bir teknolojidir. İlerleyen kısımlarda bahsedilecek olan `commit`ler sayesinde, projenin geçmişte `commit` atılmış haline kolayca erişebiliyoruz. Eğer Git'i kullanmıyorsak böyle bir ihtiyacı giderebilmek için projeyi geliştirme aşamasında v1, v2, v3, vb. şekilde klasörlememiz gerekecektir.

- Git kullanımının başka bir güzel avantajı projeyi takım olarak geliştirmeyi kolaylaştırır. Örnek olarak 4 modülden oluşan bir proje geliştirdiğimizi düşünelim. Takımdaki 4 kişi birer modül seçer ve geliştirmeye başlar. Git içerisinde bulunan `branch` yapısı ile oldukça basit bir şekilde ayrı çalışmalar yapılabilir ve en son modüller tamamlandıkça `merge` edilerek proje içerisinde birleştirilirler ve proje tamamlanmış olur. 


- Son olarak ilk başlarda GitHub ile Git'in aynı şey olduğu sanılsa da aslında Git başlı başına bir teknolojidir, GitHub ise bu teknolojiyi kullanıcıların kullanımı için hazırlanan arayüzler ile Git kullanımını kolaylaştırmayı hedefleyen bir servistir. Bunu şu şekilde görebiliriz, GitHub ve GitLab ortak olarak Git teknolojisini kullanır ama kendileri ayrı servislerdir. İkisininde kendilerine göre avantajları vardır ve kullanıcı ihtiyacına göre Git teknolojisini istediği servis üzerinden kullanabilir.

<br>

# GitHub Nedir?

- Yukarıda bahsedildiği gibi GitHub, Git teknolojisini kullanıcının arayüzler ile kullanmasını sağlayan bir bulut servisidir. Projeleri uzak depolarda geliştirmemizi sağlar. 

- Üst kısımda verilen 2 örnek tahmin edileceği üzere GitHub ile rahatça gerçekleştirilebilir. 

- GitHub içerisinde eklediğimiz projeleri gizli veya herkese açık olarak paylaşabiliriz. Bu tamamen kullanıcının isteğine bağlıdır. Eğer kullanıcı geliştirdiği projeyi diğer insanların görmesini istemiyorsa, GitHub servisini sadece versiyon kontrol sistemi olarak istiyorsa projeyi gizli olarak servise ekleyebilir.

    Kullanıcı geliştirdiği projeyi herkese açık olarak ta paylaşabilir. İnsanların kendisinin geliştirdiği projeleri görmesini, bu projelere katkıda bulunulmasını isteyebilir. Bu kısımda Açık Kaynak (Open Source) Proje kavramı giriyor. Açık Kaynak konusu ayrı bir konu olduğu için detaylarına girmiyoruz.

- GitHub üzerinde herkese açık olarak bir proje paylaştığımız zaman, diğer kullanıcılar bu projeyi kendi bilgisayarlarına kolaylıkla indirebilirler.

<br>

# GitHub üzerinden neler yapılabilir?

- `Sorunlar Alanı: ` Projenin kullanıcıları ile projenin geliştiricileri arasındaki iletişim genelde bu alan üzerinden olur. Kullanıcılar projeye eklenmesini istedikleri özellikleri, gördükleri hataları vb. durumları bu alandan projeyi geliştiren kişilere belirtirler. 

- `Fork ve Star: ` GitHub üzerindeki diğer kullanıcıların yaptığı projeleri, paylaştığı notları veya dökümanları kendi takibimize almak istiyorsak, o projeyi alıp geliştirme yapmak istiyorsak *Fork* işlemi yapabiliriz. Fork'ladığımız Repo'lar bizim Repo'larımızın arasında görülecek ama alt kısmında Repo'nun sahibi belirtilecektir.

    Eğer bir Repo'yu beğendiysek *Star* kısmını işaretleyebiliriz. Profilimizden yıldız verdiğimiz Repo'ları tekrar tekrar inceleyecebiliriz.

- `Dal (branch): ` Bu kısım aslında geliştiricileri ilgilendiren kısımdır. Projeyi yukarıda örneklendirdiğimiz şekilde, takım olarak proje geliştirirken dallar üzerinde çalışmak aynı anda geliştirme yaparken kodların çakışmasını önler. Bu çakışma olayı `conflict` olarak adlandırılır. 

    Genelde projelerde sabit,kararlı olan bir dal olur, geliştiriciler tamamladıkları kendi dallarını bu kararlı dal ile birleştirirler. Birleştirme işleminden önce kararlı dala gelen yan dal önce kontrollerden geçer ve onaylandığı zaman kararlı dal ile birleştirilir.

    Dallanmaları kullanılan IDE üzerinden veya GitHub'ın kendi içerisinden takip edip, dallar arasında işlemler yapabiliriz.

- `Commit'ler: `Commit'leri projenin geliştirme sürecinde proje içerisine atılan işaretler olarak düşünebiliriz. Proje üzerinde değişikliker yaptığımızda commit'ler atarak kendizmiz için veya takımımız için projede geriye dönüş noktaları oluşturmuş oluruz. Ayrıca commit sistemi projenin gelişimini daha okunabilir ve daha takip edilebilir hale getirir.

- `Pull Requests: ` Projedeki kararlı dala birleştirilmeye hazır bir yan dal eklenmek isteniyorsa *pull request* oluşturulur. Buradan anlaşılacağı üzere takımdaki herhangi birisinin bitirdiği dalı direkt olarak kararlı dal'a birleştirmesi engellenir (Bunun için **Onay Gerekli** olarak ayarlanmalıdır).

     Kararlı dal'a olabildiğince herhangi bir sorun çıkartmayacağını düşündüğümüz ve testlerden geçmiş, kontrol edilmiş kodların eklenmesini istiyoruz. Bu nedenle yan dallar kararlı dal ile birleştirilmeden önce (genelde takım liderleri tarafından veya projeye hakim bir kişi tarafından) kontrollerden geçer. 

    Yukarıdaki kontrol aşamasında eğer yan dal ile kararlı dal arasında bir çakışma (conflict) oluşursa GitHub zaten uyarı verecektir. Böyle durumlarda çakışmaların olduğu alanlardaki kodlar incelenir ve çakışmayı ortadan kaldıran düzenlemer yapılır ve iki dal birleştirilir (merge edilir).

<br>

# GitHub'ı Kullanalım

- Projelerde GitHub hizmetlerini kullanmanın farklı yolları vardır. Git Bash kullanarak, Git Desktop kullanarak, Visual Studio veya VS Code gibi programları kullanarak, eğer Linux tabanlı bir işletim sisteminiz var ise direkt console üzerinden vb. yollar ile GitHub hizmetlerini kullanabiliriz. Şimdilik `Git Bash` ve `Visual Studio` örneklerini Windows cihaz üzerinden göstereceğiz.

- Git teknolojisini kullanmak istiyorsak ilk önce bilgisayarımıza Git'i kurmamız gerekiyor. Hedefimiz kullanımı gösterebilmek olduğundan kurulumu detaylı anlatmayacağız. Alt kısımdaki link üzerinden kurulumları yapabilirsiniz.

    [Git İndir](https://git-scm.com/downloads)

<br>

## Git Bash

<br>

### GitHub'a Proje Yüklemek

- Öncelikle GitHub hesabımızı bilgisayarımıza indirdiğimiz Git'e tanıtmamız gerekiyor. Bunun için  

        git config --global user.name "userName"

    şeklinde işlem yapmamız gerekiyor. Daha detaylı anlatımı için [buraya](https://docs.github.com/en/get-started/getting-started-with-git/setting-your-username-in-git) bakabilirsiniz.

- Örnek olarak sıfırdan bir projeye başladığımızı düşünelim. Farklı senaryolarda bazı adımlar değişebilir ama genel anlamda benzer süreçlerden geçilir. Öncelikle GitHub'a yüklemek istediğimiz projenin bulunduğu klasöre girmemiz gerekiyor. Bu klasör içinde yapılan değişiklikleri Git'in takip edebilmesi için ve hizmetlerinden faydalanabilmemiz içn bir komut çalıştırmamız gerekiyor.

    Git Bash ile projenin olduğu klasörün içerisine girmek için (eğer kurulumda bu seçeneği seçtiyseniz) klasör içerisinde mouse ile sağ tıklayıp `GitBash Here` seçeneğine basabiliriz. 

    ![00](https://user-images.githubusercontent.com/44196434/189329196-b9466ad7-b25f-480e-bf93-123008d214a9.png)

    Eğer sizde bu seçenek gelmiyorsa temel CLI komutları ile GitBash içerisinde ilgili klasöre girmeniz gerekecektir. İsterseniz Git'in kurulum ayarlarını güncelleyerek bu özelliği aktif edebilirisiniz.

    Açılan Git Bash ekranında çalıştırmamız gereken komut:
    
        git init -b main

    Bu komutu çalıştırdığımızda artık klasör içerisinde gizli klasör olarak tutulan bir `.git` klasörü oluşacaktır. Eğer sizde bu klasör görünmüyor ise *File Explorer* ayarlarından bu özelliği açabilirsiniz. 

    ![01](https://user-images.githubusercontent.com/44196434/189329219-774c98b6-5481-48df-9ec2-c87957615c36.png)

    Artık bu klasör içerisindeki değişiklikler Git tarafından takip edilebilir, Git'in hizmetlerinden faydalanılabilir hale geldi. Git'in projeyi arka planda bu klasörde neler yaptığı ve hizmetleri nasıl sağladığı detaylı bir konu olduğu için o kısmı geçiyoruz.

- Artık Git hizmetlerinden faydalanabildiğimize göre projeye yeni bir dosya eklendiğinde, güncellendiğinde veya silindiğinde nasıl adımlar gerçekleştirilir onları inceleyelim. 

    Örnek olarak projemizin içerisine alt kısımda yeşil alan ile gösterilen gibi bir `txt` dosyasının eklendiğini düşünelim. 

    ![02](https://user-images.githubusercontent.com/44196434/189329371-24c3757c-5349-40dc-8c13-0679d4a4e179.png)

    Proje içerisinde değişiklikler yapıldığında Git üzerinden bu durumları şu komut ile takip edebiliriz:

        git status

    Bu komut, Git içerisinde yer alan **Working Tree** kavramından faydalanarak dosyaların durumları hakkında bizlere rapor verir. Örnek olarak yukarıdaki ekran görüntüsünde *turuncu* olarak işaretlenen alanda bir uyarı veriliyor. Projeye eklediğimiz *example.txt* dosyasını `Untracked file` olarak tanımlıyor ve hemen üstünde yönlendirici bir bilgi mesajı veriyor. 

    Proje içerisine eklediğimiz dosyaların Git'tarafından takibe alınmasını istiyorsak bu dosyaları şu komut ile işaretleriz:

        git add

    Bu komutu farklı şekillderde farklı parametreler ile kullanabiliriz. Örnek olarak projeye eklenen dosyalar içerisinde sadece *example.txt* dosyasının takip edilmesini istiyorsak:

        git add example.txt

    şeklinde kullanabiliriz. Eğer proje içerisindeki bütün dosyaları Git'in takip etmesini istiyorsak yukarıdaki ekran görüntüsünde yapıldığı gibi:

        git add .

    şeklinde kullanabiliriz. Örnek olarak:

   ![03](https://user-images.githubusercontent.com/44196434/189329398-eb8b70a8-3022-42c2-8ea0-724c2a309e59.png)

    Yukarıdaki ekran görüntüsünde olduğu gibi (1) komutundan sonra (2) komutunu çalıştırdığımızda artık farklı bir rapor oluşuyor. Turuncu olarak işaretlenen alanda artık farklir uyarı mesajı var ve görüldüğü gibi dosya ismi kırmızı renkten yeşil renge dönüş durumda, yani artık *example.txt* dosyası takip ediliyor.

- Sonraki adımda projeye eklenen ve Git tarafından takibe alınan dosyaları GitHub üzerindeki uzak depomuza göndermek istersek şu şekilde bir ara komut çalıştırmamız gerekiyor:

        git commit -m "commit mesajı"

    Commit komutu ile artık dosyamızı gönderilebilir olarak işaretledik ve kaydettik. İleriki zamanlardan dosyanın şuan commit'lediğimiz haline erişilebilir bir nokta da oluşturmuş olduk.

    ![04](https://user-images.githubusercontent.com/44196434/189329428-ee41a8c7-c4b9-431f-b08a-771866611fdb.png)

    Genel olarak Git komutlarını çalıştırmadan önce *git status* komutu ile dosyaların durumlarını kontrol etmemiz faydalı olacaktır. Yukarıdaki ekran görüntüsünde görüldüğü gibi (1) komutunu çalıştırdıktan sonra alt kısmındaki turuncu alandan dosyaları kontrol ettik. Sonrasında (2) komutu ile dosyalar üzerinde yaptığımız değişiklikleri işaretledik ve (2)'nin alt kısmındaki turuncu alandan gerçekleştirilen işlemlerin detayları gördük. 

    Commit işleminden sonra tekrar *git status* ile dosyalarının durumlarına bakalım (3). Görüldüğü gibi (3)'ün alt kısmındaki turuncu alan bizlere Git tarafından takip edilen dosyalardan üzerinde değişiklik yapılmış bir dosya olmadığını söyüyor. 
    
    
- Commit işleminden sonra projemizi uzak depomuza göndermek için GitHub üzerinde bir *Repository* oluşturmamız gerekiyor. Bu işlem de Git Bash üzerinden yapılabilir fakat örnekte GitHub üzerinden yeni bir Repo oluşturacağız.

    ![05](https://user-images.githubusercontent.com/44196434/189329449-07f28d3d-a9f0-4cc8-8841-4ab6c2afb64b.png)

    Bu sayfada Repo için bazı ayarlar yapılabilir ve istenirse Repo'ya açıklama eklenebilir. Şimdilik yukarıdaki ekran görüntüsündeki gibi default şekilde Repo'yu oluşturalım.

    Yeni bir Repo oluşturduğumuzda, GitHub bizlere örnek bir GitHub kullanım klavuzu veriyor. İsterseniz o kısmı da inceleyebilirsiniz. 

- GitHub üzerinde bir Repo oluşturduğumuza göre artık projeyi gönderebiliriz. Gönderme işlemi için:

        git push

    Komutunu kullanacağız. *git push* komutuna bazı parametreler vermemiz ve bu komuttan önce bazı işlemler yapmamız gerekecek. Bu işlemler için örnek bir ekran görüntüsü olarak:

    ![06](https://user-images.githubusercontent.com/44196434/189329482-8091e9b3-71ed-4d0d-b6e9-5dd70ba6d269.png)

    İlk önce yukarıdaki ekran görüntüsünde olduğu gibi (1) komutu ile *main* dalına geçiş yapalım. 

        git branch -M main

    Ardından projeyi göndereceğimiz GitHub Repo'sunun adresini Git'e belirtmemiz gerekiyor. Bu işlemi de yukarıda olduğu gibi (2) komutu ile yapabiliriz:

        git remote add origin https://github.com/AhmetOsmn/ExampleRepo.git

    Burada verdiğimiz adres GitHub üzerinde projeyi yüklemek istediğimiz Repo'nun adresi olmalıdır. Artık projeyi göndermeye hazır hale geldik. Yapmamız gereken ekran görüntüsündeki gibi (3) komutunu çalıştırmaktır.
    Gönderme işlemi tamamlandıktan sonra, (3)'ün hemen alt kısımdaki turuncu alan gibi bilgi veren bir alan gelecektir. Eğer projeyi gönderirken bir sorun yaşanırsa buradan görülebilir. Bu alan içerisinde Yapılan işlemler ve proje hakkında bazı detay açıklamalar yer alır.

- Gönderme işlemi tamamlandıktan sonra GitHub üzerinden ilgili Repo içerisine girip değişikliklerin gelip gelmediğine bakabiliriz. 

    ![07](https://user-images.githubusercontent.com/44196434/189329510-45f3dd64-f0cc-48c8-9e08-e44ebbe323ad.png)

    Burada (1) ile gösterilen alan anlaşılacağı üzere, *git push* öncesinde ara işlem olarak söylediğimiz *git commit -m "commit mesajı"* kısmındaki commit mesajıdır.

<br>

### GitHub Üzerinden Proje Çekmek

- GitHub üzerinde paylaşılmış bir projeyi kendi bilgisayarımıza indirmek istiyorsak ve bunu Git Bash ile yapacaksak ilk olarak ilgili Reponun adresini almamız gerekiyor.

    ![00](https://user-images.githubusercontent.com/44196434/189329525-91e2ecf3-96b0-4dd0-9e82-c7b51568bddc.png)

- Daha sonra Git Bash ile projeyi indireceğimiz klasörün içerisine girmemiz gerekiyor.

    ![01](https://user-images.githubusercontent.com/44196434/189329596-81c2bd89-1294-49dc-b626-805e0abdfeee.png)

- Son olarak alt kısımda kırmızı olarak gösterilen alandaki gibi şu komutu çalıştırmalıyız:

        git clone [projenin github adresi]

    ![02](https://user-images.githubusercontent.com/44196434/189329610-0ad7a5f4-2a35-4af9-a0ec-0061649cfabe.png)

    Ekran görüntüsünde yeşil olarak gösterilen alanda görüldüğü üzere indirme işlemi tamamlandıktan sonra proje bilgisayarımıza indirilmiş olacaktır.