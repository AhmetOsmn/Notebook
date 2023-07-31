# Selamlar

Gençay Hoca'nın yayınladığı [EF Core eğitiminden](https://www.youtube.com/watch?v=dbI-kostQWo&list=PLQVXoXFVVtp1o3nq3-IXv42bPaFlzroBE&index=1) aldığım notlara alt kısımdan erişebilirsiniz.

<br>

# 1 - ORM Nedir (Object Relational Mapping - Nesne İlişkisel Eşleme)?

- Projenin veritabanı ile iletişime geçebilmesi için bir connection oluşturulmalı ve bu connection üzerinden veritabanı komutları çalıştırılmalıdır.

    Proje içerisinde direkt olarak yazılan sql komutları kirli kod yazılmasına, güvenlik açıklarına, bağımlılığa ve zor yönetilebilirliğe neden olur. 

    Özellikle veritabanı bağımlılığı oluşan ön önemli dezavantajdır.
- ORM yaklaşımı yukarıdaki dezavantajları çözebilmek için geliştirilmiş bir yaklaşımdır. Veritabanı ile veri transferi yapılırken, OOP'den olabildiğince faydalanmayı hedefler ve veritabanı bağımlılığını ve bilgisini azaltır.

    Proje içerisinde katı veritabanı sorgularını kullanmak yerine, veritabanında var olan objelerin (veritabanı, tablolar ve veriler) projede de OOP neseneleri olarak kullanılabilmesini sağlar.

    Yapılması gereken işlemler artık direkt olarak veritabanı komutları üzerinden yapılmak yerine, nesneler üzerinden yapılır.
- ORM kullandığımızda kullandığımız veritabanına uygun sorgular otomatik olarak oluşturulur.Bu durum geliştiricinin, projede kullanılan veritabanı bilgisinin çok iyi olmadan da proje geliştirilebilmesini de sağlar.

<br>

# 2 - Neden ORM Kullanmalıyız?

- İlişkisel Eşleştirme (Object Relational Mapping): Ef, yazdığımız kodun karşılığı olarak bir sql sorugusu oluşturur ve bu sorguyu veritabanında çalıştırır. Gelen data'yı C# ile oluşturulan nesnelere map'leme işlemine `Object Relational Mapping` denir.

<br>

# 4 - EF Core Nedir? 

- ORM yaklaşımını benimseyen bir araçtır.
- Code First ve Database First yaklaşımları ile yazılım ve veritabanı arasındaki iletişimi sağlar.
- Kod üzerinden veritabanı nesnelerini oluşturup kullanabilmemizi sağlar.
- İçerisinde bulunan `LINQ` ifadeleri ile query sorguları oluşturabilmeyi destekler.
- Eğer `dotnet cli` üzerinden EF ile ilgili işlemler yapmak istiyor isek, projeye `Microsoft.EntityFrameworkCore.Design` isimli paketi kurmamız gerekir. 

    Eğer EF işlemlerini `package manager console` üzerinden gerçekleştirmek istiyorsak, projeye `Microsoft.EntityFramework.Tools` paketini kurmamız gerekir.

 <br> 

# 5 - Code First ve Database First Yaklaşımları

- EF Core, proje içerisindeki veritabanı çalışmalarını gerçekleştirmek için, veritabanının daha öncesineden var olup olmamasına göre iki farklı yaklaşım sergiler: Code First ve Database First.

    Bu iki yaklaşımın ortak amacı kod üzerinde veritabanı modellemesi yapabilmektir. 

- `Database First:` Üzerinde çalışacağımız projenin kullanacağı veritabanı halihazırda mevcut ise, direkt olarak bu veritabanını modelleyerek proje içerisinde kullanılmasına *Database First* denir.

    Veritabanını modellerken *Scaffold* talimatlarını kullanırız.    
    
    Avantajları:
    - Veritabanında yapılan değişiklikleri hızlı bir şekilde koda aktarmamızı sağlar.
    
    <br>

    Dezavantajları:
    - Yönetim veritabanı tarafından sağlanacağı için veritabanı bilgisi gerektirir.

<br>

- `Code First:` Eğer çalışılan projede kullanılacak olan veritabanı henüz mevcut değilse ve veritabanını kod tarafından tasarlayıp, veritabanını oluşturup yönetmek istiyorsak uyguladığımız yaklaşıma *Code First* yaklaşımı denir.

    Avantajları:
    - Kod üzerinden veritabanını yönetmemizi sağlar.    

     <br>

    Dezavantajları:
    - Oluşturulacak veritabanının tasarımı ve stratejileri developer'ın sorumluluğunda olur.

<br>

- Şuna dikkat etmemiz gerekir. Eğer ortada bir veritabanı yok ise kesinlikle *Code First* yaklaşımı kullanılır diyemeyiz. Veritabanı ayrı bir şekilde hazırlanıp *Database First* yaklaşımı ile de kullanılabilir. Burada mevcut ihtiyaçlara göre hareket edilir.

    Eğer mevcutta bir veritabanı var ise ve biz proje içerisinde bu veritabanını kullanacaksak en doğru yaklaşım çok çok yüksek ihtimalle *Database First* olacaktır.

<br>

# 6 - EF Core Aktörleri

## Veritabanı - DbContext

- Bir ORM kullanılarak veritabanında çalışmalar yapabilmek için bizim şunları modellemiş olmamız gerekiyor: Veritabanın kendisini, Veritabanı içerisindeki tabloları, tablolar içerisindeki kolonları ve veritabanı nesnelerini. Çözüm olarka bu modelleme işlemini *class*'lar üzerinden gerçekleştiririz.

- EF Core'da veritabanını modellediğimiz class `DbContext` olarak nitelendirilir. Genel kullanımda, proje içerisinde veritabanını modellediğimiz class'a isim verirken *`{classIsmi}DbContext`* şeklinde isimlendirme yapılır. 

    Bu modelleme işlemi EF Core namespace'i içerisinde yer alan *`DbContext`* sınıfından bir class türetilerek gerçekleştirilir. Yani EF Core'un bu sınıfın veritabanını modellediğini anlayabilmesi için bu türetme işleminin yapılması gerekir.

- DbContext'sınıfının sorumlulukları:

    - Temel konfigürasyonlardan sorumludur (veritabanı bağlantısı, tabloların ilişkileri vb.).
    - Sorgulama operasyonlarının yürütülmesi.
    - Sorgulamalarda elde edilen verilerin değişikliklerinin takip edilmesi (Change Tracing).
    - Verilerin kayıt edilmesi, silinmesi, güncellenmesi gibi işlemlerin gerçekleşmesinden sorumludur.
    - Caching'ten sorumludur.

## Tablo Nesnesi - Entity

- Veritabanı içerisindeki tabloları temsil eden, modelleyen class'lar `Entity` olarak nitelendirilirler.

    Not: Veritabanında tablo isimleri çoğul olarak verilir. Kod tarafında o tabloyu modelleyen class'lar ise tekil olarak isimlendirilir.

    Veritabanında -> Orders <br>
    Class ismi    -> Order

- Tablolar nasıl veritabanı içerisinde yer alıyorsa, kod kısmında da bunu modellememiz gerekiyor.

    Yani tablolara karşılık gelen entity class'ları, veritabanına karşılık gelen DbContext class'ı içerisinde tanımlanmalıdır.
    
    Entity class'ları context class'ı içerisinde tanımlanırken `DbSet` tipinde tanımlanır. Örnek olarak:

    ```cs
    public class NorthwindDbContext : DbContext
    {
        // Order  -> Entity
        // Orders -> Tablo Ismi
        public DbSet<Order> Orders { get; set; }
    }
    ```

## Tablo Kolonları

- Tablo içerisindeki kolonlar, entity içerisinde `property` olarak nitelendirir.

    ```cs
    // Tablo'yu temsil eden class.
    public class Customer
    {
        // Tablo içerisindeki kolon'u temsil eden property.
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    ```

## Veriler

- Veritabanı içerisindeki verileride entity class'larının instance'ları ile modellenir. 

    Örnek olarak *Customer* tablosundaki bir satırın 
        **CustomerId** kolonunda: 1,
        **Name** kolonunda: Ahmet,
        **Surname** kolonunda: Sezgin
     yazdığını düşünelim. Bu satırdaki veriyi kod tarafında alt kısımdaki gibi modelleyebiliriz:

    ```cs
    var customer = new Customer()
    {
        CustomerId = 1,
        Name = "Ahmet",
        Surname = "Sezgin"
    }
    ```

<br>

# 7 - Database First

- Package Manager Console üzerinden, var olan bir veritabanını C# ortamında modellemek istediğimizde alt kısımdaki farmatta bir komut çalıştırmamız yeterli olacaktır:

        Scaffold-DbContext 'ConnectionString' DbProvider (EfCore.SqlServer vb.)

        Scaffold-DbContext 'ConnectionString' DbProvider -Tables Table1, Table2


    Burada yukarıdaki işlem için Tools ve SqlServer paketlerinin kurulu olması gerekir.

    Aynı işlemi Dotnet CLI üzerinden aynı işlemi yapmak için:

        dotnet ef dbcontext scaffold 'ConnectionString' DbProvider

        dotnet ef dbcontext scaffold 'ConnectionString' DbProvider --table Table1 --table Table2

    Paket olarak Provider ve Design paketlerinin yüklü olması gerekecektir.

- Modellemeyi klasörlerin içerisine yapmak istediğimizde şu şekilde çalışabiliriz:

        Scaffold-DbContext 'ConnectionString' DbProvider -ContextDir `klasör yolu` -OutputDir 'entity'lerin klasör yolu' 

        dotnet ef db context scaffold 'ConnectionString' DbProvider --context-dir `klasör yolu` --output-dir 'entity'lerin klasör yolu' 

- Database'de meydana gelen değişiklikleri tekrar aynı komut ile modellemek istediğimizde, dosyalar zaten mevcut tarzında bir hata alırız. Eğer aynı komutun sonuna *Force* parametresini ekleyip çalıştırırsak var olan dosyaları ezerek modellemeyi tekrar yapacaktır. 

        dotnet ef dbcontext scaffold 'ConnectionString' DbProvider -Force

- Eğer database'den modellenen entity'ler üzerinde güncelleme/geliştirme yapmak istersek bu değişiklikleri direkt olarak entity sınıfı üzerinde yaptığımızda, *Force* parametreli komut tekrar çalıştırıldığında bizim yaptığımız değişiklikler kaybolur. 

    Yukarıdaki sorunun önüne geçmek için şöyle bir yöntem izleyebiliriz: Otomatik oluşan entity sınıfları `Partial` olarak oluşmaktadır. Biz farklı bir klasör içerisinde ama aynı namespace'e ve aynı isme sahip bir partial class daha tanımlarız. Yapmak istediğimiz değişiklikleri bu yeni açtığımız partial class içerisinde gerçekleştirdiğimizde artık *Force* komutlarından korunuyor hale geliriz.

<br>

# 8 - Code First

- Migration'lar, Context class'ında veya başa bir yerdeki konfigürasyonda belirtilen/kullanılan provider'a göre, veritabanı bazlı olarak oluşturulurlar. Migration oluşturmak için PMC veya Dotnet CLI kullanırız.

    SqlServer Provider'ı kullanılarak oluşturulan migration ile, PostgreSql Provider'ı kullanılarak oluşturulan migration birbirinden farklı olacaktır. Ayrıca database tipi ile oluşturulan migration eşleşmezse/uyuşmazsa hata alınır. Yani özet olarak SqlServer için oluşturulan bir migration, Postgre database tarafından kullanılamaz.

- Migration'ların hepsini değil de, belirlenen birisine (önceden uygulanan bir migration olabilir) dönüş geçiş yapmak istersek şu şekilde yapabiliriz:

        CLI -> dotnet ef database update mig_name

        PMC -> update-database mig_name

    `update-database` / `dotnet ef database update` komutları direkt olarak kullanılırsa son migration'a kadar hepsi uygulanır.

<br>

#  9 - Temel Kural | OnConfiguring | Tablo Adı

- `OnConfiguring:` Bu metot bizim EfCore tool'unu özelleştirmemizi sağlar. Context class'ı içerisinde override edip gerekli ayarlamalar ile özelleştirmeleri (Provider, ConnectionString, Lazy Loading vb.) yaparız.

- `Temel Entity Kuralı:` EfCore her tabloda default olarak primary key olması gerektiğini savunur. Eğer biz bir entity içerisinde primary key belirtmezsek, migration eklerken default'ta hata alırız. Bunu aşmanın yolları vardır fakat temelde bu şekilde çalışır.

    Eğer bir entity'nin içerisinde:

    ```cs
    public int Id {get; set;}
    public int ID {get; set;}
    public int EntityNameId {get; set;}
    public int EntityNameID {get; set;}
    ```

    Bu property'lerden birisi var ise EfCore bu property'i otomatik olarak primary key olarak tanımlar.

<br>

# 10 - Veri Ekleme ve Veri Kalıcılığı

- Context üzerinden veri eklemenin 2 yolu vardır:

    - context.AddAsync(); // Parametre olarak `Object` tipinde bir veri alır.
    - context.DbSet.AddAsync(); // Parametre olarak belirtilen sınıfa özgü tipteki veriyi alır.
    
    `DbSet` olarak belirtilen şey ekleme yapılmak istenen tablonun adıdır. Bu ikisi arasındaki tek fark tip güvenli ekleme yapmaktır.

- `SaveChanges():` insert, update, delete sorgularını oluşturur ve bunları bir transaction içerisinde veritabanına gönderip çalışmasını (execute edilmesini) sağlar. 
Eğer oluşturulan sorgulardan herhangibi birisi başarısız olursa, bu işlemden önce yapılan bütün işlemleri geri alır, yani `rollback` yapılır.

    ***Not:*** *SaveChanges()* fonksiyonu çalıştırılmadan veritabanına herhangi bir sorgu gönderilmez. 

- Bir verinin ef core'a göre statüsünü öğrenmek istiyorsak şu şekilde bakabiliriz:

    ```cs
    context.Entry(order).State;
    ```

    Eğer veri henüz database'e eklenmemişse ve biz yukarıdaki şekilde bu verinin (objenin) ef core'a göre statüsüne bakarsak `Detached` olduğunu görürüz.

    Eğer veri database'e eklenmek için **Context.Add(obje)** metodunda kullanılmış ise ama henüz **SaveChanges()** metodu kullanılmamış ise statüsü `Added`'tir.

    Eğer veri yukarıdaki gibi bir Context metodu ile kullanılmış ise ve ardından **SaveChanges()** metodu da çalıştırılmış ise artık statüsü `Unchanged` olur. Eğer **SaveChanges()** metodundan sonra tekrar başka bir işlem yapılırsa verinin statüsü tekrar değişecektir. Her **SaveChanges()** kullanımında statü `Unchanged` durumuna getirilir.

    Veri üzerinde güncelleme işlemi gerçekleştirildiğine `Modified` statüsüne sahip olur.

    Eğer bir silme işlemi gerçekleştirirsek `Deleted` durumuna gelecektir.

- Database içerisinde transaction açmak masraflı bir işlemdir. Çoklu veri ekleme işlemi gibi işlemlerde her ekleme için ayrı ayrı transaction açmak yerine tek transaction içerisinde yapabiliriz. 

    Yani örnek olarak her ekleme işleminden sonra **SaveChanges()** kullanmak yerine eklenecek verileri **Add()** ile ekledikten sonra tek bir **SaveChanges()** kullanarak işlemi gerçekleştirebiliriz.

    Çoklu ekleme işlemi için ek olarak **AddRange()** metodu da kullanılabilir.

<br>

# 11 - Veri Güncelleme ve Veri Kalıcılığı

- `ChangeTracker:` Context üzerinden gelen verilerin takibini yapan mekanizmadır. Bu mekanizma sayesinde context üzerinden gelen veriler ile update veya delete sorgularının oluşturulacağı anlaşılır.

- Eğer *Context* üzerinde getirilen bir veriyi güncellemek istiyorsak (yani takip edilen bir veriyi), ilgili güncellemeleri yaptıktan sonra direkt olarak **SaveChanges()** metodunu kullanmamız yeterlidir. Örnek olarak:

    ```cs
    var selectedOrder = context.Orders.FirstOrDefault(x => x.Id == id);
    selectedOrder.Status = OrderStatus.Accepted;
    context.SaveChanges();
    ```

- Eğer context üzerinden elde dilmeyen bir veriyi (yani Tracker tarafından takip edilmeyen veri) database'de güncellemek istersek şu şekilde yapabiliriz:

    ```cs
    context.Orders.Update(order);
    context.SaveChanges();
    ```

    Yukarıdaki şekilde olduğu gibi eğer **Update()** fonksiyonu ile takip edilmeyen bir veriyi güncellemek istiyorsak, bu verinin kesinlikle `Id` değeri verilmelidir.

<br>

# 12 - Veri Silme ve Veri Kalıcılığı

- *ChangeTracking* tarafından takip edilmeyen bir veriyi silerken *Update* örneğindeki gibi, **Id** değerine sahip olan bir nesne **Remove()** fonksiyonuna verilir ve ardından **SaveChanges()** metodu uygulanır.
- Farklı bir kullanım olarak *EntityState* üzerindenden de silme işlemi yapabiliriz. Örnek olarak:

    ```cs
    context.Entry(order).State = EntityState.Deleted;
    context.SaveChanges();
    ```

- Birden fazla veriyi silmek istiyorsak **RemoveRange()** metodunu kullanabiliriz.

<br>  

# 13 - Veri Sorgulama

- `Method Syntax:` Database üzerinden verileri sorgulama işlemini metotlar ile yaptığımız durumlara denir (LINQ Sorguları).

    ```cs
    var orders = context.Orders.ToList();
    ```

- `Query Syntax:` Verileri sorgularken query'ler kullandığımız durumlardır (LINQ Query'leri).
  
    ```cs
    var orders = (from order in context.Orders select order).ToList();
    ```        

- Oluşturulan sorgulardan cevap alabilmek için bu sorguların bir şekilde çalıştırılıyor olamsı gerekiyor. Sonraki kısımlarda tekrar açıklanacak fakat şimdilik kısaca 2 kavramı basit düzeyde incelememiz gerekiyor:

    - `IQueryable:` Sorguya karşılık gelir. Ef core üzerinden yapılmış olan sorgunun execute edilmemiş halini temsil eder. 
    - `IEnumerable:` Sorgunun execute edilip, elde edilen verilerin in memory'e yüklenmiş halini temsil eder.

    <br>

    Sorguyu (IQueryable) execute ettiğimiz zaman artık ***IEnumerable*** duruma geçmiş oluyoruz, yani artık elimizde bir veri/veri seti oluyor. *IQueryable* halden *IEnumerable* hale geçebilmek için farklı yöntemler vardır ama şimdilik **ToList()** kullanıyoruz diyebiliriz. Örnek olarak:

    ```cs
    var orders = context.Orders; //=> IQueryable
    var orders = context.Orers.ToList(); //=> IEnumerable
    ```

    Veya bir sorguyu oluşturup bir değişkende tuttuğumuzda (IQueryable durmuda), bu değişkeni bir **foreach** döngüsü içerisinde tetiklersek, bu sorgu execute edilir. Bu kullanım `Deferred Execution (Ertelenmiş Çalışma)` olarak isimlendirilir. Örnek olarak:

    ```cs
    var orders = from order in context.Orders select order; // IQueryable

    foreach(var order in orders)
    {
        Console.WriteLine(order.Id);
    }
    ```

- `Deferred Execution:` Ertelenmiş işlemlerde eğer oluşturulan sorgu henüz execute edilmeden bu sorgu içerisindeki bir değişkenin değeri değiştirilirse, sorgu execute edildiğinde bu değişkenin son hali ile bir sorgu generate edilecektir. Örnek olarak:

    ```cs    
    int orderId = 5;
    var orders = from order in context.Orders where order.Id > orderId select order; 

    foreach(var order in orders)
    {
        Console.WriteLine(order.Id);
    }
    // Bu örnekte id'si 5'ten büyük olan order'lar getirilecektir.
    ```
    ```cs
    int orderId = 5;
    var orders = from order in context.Orders where order.Id > orderId select order; 
    orderId = 15;

    foreach(var order in orders)
    {
        Console.WriteLine(order.Id);
    }
    // Bu örnekte ise id'si 15'ten büyük olan order'lar getirilecektir. Çünkü IQueryable durumdaki sorgu execute edildiğinde, sorgu içerisindeki **orderId** değişkenin son değeri 15'ti.
    ```

<br>

# 14 - Sorgulama Fonksiyonları (Çoğul Veri Getirenler)

- `ToList():` Üretilen sorguyu execute etmemizi sağlayan fonksiyon  fonksiyonudur.
- `ThenBy():` **OrderBy()** üzerinde yapılan sıralama işlemini farklı kolonlarda uygulamamızı sağar. Örnek olarak:

    ```cs
    var orders = context.Orders.OrderBy(x => x.OrderDate).ThenBy(x => x.Id);
    ```

    Default olarak **OrderBy()**'da da default olduğu gibi *ascending* sıralama yapar.

<br>

# 15 - Sorgulama Fonksiyonları (Tekil Veri Getirenler)

- `Single():` Eğer sorgu neticesinde birden fazla veri geliyorsa veya hiç veri gelmiyorsa her iki durumda da exception fırlatır.
- `SingleOrDefault():` Eğer sorgu neticesinde birden fazla veri geliyorsa exception fırlatır, hiç veri gelmiyor ise **null (default değerini)** döner.
-  `First():` Sorgu neticesinde elde edilen verilerin ilkini getirir. Eğer hiç veri gelmiyorsa exception fırlatır.
-  `FirstOrDefault():` Sorgu neticesinde elde edilen verilerin ilkini getirir. Eğer hiç veri gelmiyorsa **null (default değerini)** döner.
-  `Find():` Eğer tablodaki *primary key* üzerinden hızlı bir şekilde bir arama yapılmak isteniyorsa kullanılır.

    Ayrıca **Composite Primary Key** durumlarında da kullanılır. Örnek olarak:

    ```cs
    var orderDetail = context.OrderDetails.Find(2,5); // 2 -> OrderId, 5 -> Id (OrderDetailId)
    ```

    **Find()** Fonksiyonu üstteki diğer fonksiyonlardan farklı olarak şu şekilde davranır: 
    
    - Üstteki fonksiyonlar sorguları her zaman database'e gönderir. **Find()** ise veriyi ilk önce context içerisinde arar (yani in memory'de), eğer burada yok ise sorguyu database'e gönderir. Performans açısından avantaj sağlamış olur.
    - **Find()** sadece *primary key* üzerinde arama yapar, üstteki diğer fonksiyonlar ise *where* ifadesi ile istenilen kolon üzerinde arama yapabilir.
      
- `Last():` Sorgu neticesinde elde edilen verilerin sonuncusunu getirir. Eğer hiç veri gelmiyorsa exception fırlatır.

    ***Not:*** **Last()** ve **LastOrDefault()** fonksiyonlarını kullanmadan önce **OrderBy()** veya **OrderByDescending()** fonksiyonları kullanılmak zorundadır. Ornek olarak:

    ```cs
    var lastOrder = context.Orders.OrderBy(x => x.CreatedDate).Last(x => x.Id > 5);
    ```

- `LastOrDefault():` Sorgu neticesinde elde edilen verilerin sonuncusunu getirir. Eğer hiç veri gelmiyorsa **null (default değerini)** döner.

<br>

# 16 - Sorgulama Fonksiyonları Ekstralar

- `Count():` Bu fonksiyonun kullanımında IEnumerable ile mi çalışacağımıza IQueryable ile mi çalışacağımıza dikkat etmeliyiz. Eğer sadece veri sayısını elde etmek istiyorsak, IQueryable haldeyken, yani verileri in memory'e çekmeden (IEnumerable hale getirmeden) bunu yapabiliriz. Bu sayede daha performanslı bir işlem yapmış oluruz. Örnek olarak:

    ```cs
    var orderCount = (context.Orders.ToList()).Count(); // Kötü kullanım.
    var orderCount = context.Order.Count(); // İyi kullanım.
    ```
    Burada **Count()** içerisine şart belirterek te veri sayısı sorgulayabiliriz. Örnek olarak:

    ```cs
    var orderCount = context.Order.Count(x => x.Price > 50); // Ücreti 50'den büyük olan order'ların sayısı.
    ```
- `Any():` Sorgu neticesinde geriye herhangi bi cevap dönüyorsa **true**, dönmüyorsa **false** döner.

    İstersek şartlar ile oluşturulan bir sorgunun sonuna **Any()** ekleyerek bir veri gelip gelmediğini kontrol edebiliriz, veya direkt olarak **Any()** içerisine de şartlar ekleyerek veri kontrolü yapabiliriz.

- `Distinc():` Eğer sorgude tekrar eden kayıtlar varsa, bu kayıtları tekil hale getirir. 

- `All():` Verilen sorgu neticesinde gelen verilerin tamamının, şarta uyup uymadığını kontrol eder. Eğer bütün veriler şarta uyuyorsa **true** uymuyorsa **false** döner. Örnek:

    ```cs
    var orders = context.Orders.All(x => x.Price > 10); // Gelen order'ların hepsinin fiyatı 10'dan büyük mü? Hepsi büyükse true, bir tane bile küçük veya eşit varsa false döner.
    ```

- `Contains():` Sql sorgusu içerisine **like** ifadesi eklemek istersek **Where()** komutu içerisinde **Contains()** kullanırız. Buradaki like sorgusu **%..%** yapısında olacaktır. Örnek olarak:

    ```cs
    var orders = context.Ordes.Where(o => o.Description.Contains("test")).ToList();
    ```

- `StartsWith():` Sql sorgusu içerisine **like** ifadesi eklemek istersek **Where()** komutu içerisinde **StartsWith()** kullanırız. Buradaki like sorgusu **..%** yapısında olacaktır. Örnek olarak:

    ```cs
    var orders = context.Ordes.Where(o => o.Description.StartsWith("te")).ToList();
    ```

- `EndsWith():` Sql sorgusu içerisine **like** ifadesi eklemek istersek **Where()** komutu içerisinde **EndsWith()** kullanırız. Buradaki like sorgusu **%..** yapısında olacaktır. Örnek olarak:

    ```cs
    var orders = context.Ordes.Where(o => o.Description.EndsWith("st")).ToList();
    ```

<br>  

# 17 - Dönüşüm Fonksiyonları Ekstralar

- Sorgu sonucunda elde edilen verileri dönüştürmek istersek bu fonksiyonları kullanırız.
  
- `ToList()`, `ToArray()`, `ToDictionary()` fonksiyonlarının ortak noktası, IQueryable durumdaki ifadeleri, IEnumerable'hale dönüştürür. Yani sorguları execute eder ve yanıtları getirir.
    
- `Select():` Bu fonksiyonun işlevsel olarak birden fazla davranışı vardır.

    1. Select fonksiyonu, oluşturulacak olan sorguda tablodan çekilecek olan kolonları ayarlamamızı sağlar.

    ```cs
    var orders = context.Orders.Select(x => x.Id).ToList(); // Order'ları getirirken sadece Id'lerini getir.
    ```

    2. Select fonksiyonu, gelen verileri farklı türlerde karşılamamızı sağlar.

    ```cs
    var orders = context.Orders.Select(x => new 
    {
        Id = x.Id,
        Price = x.Price
    });
    ```

- `SelectMany():` İlişkili veriler üzerinden tekil bir veri elde etmek istiyorsak kullanırız. Örnek olarak:

    ```cs
    var orderDetails = context.OrderDetails.Include(x => x.Order).SelectMany(x => x.Order, (o, od) => new 
    {
        o.Id,
        o.Date,
        od.Id,
        od.Description
    });
    ```

<br>

# 18 - Sorgulama Fonksiyonları Ekstralar 2

- `GroupBy():` Verilerde gruplama yapmamızı sağlar. Örnek olarak:

    ```cs
    var datas = contex.Orders.GroupBy(x => x.Date).Select(group => new 
    {
        Count = group.Count(),
        Date = group.Key
    }).ToList();
    ```

<br>

# 19 - Change Tracker Detayları

- `ChangeTracker Property'si:` Takip edilen nesnelere erişebilmemizi sağlayan ve gerekli işlemleri yapmamıza imkan sunan property'dir.

    Eğer takip edilen verileri görmek istersek alt kısımdaki örnekteki gibi erişim sağlayabiliriz:

    ```cs
    var datas = context.ChangeTracker.Entries();
    ```

- `ChangeTracker.DetectChanges():` Yapılan operasyonlar otomatik olarak tracking ediliyor olsa bile, bu metot ile opsiyonel olarak EF Coru kontrole zorlayabiliriz. Normalde **SaveChanges()** metodu otomatik olarak bu metodu kullanır fakat biz kendimiz de direkt olarak tetikleyebiliriz.

- `ChangeTracker.AutoDetectChangesEnabled Property'si:` Normalde **SaveChanges() ve Entries()** metotları kullanıldığında, **DetectChanges()**'ı otomatik (default olarak) tetikliyor demiştik. Bu property'i **false** durumuna getirirsek artık tetikleyemez duruma gelir. Eğer biz tracing işlemlerini kendimiz manuel olarak yönetmek istiyorsak, sadece ihtiyacımızın olduğu yerlerde tracing yaparak performans artışı sağlamak istiyorsak bu property'i **false** yaparak kapatabiliriz.

- `ChangeTracker.Entries():` **Change Tracker** tarafından takip edilen her entity nesnesinin bilgisini **EntityEntry** türünden elde etmemizi sağlar. Ayrıca bu entity'ler üzerinden belirli işlemleri de yapabilmemize olanak tanır.

- `ChangeTracker.AccecptAllChanges():` **SaveChanges()** veya **SaveChanges(true)** kullanımlarında EF Core her şeyin doğru bir şekilde gittiğini varsayar ve track ettiği verilerin takiplerini keser ve yeni değişikliklerin takip edilmesini bekler. Böyle bir durumda hata oluşursa, verileri track edilmediği için düzeltme şansımız yoktur.

    Eğer biz **SaveChanges(false)** şeklinde bir kullanım yaparsak, track edilen verilerin işlemleri başarılı veya başarısız olsa da veriler takip edilmeye devam edilecektir. Burada biz kendi irademiz ile bu takip edilmeyi (tracking'i) kırmak/kesmek istersek **AccecptAllChanges()** metodunu kullanırız. Burada dikkat etmemiz gereken nokta eğer işlemin başarılı olduğundan eminsek takibi kesmeliyiz.

- `ChangeTracker.HasChanges():` Takip edilen nesneler arasında değişiklik yapılanların olup olmadığını kontrol eder. Arka planda **DetectChanges()** metodunu tetikler. 

- `OriginalValues Property'si:` Bir veri üzerinde değişiklik yapıldıktan sonra henüz bu değişiklikler database'e yansıtılmadan, database'deki son değerleri almak istersek kullanabiliriz. Örnek olarak:

    ```cs
    var order = context.Orders.FirstOrDefault(x => x.Id = 5);
    order.Date = DateTime.Now; // Date değeri güncellendi fakat database'e işlenmedi.

    var orgDate = context.Entry(order).OriginalValues.GetValue<DateTime>(nameOf(order.Date)); // order nesnesinin Date değeri güncellenmeden önceki değeri yani database'e işlenen son hali gelir.
    ```

    Yukarıda ki işleme benzer bir işlemi şu şekilde de gerçekleştirebiliriz:

    ```cs
    var order = context.Orders.FirstOrDefault(x => x.Id = 5);
    order.Date = DateTime.Now; 

    var orderDbValues = context.Entry(order).GetDatabaseValues(); // order nesnesinin database'e işlenen son değerlerini getirir.
    ```

- `Change Tracker'ın Interceptor Olarak Kullanılması:` Sürekli **SaveChanges()** metodu kullanılan yerlerde/zamanlarda, **SaveChanges()** metodunu override ederiz ve bu override edilen metot içerisinde database'de işlemleri gerçekleştirmeden araya girerek ChangeTracker üzeriden ihtiyacımız olan değişiklikleri/geliştirmeleri yapabiliriz.

<br>

# 20 - Change Tracker Mekanizmasının Davranışlarını Yönetmek

- `AsNoTracking():` Context üzerinden getirilen bütün veriler default olarak Change Tracker tarafından takip edilmektedir. Bu takip işleminin bir maliyeti vardır. Eğer biz context ile getirdiğimiz veriler üzerinde bir değişiklik yapmayacaksak, sadece listeleme vb. işlemler için kullanacaksak bu maliyeti azaltmak için **AsNoTracking()** metodunu kullanırız.

    Bu fonksiyon kullanılarak getirilen veriler üzerinde bir değişiklik yaptığımızda bu veriler tracking edilmediği için **SaveChanges()** fonksiyonu kullanıldığında database'e değişiklikler işlenmez. Eğer biz tracking edilmeyen bir veriyi güncellemek istersek daha önce belirtildiği gibi **Update()** fonksiyonunu kullanabiliriz.

- `AsNoTrackingWithIdentityResolution():` Normalde Change Tracker mekanizması sayesinde yinelenen datalar aynı instance'leri kullanırlar. Eğer biz **AsNoTracking()** fonksiyonunu kullanırsak, yinelenen dataların hepsi kendisine ait olarak oluşturulan instance'ları kullanır, yani maliyetli bir çalışma yapmış oluruz.

    Eğer biz hem getirilen verilerin tracking yapılmasını istemiyorsak, hem de yinelenen dataların aynı instance'ları kullanmasını istiyorsak, yani maliyetli işlem yapmaktan kaçınmak istiyorsak **AsNoTrackingWithIdentityResolution()** fonksiyonunu kullanırız.

- `AsTracking():` Change Tracker mekanizmasını manuel olarak yönetmemiz gerekebilir. Yani biz tracking'i kendimiz kapattıktan sonra, uygulama seviyesinde tekrar açabilmek için bu fonksiyonu kullanırız.

- `UseQueryTrackingBehavior():` EF Core seviyesinde veya uygulama seviyesinde ilgili context'ten gelen verilerin üzerinde Change Tracker mekanizmasının davranışını temel seviyede belirlememizi/yönetmemizi sağlar. Yani konfigürasyon fonksiyonudur. Context sınıfı içerisindeki **OnConfiguring** metodu içerisinde bu metodu kullanarak gerekli yönetimleri yapabiliriz. 

<br>

# 21 - İlişkisel Yapılar

- `Principal Entity (Asıl Entity):` Kendi başına var olabilen tabloyu modelleyen entity'e denir. 
 
- `Dependent Entity (Bağımlı Entity):` Kendi başına var olamayan, başka bir tabloya ilişkisel olarak bağımlı olan tabloyu modelleyen entity'e denir.
- `Foreign Key:` **Principal Entity** ile **Dependent Entity** arasındaki ilişkiyi sağlayan key'dir. Dependent Entity'de tanımlanır, Principal Entity'deki Principal Key'i tutar.
- `Principal Key:` Principal Entity'in kimliği olan kolonu ifade eden property'dir.
- `Navigation Property:` İlişkisel tabolar arasındaki fiziksel erişimi class'lar üzerinden sağlayan property'lerdir. Bu property'lerin türleri kesinlikle entity olmak zorundadır.

    Navigation Property'ler tanımlanma şekillerine göre entity'ler arasındaki n-n veya 1-n şeklindeki ilişkileri temsil ederler.
- `İlişki Yapılandırma Yöntemleri:` 
  - `Default Conventions:` Varsayılan entity kurallarını kullanarak uygulanan yöntemdir. Navigation Property'lerini kullanarak ilişkli şablonlarını oluşturma yöntemidir.
  - `Data Annotations Attributes:` Attribute'ları kullanarak bazı yönlendirmeler yapabiliriz. Örnek olarak [Key], [ForeignKey] gibi attribute'ları kullanabiliriz.
  - `Fluent Api:` Entity'lerdeki ilişkileri oluştururken daha detaylı işlemler yapılmasını sağlayan yöntemdir. Bu yöntemde alt kısımdaki fonksiyonları bilmemiz gerekiyor:
    
    - **HasOne():** İligili entity'nin ilişkisel entity'e 1-1 veya 1-n olacak şekilde ilişikisini yapılandırmaya başlayan fonksiyondur. 
    
    - **HasMany():** İlgili entity'nin ilişkisel entity'e n-n veya n-1 olacak şekilde ilişkisini yapılandırmaya başlayan fonksiyondur.
    
    - **WithOne():** **HasOne()** veya **HasMany()** ile başladıktan sonra, ilişkiyi 1 olarak bitirmek için kullanılan fonksiyondur. 
        
        Örnek olarak **HasOne()** ile başlayıp **WithOne()** ile devam edersek 1-1 ilişkiyi oluşturmuş oluruz.

        Eğer **HasMany()** ile başlayıp **WithOne()** ile devam edersek n-1 ilişkiyi oluşturmuş oluruz.
    
    - **WithMany():** **HasOne()** veya **HasMany()** ile başladıktan sonra, ilişkiyi n olarak bitirmek için kullanılan fonksiyondur. 

        Örnek olarak **HasOne()** ile başlayıp **WithMany()** ile devam edersek 1-n ilişkiyi oluşturmuş oluruz.

        Eğer **HasMany()** ile başlayıp **WithMany()** ile devam edersek n-n ilişkiyi oluşturmuş oluruz.

<br>

# 22 - 1-1 İlişki

1-1 İlişki oluşturmanın farklı yöntemleri vardır. Bu yöntemleri alt kısımda inceleyelim:

- `Default Conventions:`

    Her iki entity'de de birbirlerini tekil olarak referans eden **navigation property'ler** olmalıdır.

    1-1 ilişkilerde dependent entity'nin hangisi olduğunu EF Core'un anlaması zordur. Bu nedenle dependent entity içerisinde principal entity'i temsil edecek bir **foreign key** tanımlamamız gerekiyor. Bu yöntemde luzumsuz olarak ekstra bir kolon (foreign key'e karşılık gelen kolon) oluşturmuş oluyor fakat bunu yapmak zorundayız.

    ```cs
    public class Calisan
    {
        public int Id {get; set;}
        public string Adi {get; set;}

        public CalisanAdresi CalisanAdresi {get; set;} //nav prop        
    }

    public class CalisanAdresi
    {
        public int Id {get; set;}
        public string Adres {get; set;}

        public int CalisanId {get; set;} //foreign key
        public Calisan Calisan {get; set;} //nav prop
    }
    ```   

- `Data Annotations:`

    Her iki entity'de de birbirlerini tekil olarak referans eden **navigation property'ler** olmalıdır.

    Foreign kolonunun ismi default convention'ın dışında olacaksa, ForeignKey attribute'u ile bu property'i foreignkey olarak bildirebiliriz.

    Ekstra olarak foreign key kolonu oluşturmadan, tek bir kolonu hem primary hem de foreign key tanımlayabiliriz.

    ```cs
    public class Calisan
    {
        public int Id {get; set;}
        public string Adi {get; set;}

        public CalisanAdresi CalisanAdresi {get; set;} //nav prop        
    }

    public class CalisanAdresi
    {
        public int Id {get; set;}
        public string Adres {get; set;}

        [ForeignKey(nameof(Calisan))]
        public int CalisanId {get; set;} //foreign key
        public Calisan Calisan {get; set;} //nav prop
    }
    /*
     veya
    
    public class CalisanAdresi
    {
        public int Id {get; set;}
        public string Adres {get; set;}

        [ForeignKey(nameof(Calisan))]
        public int X {get; set;} //Data Annotation kullandığımızda artık bu property'e istediğimiz ismi verebiliriz.
        public Calisan Calisan {get; set;} //nav prop
    }
    
    veya
    
    public class CalisanAdresi
    {
        [Key, ForeignKey(nameof(Calisan))] // Hem key hem de foreignkey olarak tanımladığımızda hem 1-1 ilişkiyi garanti altına almış oluruz, hem ekstra olarak bir kolon daha oluşturmamış oluruz hem de ekstra olarak database'de bir index tanımlanmamış olur.
        public int Id {get; set;}
        public string Adres {get; set;}
             .
        public Calisan Calisan {get; set;} //nav prop
    }
    */
    ```

- `Fluent Api:`

    Her iki entity'de de birbirlerini tekil olarak referans eden **navigation property'ler** olmalıdır.

    Bu yöntemde entity'ler arasındaki ilişki **Context** sınıfı içerisinde ki **OnModelCreating()** fonksiyonunu override ederek bu fonksiyonun içerisinde tasarlanır.

    ```cs
    public class Calisan
    {
        public int Id {get; set;}
        public string Adi {get; set;}

        public CalisanAdresi CalisanAdresi {get; set;} //nav prop        
    }

    public class CalisanAdresi
    {
        public int Id {get; set;}
        public string Adres {get; set;}

        public Calisan Calisan {get; set;} //nav prop
    }

    public class MyDbContext : DbContext
    {
        public DbSet<Calisan> Calisanlar {get; set;}
        public DbSet<CalisanAdresi> CalisanAdresleri {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(CfgExtensions.GetConnectionString());
        }

        // Entity'lerin database'de oluşturulacak olan ilişkileri/yapıları bu fonksiyon içerisinde konfigüre edilir.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalisanAdresi>()         
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<Calisan>()
                .HasOne(c => c.CalisanAdresi)
                .WithOne(c => c.Calisan)
                .HasForeignKey<CalisanAdresi>(c => c.Id);
        }
    }
    ```  
<br>

# 23 - 1-n İlişki

1-n İlişki oluşturmanın farklı yöntemleri vardır. Bu yöntemleri alt kısımda inceleyelim:

- `Default Conventions:`

    ForeignKey kolonuna karşılık gelen bir property tanımlamak zorunda değiliz, EF Core kendisi bu kolonu oluşturacaktır. Eğer kendimiz tanımlamak istersekde o propert'i baz alacaktır.

    ```cs
    public class Calisan // Dependent Entity 
    {
        public int Id {get; set;}
        public string Adi {get; set;}
        
        public Departman Departman {get; set;} // nav prop
    }
    /*
    veya

    public class Calisan // Dependent Entity 
    {
        public int Id {get; set;}
        public string Adi {get; set;}
        
        public int DepartmanId {get; set;}
        public Departman Departman {get; set;} // nav prop
    }
    */
    public class Departman
    {
        public int Id {get; set;}
        public string DepartmanAdi {get; set;}

        public ICollection<Calisan> Calisanlar {get; set;} // nav prop
    }
    ```   

- `Data Annotations:`

    Default Convention dışarısında bir foreign key property'si oluşturmak istersek, bu property'i foreign key olarak tanımlamak için annotation kulllanırız.

    ```cs
     public class Calisan // Dependent Entity 
    {
        public int Id {get; set;}
        public string Adi {get; set;}
        
        [ForeignKey(nameof(Departman))]
        public int DId {get; set;}
        public Departman Departman {get; set;} // nav prop
    }

    public class Departman
    {
        public int Id {get; set;}
        public string DepartmanAdi {get; set;}

        public ICollection<Calisan> Calisanlar {get; set;} // nav prop
    }
    ```

- `Fluent Api:`

    ```cs
    public class Calisan // Dependent Entity 
    {
        public int Id {get; set;}
        public string Adi {get; set;}
        
        public Departman Departman {get; set;} // nav prop
    }

    public class Departman
    {
        public int Id {get; set;}
        public string DepartmanAdi {get; set;}

        public ICollection<Calisan> Calisanlar {get; set;} // nav prop
    }

    public class MyDbContext : DbContext
    {
        public DbSet<Calisan> Calisanlar {get; set;}
        public DbSet<Departman> Departmanlar {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(CfgExtensions.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calisan>()
                .HosOne(c => c.Departman)
                .WithMany(d => d.Calisanlar);

            /*   
            Eğer otomatik oluşacak olan foreign key yerine kendimiz bir tanımlama yapmak istiyorsak, 
            dependent class içerisinde tanımlamak istediğimiz foreign key için bir property oluştururz (mesela DId isimli bir property, 
            sonrasında o property'i burada foreign key olarak tanımlayabiliriz.)

            modelBuilder.Entity<Calisan>()
                .HosOne(c => c.Departman)
                .WithMany(d => d.Calisanlar)
                .HasForeignKey(c => c.DId);
            */
        }
    }
    ```  

<br>

# 24 - n-n İlişki

- `Default Conventions:`
 
    İki entity arasındaki ilişki navigation property'leri koleksiyonel olarak tanımlayarak oluşturabiliriz.

    DefaultConvention kullanıldığında cross table'ı manuel oluşturmak zorunda değiliz. EF Core bu tabloyu hazırladığımız entity'lere göre kendisi otomatik olarak oluşturur. Ek olarak da bu cross table'da composite primary key'i de kendisi otomatik olarak oluşturur.

    ```cs
    public class Kitap 
    {
        public int Id {get; set;}
        public string KitapAdi {get; set;}

        public ICollection<Yazar> Yazarlar {get; set;}
        
    }

    public class Yazar
    {
        public int Id {get; set;}
        public string YazarAdi {get; set;}

        public ICollection<Kitap> Kitaplar {get; set;}
    }
    ```   

- `Data Annotations:`

    Cross table'ı manuel olarak kendimiz oluşturmak zorunda kalırız. Bu cross table ile entity'ler arasında 1-n ilişki kurarak, entity'lerin kendi arasında n-n ilişki kurmasını sağlarız.
    
    Cross table'da composite primary key'i annotation'lar ile kuramıyoruz. Bunun için Fluent Api'de çalışma yapmamız gerekiyor.

    Cross table için oluşturulan entity, context sınıfı içerisinde **DbSet** olarak tanımlanmak zorunda değildir.

    Eğer foreign key'leri naming convention'a göre oluşturmazsak, fluent api içerisinde veya data annotation'lar ile bu foreign key'leri de belirtmemiz gerekiyor. Eğer convention'a uymazsak ve tanımladığımız property'nin foreign key olduğunu bildirmezsek, EF Core bu property'i foreign key olarak kullanmayıp kendisi tekrar 2 adet foreign key tanımlaması yapacaktır. 

    ```cs
    public class Kitap 
    {
        public int Id {get; set;}
        public string KitapAdi {get; set;}  

        public ICollection<KitapYazar> Yazarlar {get; set;}                
    }

    public class Yazar
    {
        public int Id {get; set;}
        public string YazarAdi {get; set;}

        public ICollection<KitapYazar> Kitaplar {get; set;}                
    }

    public class KitapYazar
    {        
        public int KitapId {get; set;}
        public int YazarId {get; set;}

        public Kitap Kitap {get; set;}
        public Yazar Yazar {get; set;}
    }

    public class MyDbContext : DbContext
    {
        public DbSet<Yazar> Yazarlar {get; set;}
        public DbSet<Kitap> Kitaplar {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(CfgExtensions.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KitapYazar>()
                .HasKey(ky => new {ky.KitapId, ky.YazarId});
        }
    }
    ```  

- `Fluent Api:`

    Cross table manuel olarak oluşturulmalıdır ve bu table'ın context sınıfı içerisinde **DbSet** olarak tanımlanmasına gerek yoktur.

    Composite primary key tanımlaması **HasKey()** fonksiyonu ile tanımlanmalıdır.

    ```cs
    public class Kitap 
    {
        public int Id {get; set;}
        public string KitapAdi {get; set;}  

        public ICollection<KitapYazar> Yazarlar {get; set;}                
    }

    public class Yazar
    {
        public int Id {get; set;}
        public string YazarAdi {get; set;}

        public ICollection<KitapYazar> Kitaplar {get; set;}                
    }

    public class KitapYazar
    {        
        public int KitapId {get; set;}
        public int YazarId {get; set;}

        public Kitap Kitap {get; set;}
        public Yazar Yazar {get; set;}
    }

    public class MyDbContext : DbContext
    {
        public DbSet<Yazar> Yazarlar {get; set;}
        public DbSet<Kitap> Kitaplar {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(CfgExtensions.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KitapYazar>()
                .HasKey(ky => new {ky.KitapId, ky.YazarId});

            modelBuilder.Entity<KitapYazar>()
                .HasOne(ky => ky.Kitap)
                .WithMany(k => k.Yazarlar)
                .HasForeignKey(ky => ky.KitapId);

            modelBuilder.Entity<KitapYazar>()
                .HasOne(ky => ky.Yazar)
                .WithMany(y => y.Kitaplar)
                .HasForeignKey(ky => ky.YazarId);            
        }
    }
    ```  
<br>

# 25 - İlişkisel Tablolarda Veri Ekleme

- 1-1 ilişkili tablolarda veri eklemek:

    Eğer principal nesne üzerinden ekleme gerçekleştiriliyorsa dependent entity verilmek zorunda değildir. Fakat dependent nesnesi üzerinden ekleme yapılıyorsa, principal nesne verilmek zorundadır.

    EF Core alt kısımdaki 2 yöntemde de ilk önce principal nesneyi ekler, sonrasında dependent nesneyi ekler. 

    ```cs
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Address Address { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string PersonAddress { get; set; }
        
        public Person Person { get; set; }
    }
    ```   

    1. Principal entity üzerinden eklemek:
        ```cs
        public class Program
        {
            public void Main(string[] args)
            {
                Person person = new();
                person.Name = "Ahmet";
                person.Address = new(){ PersonAddress = "Istanbul" };

                await context.AddAsync(person);
                await context.SaveChangesAsync();
            }
        }  
        ``` 

    2. Dependent entity üzerinden eklemek:
        ```cs
        public class Program
        {
            public void Main(string[] args)
            {
                Address address = new()
                {
                    PersonAddress = "Istanbul",
                    Person = new(){ Name = "Ahmet" }
                };

                await context.AddAsync(address);
                await context.SaveChangesAsync();
            }
        }             
        ```

- 1-n ilişkili tablolarda veri eklemek:

    ```cs
    public class Blog
    {
        public Blog()
        {
            // HashSet olarak new'lenmesi performans içindir. 
            // Herhangi bir koleksiyon tipi de tercih edilebilir.
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }

        public Blog Blog { get; set; }
    }
    ```

    1. Principal entity ile
        - nesne referansı üzerinden eklemek:            
            ```cs           
            public class Program
            {
                public void Main(string[] args)
                {
                    Blog blog = new() { Name = "Ahmet's Blog" };

                    // Bu şekilde ekleme yaparken, Posts'un kesinlikle null olmadığından emin olmalıyız. 
                    // Emin olmak için de ilgili class'ın ctor'unda ilgili property new'lenir (Blog class'ının ctor'u).
                    blog.Posts.Add(new() { Title = "Post 1" });
                    blog.Posts.Add(new() { Title = "Post 2" });
                    blog.Posts.Add(new() { Title = "Post 3" });

                    await context.AddAsync(blog);
                    await context.SaveChangesAsync();
                }
            }                       
            ```

        - object initializer üzerinden eklemek:
            ```cs
            public class Program
            {
                public void Main(string[] args)
                {   
                    // Bu şekilde ekleme yapılacağında, ctor içerisinde new'leme yapılmasına gerek kalmaz.
                    Blog blog = new()
                    {
                        Name = "Blog 1",
                        Posts = new HashSet<Post>()
                        {
                            new() { Title = "Post 10" },
                            new() { Title = "Post 11" }
                        }
                    };

                    await context.AddAsync(blog);
                    await context.SaveChangesAsync();
                }
            }   
            ``` 
    
    2. Dependent entity ile eklemek:
        ```cs
        public class Program
        {
            public void Main(string[] args)
            {   
                // Bu şekilde de (dependent'tan başlayarak) ekleme yapabiliriz fakat burada 1-n ilişki olmasına rağman 1-1 ilişki gibi ekleme yapılır, yani sadece 1'er adet veri girişi sağlanabilir.
                Post post = new()
                {
                    Title = "Post 20",
                    Blog = new() { Name = "N Blog" }
                };

                await context.AddAsync(post);
                await context.SaveChangesAsync();
            }
        }          
        ``` 

    3. Foreign Key kolonu ile eklemek:
        ```cs
        public class Program
        {
            public void Main(string[] args)
            {   
                Post post = new()
                {
                    Title = "Post 42",
                    BlogId = 1 // Var olan principal verisinin id'sini belirterek, var olan bir principal veriye dependent veri ekleyebiliriz.
                };

                await context.AddAsync(post);
                await context.SaveChangesAsync();
            }
        }   
        ``` 

- n-n ilişkili tablolarda veri eklemek:   

    1. Default convention ile tasarlanan ilişkilerde kullanılır:
        ```cs
        public class Book
        {
            public Book()
            {
                Authors = new HashSet<Author>();
            }

            public int Id { get; set; }
            public string BookName { get; set; }

            public ICollection<Author> Authors { get; set; }
        }

        public class Author
        {
            public Author()
            {
                Books = new HashSet<Book>();
            }

            public int Id { get; set; }
            public string AuthorName { get; set; }

            public ICollection<Book> Books { get; set; }
        }

        public class Program
        {
            public void Main(string[] args)
            {   
                Book book = new()
                {
                    BookName = "A Kitabı",
                    Authors = new HashSet<Author>()
                    {
                        new (){ AuthorName = "Ali" },
                        new (){ AuthorName = "Veli" }                        
                    }
                };

                await context.Books.AddAsync(book);
                await context.Books.SaveChangesAsync();
            }
        }   
        ```

    2. Fluent Api ile tasarlanan ilişkilerde kullanılır:

        ```cs
        public class Book
        {
            public Book()
            {
                Authors = new HashSet<AuthorBook>();
            }

            public int Id { get; set; }
            public string BookName { get; set; }

            public ICollection<AuthorBook> Authors { get; set; }
        }

        public class Author
        {
            public Author()
            {
                Books = new HashSet<AuthorBook>();
            }

            public int Id { get; set; }
            public string AuthorName { get; set; }

            public ICollection<AuthorBook> Books { get; set; }
        }

        public class AuthorBook
        {
            public int BookId { get; set; }
            public int AuthorId { get; set; }

            public Book Book { get; set; }
            public Author Author { get; set; }
        }

        // Context sınıfının hazır olduğunu düşünelim.

        public class Program
        {
            public void Main(string[] args)
            {   
                Author author = new()
                {
                    AuthorName = "Can",
                    Books = new HashSet<AuthorBook>()
                    {
                        new() { BookId = 1 }, // var olan bir kitap için yeni bir yazar ve cross table'a ilişki eklemek.
                        new() 
                        { 
                            Book = new () { BookName = "B Kitap" } // hem yeni kitabı, hem yeni yazarı hem de cross table'a ilişkiyi eklemek.
                        }
                    } 
                };

                await context.Books.AddAsync(book);
                await context.Books.SaveChangesAsync();
            }
        }   
        ```

<br>

# 26 - Entity'lerde İlişkilerin Güncellenmesi



