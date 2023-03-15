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


