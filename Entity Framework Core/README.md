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

