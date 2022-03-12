# Docker

Bir proje içerisinde farklı servisler kullanılabilir. Her servisin kendisine ait olan bağımlılıkları ve kullandığı kütüphaneler olacaktır. Bu servislerin kütüphaneleri ve bağımlılıkları birbirlerini etkileyebilir. Ayrıca servislerin çalışabilmesi için uygun işletim sistemi üzerinde olmaları gerekmektedir. 

Bizim istediğimiz en iyi durum, bütün servislerin kütüphanelerinin ve bağımlılıklarının birbirinden ayrı olması ve birbirlerini etkilememesidir.
Eğer her servis kullandığı kütüphaneler ve bağımlılıkları ile birer paket içerisinde olursa aslında istediğimiz durumu elde ediyoruz.

# Container Nedir?

- Kendilerine ait Processleri, Servisleri ve Ağları vardır. Aynı işletim sistemi veya VM üzerinde çalışırlar. İzole edilmiş ortamlardır.

- Elimizde 2 Adet componentimiz olsun, ikisine de kendilerine ait olan bağımlılıkları ve kütüphaneleri koyduğumuzda bunlar birerer ***Container*** olarak düşünülebilir. Bunun sonucunda bu iki component-container birbirlerine etki etmezler, bir nevi izole edilmiş olurlar.

- Container'ların çalıştığı yerlerde sadece bir işletim sistemi vardır ve container'lar bu işletim sistemin kernel'ini (çekirdeğini) kullanırlar.

- Container türleri olarak ***LXC, LXD, LXCFS*** vardır. Docker bunlardan ***LXC*** türünü kullanmaktadır. Container'lar kullanılması ve kontrolü zor olan yapılardır. Docker bizlere bu container'ların kullanılmasını kolaylaştırır ve kontrol edilebilir hale getirir.

- Windows server üzerine Docker kurduğumuzda, Docker otomatik olarak bir Linux VM yükler ve container'ları VM üzerinden çalıştırır.

- VM'lerin container'lardan farkı olarak her bir VM'in kendine ait OS vardır. Container'lar üzerinde yüklü oldukları OS'u kullanılırlar. Ayrıca VM'ler ***Hypervisor*** ile birlikte çalışırlar.

 -  |Docker|VM|
    |---|---|
    |Düşük RAM kullanımı|Yüksek RAM kullanımı|
    |Düşük CPU kullanımı|Yüksek CPU kullanımı|
    |Düşük disk kullanımı|Yüksek disk kullanımı|
 
 <br>

# Image Nedir?

- İçerisinde bir çok farklı yapıyı barındıran yapılardır (İşletim sistemi, application vb.). Aslında Container'ların nasıl çalışacağının planını söyleyer. *"Bu Mysql'dir ve Mysql şu şekilde çalıştırılacak."* gibi bilgileri verir. Akılda kalması için, Image'leri geliştirdiğimiz projelerin kaynak kodlarına benzetebiliriz.

- Container'i tekrar açıklarsak, Image'i çalıştırdığımızda elde ettiğimiz proses (process) olarak düşünebiliriz. Mesela bilgisayarda çalıştırdığımız uygulamalar da process olarak çalışır. Örnek olarak Spotify'ı çalıştırdığımızda bilgisayarımızda bir process olarak çalışır. Docker'da bu şekilde çalışmaktadır.

- **Docker Hub** aslında bir nevi Image havuzudur diyebiliriz. Container olarak çalıştırmak istediğimiz uygulamaları, OS'ları, kütüphaneleri her neyse **Docker Hub** üzerinden indirebiliriz. Örnek olarak biz Windows veya MAC OS bilgisayarımızda Ubuntu'yu container olarak çalıştırmak istiyorsak `docker pull ubuntu` komutu ile Ubuntu'yu indirebiliriz.

- İndirdiğimiz bir Image'i çalıştırmak istersek, `docker run` komutu ile çalıştırabiliriz. Biz örnekte Ubuntu indirdiğimiz için `docker run ubuntu` komutunu çalıştırabiliriz. Eğer bir kez daha `docker run ubuntu` komutunu çalıştırırsak, ayrı bir process olarak bir tane daha Ubuntu çalışmaya başalyacaktı. Docker'ın güzel ve önemli özelliklerinden birisi de budur. Eğer `docker run ubuntu` komutunu çalıştırdığımızda, ilk önce local'de Ubuntu Image'ini arayacaktır. Eğer local'de bu Image yoksa, Docker Hub'a istekte bulunur ve bu Image'i otomatik olarak indirir.

<br>

# Docker ne işimize yarayacak?

- Process'ler birbirinden izole edilmiş olacak.

- Bilgisayarımıza indirmedek servisleri, uygulamaları kullanabiliriz.

- Geliştiricile ile DevOPS arasında bir iletişim olmadan projeler çalıştırılabilir.

- Test kısımlarında, testleri yapan kişilerin projenin çalışması için gerekli olan her şeyi kurmasına gerek kalmamış olur. Docker üzerinden çalıştırarak testleri yapabiliriz.

- Gelişticiler oluşturdukları projelerin içerisine bir DockerFile ekler ve içerisinde tanımlamaları yapar. Ardından yönetici,test yapan kişi veya her kimse, geliştirilmiş olan projeyi çalıştırmak için sadece bu DockerFile dosyasını built eder ve çalıştırır. 

<br>

# Docker kullanımı

- `docker pull 'image_name'` - Image'i indirir.  

- `docker run 'image_name'`	- Image'i çalıştırır. Eğer Image indirilmemişse, ilk önce indirir ardından çalıştırır. 

- `docker run -it ubuntu` yaparak ubuntunun içerisine gireriz. Normal çalıştırsaydık, process hemen çalışıp kapanacaktı. Ubuntu bir OS olduğu için bu şekildedir. Eğer Ubuntu'dan çıkmak istiyorsak, `exit` yazıp çıkabiliriz. `-it` Parametresi, kullanıcıdan girdi bekleyen uygulamaların hemen kapanmamasını sağlamak için kullanılır.

- `docker run -it --name 'name' ubuntu` - Image'i çalıştırır ve ismi 'name' olarak ayarlar. Eğer `--name` parametresini vermezsek otomatik olarak rastgele bir isim atanacaktır.

- `docker images` - İndirmiş olduğumuz image'leri gösterir.

- `docker ps` - Çalışan container'ları gösterir. 

- `docker container ls` - Çalışan container'ları gösterir. 

- `docker ps -a, docker ps -all` - Daha önce çalışmış olan bütün process'leri gösterir.

- `docker start 'container_name'` - Container'ı ismi ile çalıştırır.

- `docker stop 'container_name' || 'container_id'` - Container'ı ismi ile veya Id'si (en az 2 karakter) ile durdurur.

- `docker rm 'container_name' || 'container_id'` - Container'ı ismi ile veya Id'si (en az 2 karakter) ile kaldırır. Toplu silme işlemi için bir komut yok istersek kendimiz bir bash komutu yazabiliriz. Veya `docker rm id1 id2 id3..` şeklinde çoklu silme yapabiliriz.

- `docker run 'image_name':'tag'` - Tag etiketi, Image'in istenilen versiyonunun çalıştırılmasını sağlar. Eğer versiyon inmiş durumda değilse, versiyonu indirir ve çalıştırır.

- `docker image tag 'image_name' 'new_tag'` - Image'ten bir tane daha yaratır ve verilen etiket ile etiketler.

- `docker run -d 'image_name'` - Bir Image'in arka planda çalışmasını istiyorsak, `-d` parametresini kullanırız.

- `docker attach 'image_id` - Arka planda çalışan Image'i ön tarafa çeker.

- `docker container logs 'container_id'` - Container'ın başladığı andan itibaren, bu komutun çalıştırıldığı ana kadar olan logları gösterir.