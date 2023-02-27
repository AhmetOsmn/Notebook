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

<br>

# Service-Oriented Architecture (SOA)

- SOA, geliştireceğimiz uygulamayı alt sistemler veya alt katmanlar olarak adlandırılabilecek birden çok hizmete ayrıştırarak geliştirmeyi söyler.

- Docker container'ları olarak deploy edilen servisler ölçeklendirilebilir ve çevik durumda olur. Bu kısımda **Docker Clustering** ve **Docker Orchestrator** yardımcı olur.  

- Mikro servisler SOA'dan türetilmiştir, ama SOA mikro servis mimarisinden farklıdır. 

    SOA'da:

    - Merkezi broker'lar,
    - Kuruluş düzeyinde merkezi orchestrator'lar,
    - Enterprise Service Bus (ESB)'ler tipiktir. 
    
    Bunlar mikro servis mimarisine uygun olmayan yapılardır.

    Fakat sonuca vardığımızda eğer bir uygulamayı mikro servis mimarisi ile nasıl geliştirebileceğimizi biliyorsak, o uygulamayı SOA olarak geliştirmemiz daha kolay kolay olacaktır.

<br>

# Microservices Architecture

- Mikro servislerin boyutlarının ne kadar olacağı çok önemli değildir. 
    
    Amacımız olabildiğince küçük, kendi kendine çalışabilen ve deploy edilebilecek olan paketler çıkartmak olduğundan bunların boyutlarını tabiki elimizden geldiğince küçültmeye çalışacağız ama asıl hedeflememiz gereken nokta servislerin birbirleri ile ve ortam ile olan uyumlarıdır.

- Mikro servisleri tercih etmemizin başka bir nedeni ise, uzun dönemi düşündüğümüzde bize çeviklik sağlıyor olmasıdır.

    Mikro servisler sayesinde otonom yaşam döngüsüne sahip, her biri parçalı olan ve bağımsız olarak konuşlandırılabilen uygulamalar oluşturmamızı sağlar. Bu da karmaşık, büyük ve yüksek düzeyde ölçeklenebilir sistemlerde-uygulamalarda daha iyi bakım sağlar. 

![](images/monovsmicro.png)

<br>

- Mikro servis mimarisi ile geliştirilen bir uygulamada hızlı bir şekilde çalışan modüller görürüz. Yani bütün projeyi tamamlamadan modüller üzerinden projeyi geliştirme aşamalarını müşteriye gösterebiliyor oluruz.

- Mikroservis uygulamalarda başarı sağlamanın bazı noktaları:

    - Servis'lerin ve altyapının izlenmesi ve sağlık kontrollerinin yapılması.
    - Servis'ler için ölçeklenebilir bir altyapı'nın olması (Cloud ve Orchestrator).
    - Birden çok seviyede güvenlik tasarlanması be uygulanması (Oturum yönetimi, yetki yönetimi, güvenli iletişim vb.).
    - Genellikle farklı mikro servislere odaklanan farklı takımlar ile hızlı uygulama teslimi.
    - DevOps ve CI/CD uygulamaları ve altyapısı.

<br>

# Data Sovereignty Per Microservice (Mikro servis başına Veri Egemenliği)

- Mikro servis mimarisinin önemli bir kuralı da şudur: her mikro servisin kendisine ait olan bir domain data'sı ve logic'i olmalıdır.

    Mikro servislerin her biri kengi mantığına (logic) ve verilerine, kendi başına deploy edilebilme özelliğine ve otonom bir yaşam döngüsü altında olması gerekir.

![](images/dbmodels.png)

<br>

- Geleneksel yaklaşımda bütün servisler sadece bir adet veritabanını kullanır. Mikro servis mimarisinde ise her servisin kendisine ait veri tabanı vardır.

- Geleneksel yaklaşımın tipik olarak 2 adet avantajı vardır:

    1. Tüm tablolarda ve verilerde çalışan ACID işlemleri.
    2. Tüm tablolarda ve verilerde çalışan SQL dili.

    Bu yaklaşım birden fazla tablodaki verileri birleştiren sorguları kolayca yazmayı sağlar.

    Mikro servis mimarisine geçildiğinde farklı tablolardan verileri birleştirme işlemi biraz daha zorlaşır. 

    Farklı servislerden verileri API endpoint'lerden alabiliriz veya asenkron kuyruk yapılarını kullanabiliriz.

- Verileri kapsüllememiz bize servislerin bağımlılıklarını azalatmamızı ve servisleri bağımsız olarak evriltebilmemizi sağlıyor.

<br>

# The Relationship Between Microservices and The Bounded Context Pattern

- Mikro servis'lerin konsepti **DDD** içerisindeki **Bounded Context (BC) Pattern**'den gelmektedir. 

    DDD, büyük modelleri bölerek ve sınırlarını kesinleştirerek ele alır. Her BC'nin kendisine ait model'i ve veritabanı olmalıdır.

<br>

# Challenges and solutions for distributed data management

### Challenge #1: How to define the boundaries of each microservice

Mikro servisleri oluştururken, her bir mikro servis için sınırları nasıl belirleyebiliriz bunları inceleyelim:

- İlk olarak uygulamanın mantıksal etki alanı modellerine (domain models) ve ilişkili verilere bakmamız gerekiyor.

- Veri içerisinde birbirinden ayrılmış alanları bulmaya ve uygulama içerisindeki farklı bağlamları bulmaya çalışmalıyız.

- Bağlamlar birbirinden bağımsız olarak tanımlanmalı ve yönetilmelidir.
- Her zaman servisler arasındaki bağlantıyı en aza indirmeye çalışmalıyız.

<br>

### Challenge #2: How to create queries that retrieve data from several microservices

Birkaç farklı mikro servisten veri alan sorgular nasıl oluşturulur? Bunun için alt kısımdaki çözümlere bakalım:

- Farklı mikro servislerden (onların kendi db'lerinden) veri almanın önerilen ve yaygın olan çözümü **API Gateway** mikro servisleridir.  

    Bu yöntemi kullanırken tıkanma noktaları oluşturmamaya (choke points) dikkat etmeliyiz. Bu sorunla karşılaşmayı azaltabilmek için her biri sistemin dikey dilimine veya işine odaklanan çok sayıda ayrıntılı API Gateway oluşturabiliriz.

- Diğer bir yöntem ise sorgu/okuma tabloları ile birlikte CQRS'tir (Command and Query Responsibility Segregation). 

    Bu yaklaşımda birden çok mikro servisin sahip olduğu verilerle salt okunur bir tablo oluşturulur. Tablo istemci uygulamasının gereksinimlerine uygun bir biçime sahiptir.

    Örnek olarak uygulamadaki bir ekran farklı servislerdeki verileri içeriyor olsun.

    Farklı bir veritabanında yalnızca sorgular için kullanılan bir tablo oluştururuz. Tablo uygulamanın ihtiyaç duyduğu alanlar ile sorgu tablosundaki sutunlar arasnda bire bir ilişki ile karmaşık sorgular için ihtiyaç duyulan verilere özel olarak tasarlanabilir. 

    CQRS farklı servislerdeki verileri birleştirme sorununu çözmekle kalmaz aynı zamanda da karmaşık join sorgularına göre çok daha performans avantajı da sağlar.

- Çözüm olarak düşünebilinecek son yöntem iste **Cold Data in central databases**.  

    Bu yaklaşımda, gerçek zamanlı veri gerektirmeyen karmaşık raporlar ve sorgular için şöyle bir yöntem kullanılır. Bu tarz veriler büyük veritabanlarına eklenir ve orada bekletilir. Rapor alınması gerektiğinde buradaki soğuk veriyi geri gönderir.

    Bu yöntemi kullanırken soğuk depo ile gerçek zamanlı verilerinizi senkronize tutabilmek için **olay güdümlü iletişim (event-driven communication)** veya farklı veritabanı araçları kullanılabilir.

    Son olarak servisler diğer servislerdeki verilere çok fazla ihtiyaç duyuyorsa burada mimari bir sorun olabilir. Öyle bir durumda gerekli adımları atarak servisler arasındaki veri bağımlılığı azaltılmaya çalışılır.

<br>

### Challenge #3: How to achieve consistency across multiple microservices

- Buradaki sorunumuz mikroservisler arasında veri bütünlüğünü nasıl sağlayacağmızdır. 

    Sorunu biraz daha açmak gerekirse, örnek olarka bir e-ticaret projesi düşünelim. Burada bir Sepet servisi ve Katalog servisinin var olduğunu varsayalım. 

    Kullanıcının sepetine eklediği bir ürünün fiyarı Katalog servisinde değiştiğinde Sepet Servisi'nin değişikliği uygulaması gerekecektir. Ekstra olarak mesela kullanıcıya sepetindeki bir ürünün fiyatının değiştiği belirten bir bildirim de gönderilmelidir, bu da bir servisin daha tetiklenmesi demektir.

    ![](images/privatedb.png)

    <br>

    Yukarıdaki görüntüden farkedildiği gibi, Katalog servisi direkt olarak gidip Ürün servisinin db'sini manipüle etmemelidir. 

    Bunun yerine Katalog ile Ürün servisi arasında asenkron olarak çalışan bir iletişim kurulmalı (mesaj ve event tabanlı iletişim) ve o iletişim üzerinden gerekli manipülasyonlar Ürün servisine yaptırılmalıdır.

- Uygulamalarda güçlü tutarlılığı ile yüksek ölçeklenebililik ve kullanılabilirlik arasında seçim yapmamız gerekiyor. 

    Mikro servislerin çoğunda kullanılabilirlik ve yüksek ölçeklenebilirlik ağır basmaktadır.

    Görev açısından kritik uygulamalar çalışır durumda kalmalıdır. Bu tarz uygulamalarda geliştiriciler zayıf tutarlılıkla çalışmak için teknikler kullanarak güçlü tutarlılık etrafında çalışabilirler.

- Veri tutarlılığını sağlayabilmek için genellikle event-driven iletişim kullanılır. Bunu da pub/sub yöntemi ile uygularız.

<br>

# Identify domain-model boundaries for each microservice

- Mikro servislerin sınırlarını ve boyutlarını belirlerken amacımız en ayrıntılı ayrımı elde etmek değildir ama mümkünse en küçük mikro servislere yönelmeliyiz.

    Bunun yerine hedefimiz en anlamlı ayrıma ulaşmaktır. Yukarıdaki boyut birim olarak değil, kabiliyet olarak en küçük olanı belirtmektedir.

- Servislerde aynı olduğunu düşündüğümüz modellerimiz olsa bile bunları birleştirmek yerine ayrı ayrı tutmalıyız. Örnek olarak alt kısımdaki görsele bakalım:

    ![](images/dbmodels.png)

    <br>

    Daha detaylı olarak bir modelin içerisine bakalım:

    ![](images/modelexample.png)

    <br>

# The API gateway pattern versus the Direct client-to microservice communication

### Direct client-to-microservice communication

- İstemciden direkt olarak mikro servise iletişim kurabiliriz.

    ![](images/clientToDirectMS.png)

    <br>

- Bu yaklaşımda mikro servislerin public endpoint'leri vardır. Bu endpoint'ler bazen farklı port'lar ile erişilebilir olabilirler.
- Burada cluster içerisindeki bir load balancer client'tan gelen istekleri mikro servislere dağıtır. Ayrıca SSL sonlandırmayı da üstlenir.
- Bu yaklaşım bir nebze küçük projelerde kullanılabilir, fakat büyük bir proje geliştirmek istediğimizde bazı sorunlar yaratacaktır.

    Büyük projelerde tek bir UI üzerinden bircen çok mikro servise erişmeye çalışmak istek ve cevap sayısını oldukça arttıracaktır. Bu da UI tarafında gecikme süresini ve karmaşıklığı arttırır.

- Güvenlik ve Oturum Yönetimi gibi önemli işlevleri her servise eklemek gerçekten ciddi geliştirme maliyetine neden olur. 

   Bunun yerine bunları direkt olarak erişilir kılmadan Docker Host gibi veya dahili cluster içerisinde bulundurabiliriz ve bu işlevleri API Gateway gibi merkezi bir yerde uygulayabiliriz.


<br>

### Why consider API Gateways instead of direct client-to-microservice communication

- Mikro servis tabanlı uygulamalarda genellikle birden çok servis vardır ve kullanılır. Eğer client'lar mikro servislere direkt olarak erişiyor ise bir çok servisin endpoint'ine yapılan bir çok isteği yönetebiliyor olmalıdır. 

    Ayrıca uygulama evrildiğinde, yeni mikroservisler eklendiğinde veya var olan mikroservisler güncellendiğinde client'ın bunlar ile baş etmesi çok çok zor olacaktır.

    Bu nedenle orta düzeyde veya dolaylı bir katmana (Gateway) sahip olmamız mikroservis uygulamalarında avantaj sağlayabilir.

<br>

### What is the API Gateway pattern?

- Bu pattern birden fazla client'ı ve mikro serivisi olan büyük uygulamarda mikro servis grupları için tek bir giriş noktası sağlar.
- API Gatewat pattern bazen **BFF (backend for frontend)** olarak da bilinebilir.
- Gateway'ler mikro servisler ile client'lar arasında yer alır. Bir bakıma ters proxy olarak davranır, yani istekleri istemcilerden hizmetlere yönlendirir.

    Ek olarak Authentication, SSL sonlandırma ve cache gibi özellikleri de sağlayabilir.

    Örnek olarak alt kısmı inceleyebiliriz:

    ![](images/simplegateway.png)

- Uygulamalar gateway üzerindeki bir endpoint'e bağlanır. Gateway bunları konfigüre eder ve istekleri mikro servislere yönlendirir.
- Eğer uygulamamız büyük çaplı bir proje ise tek bir gateway kullanmak sakıncalı olabilir. Çünkü gelen istekler gateway'in şişmesine neden olabilir.

    Bu nedenle Gateway'lerin de daha küçük gateway'lere bölünmesi ve ayrı ayrı kullanılması önerilir. Bölme işlemini iş sınırlarına ve istemci uygulamalarına göre ayırmalıyız.

    İlk ayrım noktası istemcilere göre olabilir.

    Örnek olarak alt kısımdaki görsele bakalım:

    ![](images/multiplegateway.png)

    <br>

    Gateway'lerin yukarıdaki gibi kullanılmasına **BFF Pattern** denir. Projenin büyüklüğüne göre daha çok gateway ihtiyacımız var ise o zaman ikinci ayrım noktası olarak, istemci ayrımını yaptıktan sonra iş sınırlarına göre ayrımları da yapıp yeni gateway'ler ekleyebiliriz.

<br>

### Main features in the API Gateway pattern

- **Reverse proxy or gateway routing.** Gateway'leri proxy gibi düşünebiliriz. Client'tan gelen istekleri mikro servislerdeki endpoint'lere iletir.

    Gelen istekler için tek bir giriş noktası oluşturur.

- **Requests aggregation.** Client'tan gelen birden çok isteği tek bir istemci isteğinde toplayabilir. Özellikle bir ekranda farklı mikro servislerden gelmesi istenen veriler varsa bu durum için çok uygun olur.

- **Cross-cutting concerns or gateway offloading.** Her bir mikro serviste tanımlanması gereken veya kendisine özgü olan bazı işlevler gateway'e aktarılır ve bir yerden diğer servislerin kullanması sağlanır. Bunlara örnek olarak bazı işlevler:

    - Authentication and authorization
    - Service discovery integragion
    - Response caching
    - Load balancing
    - Loggin, tracing, correlation
    - Headers, query strings, and claims transformation
    - IP allowlisting

<br>

# Azure API Management

![](images/apigatewaywithazure.png)

<br>

- Azure ile API Gateway yönettiğimizde bize fayda olarak loglama, güvenlik ve ölçüm gibi yönetim ihtiyaçlarımızı da kolayca karşılayabiliyoruz. 

    Ayrıca API'leri filtreleyebilir ve bu API'lere yetkilendirmeler uygulayabilir. Raporlara erişmek istediğimiz bize kolaylık sağlar.

- API'lerin güvenliğini sağlamak için **API using key, token ve IP filtreleme** öğelerini kullanabiliriz.

    Bu özellikler esnekliği sağlar ve detaylı limit bilgileri sağlar. API'lerin özelliklerini ve davranışlarını değiştirebiliriz ve cevapları cache'leyerek performansı arttırabiliriz.

<br>

# Ocelot

- Ocelot daha basit yaklaşımlar için önerilen hafif bir API Gateway'dir.

    Hafif, hızlı, ölçeklenebilir ve diğer bir çok özelliği ile birlikte kimlik doğrulamada sağlar.


<br>

# Drawbacks of the API Gateway pattern

- API Gateway'lerin bir dezavantajı potansiyel bir hata noktası oluşturmasıdır.
- API'nin yaptığı ek network çağrıları yanıt süresinin artmasına neden olabilir.
- Eğer gateway'ler doğru şekilde ölçeklendirilmezse darboğaza dönüşebilirler.
- API Gateway'ler ekstra olarak geliştirme maliyeti demektir. Kendi logic'leri ve veri eklentileri vardır.

    Geliştiriciler her bir mikroservisin her endpoint'ini belirlemeli ve gatewayi güncellemelidir.
- API Gateway tek bir ekip tarafından geliştirilirse, geliştirme darboğazı oluşabilir. 


<br>

# Communication types

- Servisler ve client farklı şekillderde iletişim kurabilir. Bu iletişim türleri ilk olarak 2 eksene ayrılabilir.

    1. İlk eksen iletişimin senkronize mi yoksa asenkronize mi olacağını belirler.

       - Senkron iletişim. Örnek olarak HTTP protokolü senkron şekilde çalışan bir protokoldür. İstemci bir istek atar ve o isteğe servisten bir cevap gelmesini bekler.
       - Asenkrol iletişim. AMQP protokolü asenkron mesajlaşmaya örnek olarak verilebilir. İstemci broker'a bir mesaj gönderir ve o mesaja yanıt beklemeden işlerine devam eder. Broker gelen mesajı iletmekten kendisi sorumludur.

        Örnek olarak bir kayıt ekranında email onay işlemi yapılacak. Burada o email gönderme ve onayı alma işlemi eğer 'gönder' butonu altında senkron bir şekilde yapılacak olursa kullanıcı sayfada bir şey yapmadan beklemek zorunda kalırdı, çünkü sunucudan bir yanıt beklenecekti.

        Bunun yerine kullanıcı bilgilerini girdikten sonra 'gönder' butonuna bastığında email doğrulama mesjaı bir broker'a gönderilir ve kullanıcı başka bir sayfaya yönlendirilir ve bekletilmemiş olur. Broker uygun olduğunda gelen mesajı kullanıcının email'ine gönderir ve kullanıcı bir ekranda durdurulup bekletilmeden email onay işlemi çözülmüş olur.

    2. İkinci eksen iletişimde bir tane mi dinleyici var yoksa birden fazla mı dinleyici var bunu belirler.

        - Eğer tek dinleyici varsa her istek mutalaka sadece bir dinleyici veya servis tarafından işleniyor olmalıdır.
        - Birden fazla dinleyici olduğunda istek 0 veya N dinleyici tarafından işlenebilmelidir. Burada iletişim asenkron olmak zorundadır (Örnek olarak pub/sub mekanizmasını kullanarak Event-Driven Arch. ile olması gibi). Burada message broker sistemleri veya service-bus'lar kullanılabilir. Sistem içerisinde bir veride değişiklik meydana geldiğinde veya yeni bir veri eklenmesi gibi durumlarda broker'lar veya bus'lar tarafından sub halindeki mikroservislere bir event gönderilir ve mikro servisler bu event'leri işlerler. 

# Async microservice integration

- Bir mikro servis oluşturulduktan sonra sisteme dahil edilirken dikkat edilmesi gereken nokta bu mikro servisin diğer mikro servisler ile asenkron olarak iletişimde olmasıdır. Bir de mikro servisler arasındaki iletişimin-bağımlılığın olabildiğince az olmasını istediğimizi tekrar belirtmiş olalım.

    Burada mümkünse senkron iletişimden (request/response)(HTTP) uzak durmaya çalışalım.

    Her mikro servisteki amacımız otonom olması ve halihazırda iletişimde olduğu başka bir mikro serviste bir sorun olsa da yeni dinleyicilere cevap verebiliyor olmasıdır.

# Multiple-Receivers message based communication

- Mikro servisler arasındaki iletişim mesaj tabanlı olması, yani pub/sub yönetimi ile yapılıyor olması ölçeklendirilebilirlik açısından önemlidir. Bir servisin yayınladığı mesajı başka 2 servis tüketiyor olsun. Eğer 3. bi servisin de bu mesajı tüketmesini istersek yapılması gereken şey sadece mesajı yayınlayan servise sub olmak. Bu sayede `Open/closed prensibi`'ne de bağlı kalmış oluyoruz.

## Microservice APIs

- Mikro servislerde API'leri oluştururken API'ler kullanılan yönteme veya protokole bağımlı olarak geliştirilir. Örnek olarak HTTP ve RESTful bir yapı kullanıyorsanız API URL'lerden, JSON formatındaki istek ve cevaplardan oluşacaktır. Veya mesajlaşma yöntemini kullanıyorsanız, API mesaj tiplerinden oluşacaktır.

- API'leri oluşturduktan sonra üzerinde değişiklikler yapmamız gerekebilir. Böyle durumlarda bu API ile iletişimde olan diğer servislerin etkilenmemesi için API'leri versiyonlamaya başlarız. Burada API güncellendikçe, yeni versiyonları çıkmayada devam etse de eski versiyonların belirli bir süre hizmet vermeye devam ediyor olması önemlidir.

 