# Kaynaklar

- https://learn.microsoft.com/en-us/dotnet/architecture/microservices/
- https://learn.microsoft.com/en-us/training/modules/dotnet-microservices/
 
# Mikroservis Mimarisi

- Bu mimarinin hedefi, büyük yapıları daha küçük modülleri birleştirerek oluşturmaktır.

- Monolithic mimariler bir bütün olarak ölçeklendirilebilirken, mikroservis mimarisinde bütün modüller kendi içerlerinde bağımsız olarak ölçeklendirilebilir olurlar.

    Örnek olarak uygulamada çok fazla talep gelen bir modülün işlem gücünü ve ağ bant genişliğini arttırıp, beklenen boyutta talep gelen modüllerinkileri arttırmayabiliriz.

- Her modül kendi başına çalışabilir haldedir. Ayrıca modüller HTTP/HTTPS, WebSockets, AMQP gibi protokoller üzerinden birbirleri ile iletişim kurabilirler. Burada güzel tasarlanan API'ler de kullanılır.

- Her modül otomatik bir şekilde geliştirilebilmeli ve birbirlerinden bağımsız olarak deploy edilebilmelidir, birbirlerinden ayrı test edilebilmelidir, birbirlerinden ayrı olarka yönetilebilmelidir. 

    Bir modül güncellediğinde bütün uygulamayı rebuild ve redeploy etmek yerine sadece ilgili modül rebuild ve redeploy edilir.

- Her modülün codebase'i farklıdır. Böylece küçük takımlar tarafından yönetilebilirler.

- Her modül kendi verilerinin ve harici durumların sürdürülebilirliğinden sorumludur. Monolithic mimaride bütün modüllerin veritabanı ortak iken, mikroservis mimarisinde (ihtiyacı olan) her modülün kendi veritabanı vardır.

- Bu mimarinin en önemli bir özelliği de farklı diller ile geliştirmeler yapmaya olanak sağlamasıdır. Örnek olarka bazı modüller Java ile bazı modüller C# ile geliştirilebilir. Burada önemli olan bu modüller arasındaki iletişimleri düzgün kurabilmektir. 

<br>

# Mikroservisler ve Docker

- Hazırlanan modülleri mikroservis olarak kullanmamızı sağlayan teknoloji Docker'dır. Docker image ve container'ları sayesinde servisleri ayağa kaldırıp çalışır duruma getiririz.

- Hazırladığımız modülleri dockerize edebilmek için modüllerin içerisinde bir `Dockerfile` olmalıdır. Örnek bir `Dockerfile` dosyası hazırlama aşaması alt kısımdadır:

    <img src="./images/dockerfile01.png" alt="dockerfile" />

    <br>

    1. `mcr.microsoft.com/dotnet/sdk:6.0` image'ini çek ve `build` olarak adlandır.
    2. image içerisindeki çalışma klasörünü `/src` olarak ayarla.
    3. Yerelde bulunan `backend.csproj` dosyasını `/src` içerisine kopyala.
    4. Projede `dotnet restore` komutunu çalıştır.
    5. Yerel çalışma klasöründeki her şeyi image içerisine kopyala.
    6. Projede `dotnet publish` komutunu çalıştır.

    <br>

    <img src="./images/dockerfile02.png" alt="dockerfile" />
    
    <br>

    7. `mcr.microsoft.com/dotnet/aspnet:6.0` image'ini çek.
    8. Image içerisindeki çalışma klasörünü `/app` olarak ayarla.
    9. 80 port'unu aç (expose et).
    10. 443 port'unu aç.
    11. Build image'inin `/app` klasörü içerisindeki her şeyi bu image'in `/app` klasörüne kopyala.
    12. Image'in giriş entrypoint'i olarak `dotnet`'i ayarlar. Bu entrypoint'e bağımsız değişken olarak `backend.dll`'i iletir.

    <br>

    Dockerfile dosyaysının son hali alt kısımdaki gibi olur.
    <br>

    <img src="./images/dockerfile03.png" alt="dockerfile" />

    <br>

    Sonraki adımda Dockerfile dosyası ile aynı dizin içerisinde alt kısımdaki kodu çalıştırırız. Buradaki önemli nokta, proje üzerinde bir değişiklik yaptığımızda bu image'i tekrar build etmemiz gerekecektir.

    <br>

    <img src="./images/dockerfile04.png" alt="dockerfile" />

    <br>

    Build işleminden sonra `docker images` komutu ile bilgisayarımızda bulunan image'leri listeleyebiliriz. Bu liste içerisinde az önce build ettiğimiz image'in de yer aldığını görebiliriz.

    Backend projesini docker image'ı olarak çalıştırmak için yapmamız gereken son şey alt kısımdaki komutu çalıştırmaktır.
    
    <br>

    <img src="./images/dockerfile05.png" alt="dockerfile" />

    <br>

    Artık `http://localhost:5200/pizzainfo` adresi üzerinden projenin ayakta olduğunu test edebiliriz.
    
     Burada görüldüğü üzere Dockerfile dosyası içerisinde expose edilen 80 portunu, dışarıya 5200 portu ile açmak istediğimizi `--p` parametresi ile belirtmiş olduk.

<br>

# Bölüm 1 - Introduction to Containers and Docker (Konteynırlar ve Docker'a Giriş)

- `Containerization` dediğimiz şey bir uygulamanın, bu uygulamanının bağımlılıklarının ve konfigürasyonlarının bir araya toplanıp bir paket haline getirilmesi diyebiliriz.

    Container haline getirilen bir uygulama kendi başına test edilebilir ve kendi başına deploy edilebilir.

    Gerçek hayattan örnek vermek istersek, yük gemilerinin taşıdığı konteynırlara bakabiliriz. Bir yerden bir yere, içerisi değişmeden taşınıyorlar. Yazılımda da benzer mantıkta konteynır hale getirilen bir uygulama farklı ortamlara, içerisi değişmeden taşınır ve çalıştırılır.

- Konteynırlar bizlere uygulamanın yaşam döngüsü boyunca izolasyon, taşınabilirlik, çeviklik, ölçeklenebilirlik ve kolay kontrol edilebilirliği sağlar. En önemli faydası olarak Dev. ve Ops. arasında sağlanan ortam izolasyonudur.

<br>

### Docker Containers vs Virtual Machines

- Docker Container'lar ile Virtual Machine'ler arasındaki farklar için alt kısımdaki görüntüyü inceleyebiliriz:

    ![](images/vmsvsdcs.png)

    - VMs:

        Sanal makinelerin içerisinde uygulamalar, kütüphaneler, binary dosyalar ve konuk işletim sistemi yer alır.

        Eğer tam anlamıyla sanallaştırma yapmak istersek, konteynırlaştırmadan çok daha fazla maliyetli olacaktır.

    - DCs:

        Konteynırlar uygulamayı ve onun bütün bağımlılıklarını içerir.

        İşletim sistemini diğer konteynırlar ile birbirlerinden izole olacak şekilde paylaşırlar.

    Konteynırlar daha az kaynak gerektirdiğinden daha hızlı bir şekilde deploy edilebilirler ve daha hızlı çalıştırılabilirler. Ayrıca aynı donanım ile daha fazla servis çalıştırabiliyor olmamızı da sağlarlar.

<br>

### Docker Terminology (Docker Terminolojisi)

- `Container image`: Container oluşturmak için gereken bütün bağımlılıkları ve bilgileri içeren pakettir. 

    Bazı durumlarda image'lar başka image'lardan oluşabilir.

    Bir kez oluşturulan image üzerinde değişiklik yapılamaz (immutable).

- `Dockerfile`: Bir Docker image'ını build edilebilmesi için gerken bilgileri içeren bir metin dosyasıdır. 

    Bir sürü komutu içeren bir dosya olarak düşünebiliriz.

    İlk satır base image'i belirtir. Sonraki kısımlarda yazılı olan komutlar ile gerekli olan programları yüklemek, dosyaları kopyalamak vb. işlemleri gerçekleştirir.

- `Build`: Image'leri oluşturabilmek için Dockerfile içerisinde yer alan komutların tetiklenmesidir.

    `docker build` şeklinde kullanılabilir.

- `Container`: Bir Docker image'inin örneğidir (instence). Container'lar tek bir uygulamanın, işlemin veya hizmetin yürütülmesini temsil eder.

    Ölçeklendirme yaparken aynı image'ı kullanarak bir çok container oluşturabiliriz (hepsine farklı değerler de gönderilebilir).

- `Volumes`: Container'ların kullanabileceği bir dosya sistemidir. Image'ler read-only'dirler. Container'lar ve volum'leri birlikte kullanarak veri yazabilme yeteneğini de kazandırmış oluruz.

- `Tag`: Farklı image'lerin veya aynı image'in versiyonlarının anlamlı bir şekilde tanımlanması için kullanılan işaret/etiket.

- `Multi-stage Build`: Son image'lerin boyutlarını küçültmeye yarar.
- `Repository (repo)`:Image sürümünü belirten bir etiketle etiketlenmiş ve ilgili (birden fazla olabilir) image'leri içeren koleksiyon.
- `Registry`: İçerisinde repo'lar barındıran bir kayıt defteridir.
- `Multi-arch image`: Çoklu mimari kullanılırken, Docker'ın çalıştığı platforma göre uygun image'leri seçmeyi kolaylaştıran bir özelliktir.
- `Docker Hub`: Image'leri yüklemek ve onları çalıştırabilmek için kullanılan bir registry'dir.
- `Docker Trusted Registry (DTR)`: Şirketlerin kendi ağında, kendi image'lerini yönetmek ve çalıştırmak için kullandığı sistemdir. 
- `Compose`: İçerisinde birden fazla container bulunduran uygulamaları ayağa kaldırmak için çalıştırılması gereken komutları içeren, YAML uzantılı dosyadır. 

    İçerisindeki komutlaru olması gerektiği gibi hazırladığımızda n tane container'dan oluşan bir uygulamayı sadece tek bir satır komut ile ayağa kalırabiliriz. 
    
    `docker-compose up`

    Bu komut Compose dosyasında tanımlanan her image için birer container oluşturur ve onları çalıştırır.

- `Cluster`: Docker ile birlikte kullanılan uygulamaları birden fazla instance'ını oluşturarak ölçeklendirebiliriz bu durumda ortaya **Cluster**'lar çıkmış olur. **Cluster**'ları oluşturmak için **Kubernetes, Azure Service Fabric, Docker Swarm ve Mesosphere DC/OS** kullanılabilir. 

- `Orchestrator`: Image'leri ve container'ları yönetimini basitleştiren bir araçtır. 

<br>

### Docker containers, images and registries

- Geliştirilen bir uygulamanın veya servisin kendisinin ve bağımlılıklarının hepsinin bir arada paketlenmiş haline **image** demiştik. Bu **image** dosyaları statik yapılıdır.

    Dockerize edilimiş bir uygulamayı çalıştırmak için uygulamanın **image**'inden bir instance (örnek) oluşturulur ve bu instanve Docker Host üzerinde çalıştırılır.

    Geliştiriciler **image**'leri **registry**'de tutmalıdır. **Registry**'ler hep **image** kitaplığı gibidir hem de deploy sırasında **production orchestrators** tarafından istenirler.


<br>

# Choosing Between .NET 6 and .NET Framework for Docker Containers

- İki tarafta dockerize edilebilir durumdadır. Kullanıcı ihtiyaçlarına ve bağımlılıklarına göre tercih edebilir.

<br>

### General guidance

- Bu tür ihtiyaçlarınız varsa **.NET 6** kullanabilirsiniz:

    - **Cross-platform** çalışmanız gerekiyorsa. Örnek olarak geliştirdiğiniz proje hem windows'ta hem de linux'ta çalışmalı ise .NET 6 kullanabilirsiniz.
    - Uygulamanızın mimarisi **mikroservis** tabanlı ise kullanabilirsiniz.
    - Container'ları daha hızlı çalıştırmak istiyorsanız, container başına oluşan **footprint**'in daha az olmasını istiyorsanız veya aynı donanım ile daha fazla container çalıştırabilmek istiyorsanız .NET 6 kullanabilirsiniz.

    Özetle eğer dockerize edilmiş bir .NET uygulaması geliştirmek istiyorsanız .NET 6'yı tercih etmeyi kesinlikle düşünmelisiniz.

    Ayrıca .NET 6 da aynı makine içersindeki uygulamalar için yan yana .NET sürümleri çalıştırabiliriz.

- Eğer alt kısımdaki maddeler sizin için geçerliyse **. NET Framework** kullanabilirsiniz:

    - Uygulama halihazırda .NET Framework ile çalışıyorsa ve güçlü windows bağımlılıkları varsa.
    - Uygulama içerisinde bir Windows API kullanmaya ihtiyacınız varsa ve bu API .NET 6 tarafından desteklenmiyorsa.
    - Kullanmanız gereken 3. parti kütüphaneler veya NuGet paketler .NET 6 da kullanılamıyorsa.

<br>

### .NET and Docker image optimizations for development versus production

- Microsoft geliştirme aşaması için ayrı bir **Image** ürünü canlıda çalıştırmak için ayrı **Image** sunmaktadır.

    Bunun amacı 2 işlemin farklı gereksinimlerinin olmasıdır. Bu ayrım sayesinde daha verimli ve hızlı işlem yapabiliyor oluyoruz.

<br>

# Architecting container and microservice-based applications

### Container design principles

- Container tasarlarken `Entrypoint` tanımıyla karşılaşırız.

    Bu şunu yapar, container'ın ömrünü takip eden process'in ömrünü belirtir. Process tamamlandığında container'ın yaşamdöngüsü tamamlanır.

    Eğer process başarısız olursa container sona erer ve orkestratör devralır. Eğer orkestratör hata alındığında başka container'ları çalıştıracak şekilde konfigure edildiyse, başarısız olan container yerine yeni bir container oluşturulur.

    Eğer bir container içerisinde birden fazla process çalıştırmamız gerekiyorsa bunu çözmenin farklı yolları vardır. Unutmamamız gerkene şey bir container'da bir adet entrypoint tanımlayabiliriz.

    Eğer birden fazla process çalıştırmamız gerekiyorsa bu işi bir script tanımlayarak yapmamız gerekecektir. 

<br>

### Containerizing monolithic applications

![](images/monocontainer.png)

<br>

- Bu yaklaşımın dezavantajını proje büyüdüğünde be ölçeklendirilmesi gerektiğinde görürüz.

    Eğer tüm uygulama ölçeklenebiliyorsa, bu gerçekten bir sorun değildir. Genellikle uygulamanın sadece bazı kısımları ölçeklendirmede sorun yaşatabilir, yani sadece bazı kısımlar yüzünden ölçeklendirme yapılmak zorunda kalınabilir. Diğer kısımlar oldukça az kullanılan kısımlardır.

    Örnek olarak bir e-ticaret sitesini düşünelim. Ürün detay modülünü ölçeklendirebilmeliyiz, çünkü bir çok kullanıcı bir ürünü satın almaktansa, ürünün detayını inceler ve çıkar.

    Kullanıcılar sepet modülünü, ödeme modülünden daha fazla kullanır. Sepet modülünün ölçeklendirme gerektirmesi çok daha olasıdır.

    Yukarıdaki gibi bir çok örnek çıkartabiliriz. Burada odaklanmamız gerken nokta şudur: eğer Monolithic bir uygulamamız varsa ve uygulamadaki 10 modülden sadece 2 tanesini ölçeklendirmemiz gerekse bile biz 10 modülü de ölçeklendirmek zorunda kalıyoruz. Çok az kullanılan alanlar için de daha fazla kaynak ayırmış oluyoruz.

    Ayrıca Monolithic uygulamalarda bir bölümde değişiklik yaptığımızda bütün uygulamanın tamamen yeniden test edilmesi gerekecek ve uygulama daha önce nerelerde kullanılıyorsa oralara tekrar son hali gönderilecek.

- Peki bu kadar dezavantajı olduğu halde neden hala yaygın olarak Monolithic uygulamalar kullanılıyor?

    Çünkü uygulamanın geliştirilmesi başlangıçta mikro servislere göre daha kolaydır. Daha kolay geliştirilir.

    ![](images/monoarch.png)

<br>

### Deploying a monolithic application as a container

- Monolithic uygulamaları Docker ile deploy etmenin bazı avantajları vardır. Örnek olarak:

    - Container'ların instance'larını ölçeklendirmek, ilabe VM'lere göre çok çok daha hızlıdır ve kolaydır.
    - Yapılan güncellemeleri de container'lar ile deploy etmek daha hızlıdır ve network tarafı için daha verimlidir.
    - Önceden oluşturduğumuz bir Image'ı yok etmek çok kolaydır (`docker stop`). Bu işlemde saniyler içerisinde gerçekleştirilir.
    - Container'ları ve onların yaşam döngülerini bir orkestratör ile yönetmek te avantaj sağlar.

<br>

# Manage state and data in Docker applications

- Docker uygulamalarında verileri yönetmenin farklı yolları vardır. 

    Docker host ile **Docker Volume** olarak yönetmek:

    - **Volume**'lar host eden bilgisayarın dosya sisteminde, Docker'ın yönetiminde olan bir alanda tutulur.
    - **Bind Mount**'lar (bağlantı parçaları) ana bilgisayarın dosya sistemindeki herhangi bir klasöre eşlenebilir. 

        Docker tarafından kontrol edilemezler, bu nedenle güvenlik riski oluşturabilir.

    - **tmpfs Mount**'ları sadece ana bilgisayarın belleğinde bulunan ve hiçbir zaman dosta sistemine yazlmayan sanal klasörler gibi düşünebiliriz.

    Uzak depolama ile yönetmek:

    - Azure Storage coğrafi olarak dağıtılabilir depolama hizmeti sağlar. Container'lar için uzun vadeli kalıcılık çözümü sağlar.

    <br>

    Docker Container ile yönetmek:

    - **Overlay File System**. Bu Docker özelliği, güncelleştirilmiş bilgileri kabın kök dosya sistemine depolayan bir yazarken kopyala görevini uygular. Bu bilgi, kapsayıcının dayandığı orijinal görüntünün üstündedir.

    <br>

    Kapsayıcı sistemden silinirse, bu değişiklikler kaybolur. Bu nedenle, bir container'ın durumunu kendi yerel deposuna kaydetmek mümkün olsa da, bunun etrafında bir sistem tasarlamak, varsayılan olarak durum bilgisiz olan konteyner tasarımı öncülüyle çelişir.

    Yukarıdaki seçeneklerden **Volume** ile yönetmek önerilen-tercih edilen yöntemdir.


- **Volume**'lar ana işletim sisteminden container'lardaki klasörler ile eşleştirilen klasörlerdir.

    Docker ile çalıştırdığımız bir uygulamada bir kod eğer bir klasöre erişmek isterse aslında ana işletim sistemindeki bir klasöre erişir. 
    
    Bu klasör container'ın kullanım zamanına bağlı değildir. Klasör ana bilgisayardan izole edilmiş bir şekilde Docker tarafından yönetilir. Bu nedenle **volume**'lar contaier'a bağlı olmadan verileri sürdürebilecek şekilde tasarlanmıştır.

    Eğer bir container'ı veya image'ı silersek, volume içerisinde kalıcı olan veriler silinmez.

    Farklı Docker Host üzerinde olan container ve volume arasında veri alışverişi sağlanamaz.Fakat uzak sunucudaki ile sağlanabilir.

    ![](images/volumeandcontainer.png)

    <br>


- **Bind Mount**'lar uzun zamandır kullanılabilir durumdadır. **Volume**'lara göre daha çok limitlendirilmişlerdir ve bazı önemli güvenlik sorunları vardır. Bu nedenle önerilmeyen yöntemdir.

- **tmpfs Mount**'lar ise aslında ana bilgisayarın hafızasında tutulan soyut klasörlerdir ve hiçbir zaman dosya sistemine yazılmazlar.

    Hızlıdırlar ama hem hafızayı tüketirler hem de geçicidirler, sürdürülebilir veri sağlamazlar.

- Container'lar orkestratör tarafından yönetildiğinde, optimizasyondaki bağımlılıklara bağlı olarak farklı Docker Host'lara taşınabilirler.


