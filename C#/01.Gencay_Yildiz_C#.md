# İçindekiler

- [Açıklama](#açıklama)
- [Genel Kısa Notlar](#genel-kısa-notlar)
- [Tip Dönüşümleri](#tip-dönüşümleri)
- [Bilinçli ve Bilinçsiz Tip Dönüşümleri](#bilinçli-ve-bilinçsiz-tip-dönüşümleri)
- [Operatörler](#operatörler)
- [Switch Case](#switch-case)
- [İf Blokları](#if-blokları)
- [For Döngüsü](#for-döngüsü)
- [Diziler](#diziler)
- [Array Sınıfı Metotları](#array-sınıfı-metotları)
- [Class Yapısı](#class-yapısı)
- [Metotlar](#metotlar)
- [Nesne Oluşturma](#nesne-oluşturma)
- [Nesne Üzerinden Metotlara Erişim](#nesne-üzerinden-metotlara-erişim)
- [Metot Overload](#metot-overload)
- [Property ve Field](#property-ve-field)
- [Constructor](#constructor)
- [Destructor ve GC](#destructor-ve-gc)
- [This Keyword](#this-keyword)
- [ArrayList ve List](#arraylist-ve-list)
- [Kalıtım](#kalıtım)


<br>

# Açıklama

Gençay Yıldız youtube kanalında bulunan 40 derslik C# eğitimini takip ederken çıkardığım notlar alt kısımdadır. 

Eğitimde verilen her bilgiyi, her komutun nasıl çalıştığını veya ne demek olduğunu not almadım,sadece küçük trick olarak gördüğüm yerleri not aldım. Dersleri takip ederek daha temel bilgileri ve daha farklı bilgileri de bulabilirsiniz.

İyi çalışmalar.

<br>

# Genel Kısa Notlar

- `;` işaretinin amacı satır sonunu belirtmek değildir. Yazılan komutun sonunu belirtmektir.

- Değişken isimleri sayı ile başlamaz.

- `string` ifadeler `" "` ile, `char` ifadeler `' '` ile tanımlanır.

- Metotlar içerisinde tanımlanan değişkenlere varsayılan değerleri atanmalıdır. Sınıflar içerisinde tanımlanan değişkenlere varsayılan değerleri otomatik olarak atandığı için bizim atama yapmamıza gerek yoktur.

- `var` değişken tipinin amacı farklı diller arasında tip farklılıkları olabileceğinden, diller arasında ortak tip olarak kullanılmasıdır.

    Ayrıca `var` tipinin dezavantaj olarak söylenebilecek bir özelliği vardır. Program içerisinde kullandığımız değişkenler RAM içerisinde oluşturulurken, `var` ile tanımlanan değişkenin tipini bulunması gerekecek. Eğer geliştirici değişkeni tanımlarken tipini belirterek tanımlarsa performansı arttıracak şekilde geliştirme yapmış olur.

- `region` yapısı, yazdığımız kodları bloklar haline getirmemizi sağlar. Derlenmez, sadece geliştirici için kolaylık sağlamak amacı ile kullanılır.

    ```csharp
        # region Ornek
            .
            .
            .
        #
    ```

- Değişken tiplerini 2 gruba ayırabiliriz:

    | Stack - Değer Tipli | Heap - Referans Tipli|
    | --- | --- |
    |int|string|
    |bool|class|
    |float|interface|
    |.|.|
    |.|.|

- `object` tipi diğer değişken tiplerinin neredeyse tamamını kapsayan tiptir. Bilmemiz gereken 2 özelliği vardır:

    1. Boxing

        Object tipli bir değişkene değer atarsak oluşan durumdur. Örnek olarak: 

        ```csharp
            object eleman = "Ahmet";
        ```

        Burada **eleman** değişkeninin tipine baktığımızda `string` değil `Object` olduğunu görürüz. Bu duruma `Boxing` denmektedir.

    2. Unboxing

        Boxing yapılmış bir değişkeni kendi tipini ile dışarıya çıkartmaya denir. Örnek olarak:

        ```csharp
            string x = (string)eleman;
        ```

        Burada **eleman** değişkeninin içerdiği veriye göre tip dönüşümü yapılır.

<br>

# Tip Dönüşümleri

- Tip çevirme yapılırken kullanılan bazı yöntemler:

    ```csharp
        Convert.ToInt32(x);
    ```
   
    ```csharp
        int(x);
    ```

    `string` tipleri dönüştürürken özel olarak kullanılabilecek bir de `Parse()` yöntemi vardır.

    ```csharp
        int x = int.Parse("112");
    ```

- Tip dönüşümleri yaparken derleyici hatası almasak bile çalışma zamanı `runtime` hataları alabiliriz. Örnek olarak:

    ```csharp
        string x = "123ab";
        int y = Convert.ToInt32(x);
    ```

    Burada `x` içerisinde bulunan metinsel ifadede bulunan harfler sayısal değere dönüştürülemediği için derleme aşamasında hata almıyoruz ama program çalışma zamanında hata fırlatıyor.

- `char` ve `int` tipleri arasında `ASCII` ilişkisi vardır. `char` tipli bir değişkenin sayısal karşılığını bulmak istersek:

    ```csharp
        char x = 'a';
        int y = Convert.ToInt32(x);

        Console.WriteLine(y); 
        //output: 97
    ```

    Örnekten de görüldüğü üzere `a` karakterinin `ASCII` tablosundaki sayısal karşılığının `97` olduğunu öğreniyoruz.

    Ayrıca yukarıdaki örneği tersten giderek yapmak ta mümkün. Yani sayısal bir değerin `ASCII` tablosundaki metinsel karşılığını bulabiliriz. Örnek olarak:

    ```csharp
        int x = 97;
        char y = Convert.ToChar(x);

        Console.WriteLine(y)
        //output: a
    ```

- Tip dönüşümü yaparken `()` kullanarak `casting` işlemi yapılabilir. Örnek olarak:

    ```csharp
        int x = (int)'a';
        char y = (char)97;
    ```
    
- `int` tipinde olan `1` değerini `bool` tipine dönüştürürsek `true` elde ederiz. Eğer `0` değerini dönüştürürsek `false` elde ederiz.

<br>

# Bilinçli ve Bilinçsiz Tip Dönüşümleri

- Değer aralığı küçük olan tipi, değer aralığı büyük olan tipe dönüştürmek istiyorsak `Bilinçsiz Dönüşüm` yapılır.

- Eğer değer aralığı büyük olan tipi, değer aralığı küçük olan tipe dönüştürmek istiyorsak `Bilinçli Dönüşüm` yapılır. Bu dönüşümlerde dikkat edilmezse veri kaybı yaşanabilir.

- `Bilinçsiz Dönüşüm` yapılırken derleyici hata vermeden kendisi dönüşümü otomatik olarak yapar. 
    
    `Bilinçli dönüşüm` yapılırken ise derleyici işlemi kendisi yapamaz, hata verir, bu nedenle geliştirici gerekli yöntemleri kullanarak dönüşümü kendisi yapar (Örnek olarak `Convert` metotları kullanılabilir).

- Tip dönüşümlerinde olabilecek veri kayıplarını kontrol altına alabilmek için kullanılan bir yapı vardır: `checked-unchecked`. Örnek kullanımları:

    ```csharp
        checked
        {
            int x = 1255;
            byte y = (byte)x;
            Console.WriteLine(y);
        }
    ```

    `checked` içerisine yazılan kodlarda eğer veri kaybı yaşanırsa program hata fırlatır. Veri kaybı yaşanmıyor ise kodlar normal şekilde çalışmaya devam eder.

    ```csharp
        unchecked
        {
            int x = 1255;
            byte y = (byte)x;
            Console.WriteLine(y);
        }
    ```

    `unchecked` içerisine yazılan kodlar ise veri kaybı olsa bile normal bir şekilde çalıştırılır. Aynı kod üst kısımda hata fırlatacak iken alt kısımdaki kod herhangi bir hata vermeyecektir.
    
     Eğer yazdığımız kodları `checked veya unchecked` bloklarının içerisine yazmasak default olarak `unchecked` içerisinde gibi çalışır.

<br>

# Operatörler

- Temel dört işlem operatörleri işlem sonucunu işlemde kullanılan en büyük değer aralıklı tipe çevirerek sonuç döndürür. Örnek olarak:

    ```csharp
        int x = 5;
        double y = 10;
        var sonuc = x + y;

        Console.WriteLine(sonuc.GetType());
        //output: double
    ```

- `++` ve `--` operatörleri kullanılırken kullanım şekline göre farklılık olacağını unutmamalıyız. Örnek olarak:

    ```csharp
        int x = 10;
        Console.WriteLine(++x);
        //output: 11
        //x önce arttırıldı sonra ekrana yazıldı.

        int y = 10;
        Console.WriteLine(y++);
        //output: 10
        //y önce ekrana yazıldı sonra arttırıldı.
    ```

- Karşılaştırma operatörleri `(>,>=,<,<=,==,!=)` geriya `bool` tipinde sonuç döndürürler.

<br>

# Switch Case

- Switch Case yapısında `goto` kullanımına örnek:

    ```csharp
        int x = 10;
        switch(x)
        {
            case 3:
                Console.WriteLine("C3");
                break;

            case 8:
                Console.WriteLine("C8");
                break;
            
            case 10: goto case 3;
        }
        //output: C3
    ```

    Bu örnekte eğer program `case 10` durumuna girerse `case 3`durumuna gidilecek ve `case 3` içerisinde yer alan işlemler gerçekleştirilecek.

<br>

# İf Blokları

- Koşulun sağlanması durumda tek satırlık işlem yapılacaksa, scope kullanmaya gerek yoktur. Eğer koşul durumu sağlandığında birden fazla satır yazılacaksa o zaman scope kullanılması gerekmektedir. Örnek olarak:

    ```csharp
        bool x = true;

        if(x)
            Console.WriteLine("İşlem başarılı.");

        .
        .
        .
        .
    ```
<br>

# For Döngüsü

- `for` döngülerinde sonsuz döngü oluşturmak için 2 adet örnek:

    ```csharp
        for(int i=0;true;i++)
        {
            .
            .
            .
        }

        //veya

        for(;;)
        {
            .
            .
            .
        }
    ```
<br>

# Diziler

-  Diziler ile çalışırken eleman sayıları verilmek zorundadır. Bu gereklilik bir sınırlılık oluşturur.

    Örnek olarak 10 elemanlı bir dizi tanımlanırsa, bellekte 10 hücrelik alan hazırlanıyor diyelim. Eğer biz program içerisinde bu dizinin 10 elemanını doldurmak yerine sadece 5 eleman ekleyip geriye kalan kısmı boş bırakırsak, bellekteki 5 hücreyi boşuna işgal ediyor olacağız. Yani gereksiz bellek kullanımında neden olacağız.

- Bir dizi oluşturulduktan sonra, yukarıdaki şekilde değer atanmayan alanlara otomatik olarak dizinin tipine göre default değerler atanır. Örnek olarak elimizde `int` tipinde bir dizimiz olsun. Bu dizide eleman ataması yapılmayan yerlere, otomatik olarak `0` atanacaktır.

<br>

# Array Sınıfı Metotları

- **IsFixedSize:** Dizinin eleman sayısı sabit ise `true`, eleman sayısı değişkene bağlı ise `false` döner.

- **IsReadOnly:** Dizinin elemanları sadece okunabilir durumda ise `true`, hem okunabilir hem de yazılabilir durumda ise `false` döner.

- **Length:** Dizinin eleman sayısını döndürür.

- **Clear:** Dizideki elemanları default değerlerine çevirir.

- **Copy:** Dizinin istenilen eleman aralığını başka bir diziye kopyalar.

- **IndexOf:** Dizi içerisinde harf veya kelime, eleman aramayı sağlar. Eğer bulursa `bulunan ilk indexi` döndürür, eğer bulamazsa `-1` döndürür.

- **Reverse:** Diziyi ters çevirir.

- **Sort:** Dizinin elemanlarını sıralar.

<br>

# Class Yapısı

- Bir class içerisinde başka bir class olabilir. Örnek olarak:

    ```csharp
        class Ornek
        {
            class Ornek2
            {

            }
        }
    ```

- Class içerisinde `class, metot, constructor, global değişken ve referanslar, propertyler` tanımlanabilir.

- Algoritmik kodlar class içerisine direkt yazılmaz. Bu tür kodlar metotların veya propların içerisine yazılır.

<br>

# Metotlar

- Metotlar sınıflar içerisinde tanımlanır, farklı metotlar veya kendileri içerisinde kullanılır. Metotlar birer işlem parçacıklarıdır.

- Static metotlar-yapılar, static metotları-yapıları kullanır.

<br>

# Nesne Oluşturma

- Class'lar nesneler türetmemizi sağlayan yapılardır.

- Class'lar da int gibi, string gibi birer tiptir.

- Oluşturulan nesneler `Heap`'te tutulur. Değerler, referanslar ve değişken isimleri `Stack`'te tutulur.

- Referanssız oluşturulan nesnelere doğrudan erişim mümkün değildir. Heap içerisinde nesne oluşturulur ama erişilemez. 

    ```csharp
        new Ornek();
    ```

    Böyle durumlarda yani referansı olmayan nesneler oluştuğunda, `GC` belirli sıklıkla temizlik yaparak, referansı olmayan nesneleri siler.

- Referans tipler arasında atama işlemi yapılırsa, adres ataması yapılır. Referans tiplerde işaretleme vardır. Örnek olarak:

    ```csharp
        Ornek x = new Ornek();
        Ornek y = x;
    ```

    ![stackheap1](https://user-images.githubusercontent.com/44196434/178970970-bee33126-9ff2-41be-b2e0-75eb87e1f84a.png)

- Değer tiplerde durum farklıdır. Değer tiplerde işaretleme yerine, değerin kopyalanması vardır. Örnek olarak:

    ```csharp
        int x = 3;
        int y = x;
    ```
    ![stackheap2](https://user-images.githubusercontent.com/44196434/178971047-d295b226-efe0-4cd4-96be-c0b26b7d8be0.png)

<br>

# Nesne Üzerinden Metotlara Erişim

- Sınıfların içerisinde bulunan metotların işlem yapabilmesi için nesneler gereklidir.

    Örnek olarak `Matematik` sınıfı ile referans oluşturulup ona nesne atanmazsa, yani:

    ```csharp
        Matematik mat1 = null;
    ```

    Bu durumda oluşturulan `mat1` referansı ile `Matematik` sınıfı içerisindeki metotlar görüntülenebilir fakat çalıştırılamaz. Metotlara erişmek isteyin program hata fırlatacaktır.

    Sonuç olarak `Matematik` sınıfı içerisindeki metotlara erişip onları çalıştırmak istiyorsak `mat1` referansına nesne ataması yapılmalıdır. Örnek olarak:

    ```csharp
        Matematik mat1 = new Matematik();
        int sonuc = mat1.Topla(5,3);

        Console.WriteLine(sonuc);
        //output: 8
    ```
<br>

# Metot Overload

- Metot isimleri aynı olmalıdır ama parametre durumları veya geri döndürülen değerin tipi farklı olmak zorundadır.

<br>

# Property ve Field

- Class içerisinde tanımlanan değişkenlere `Field` denir. Örnek olarak:

    ```csharp
        class Ornek
        {
            string adi;
            .
            .
            .
        }
    ```
- Class içerisinde olan `field`'ları dışarıya kontrollü bir şekilde açmak istiyorsak `property` yapılarını kullanırız. Örnek olarak:

    ```csharp
        class Ornek
        {
            public string Adi
            {
                get
                {
                    return adi;
                }

                set
                {
                    adi = value;
                }
            }

            //veya

            public string Adi {get; set;}
        }
    ```
- Prop'lara basit bir şekilde başlangıç değeri atanması durumuna `Auto Property Initializer` denir. Ornek olarak:

    ```csharp
        class Ornek
        {
            public string Adi { get; set; } = "Ahmet";
        }
    ```

<br>

# Constructor

- Bir nesne türetilirken her şeyden önce tetiklenen metottur.

- `Static Constructor` normal ctor'dan da önce tetiklenir. Normalinden farklı olarak
sadece ilgili sınıfa gelen ilk nesne türetme isteğinde tetiklenir. Sonraki isteklerde tetiklenmez.

<br>

# Destructor ve GC

- `Destructor` nesne bellekten silinmeden hemen önce yapılacak son işlemleri içeren yapıdır. En son tetiklenir ve sonrasında nesne silinir.

    ```csharp
        ~Ornek()
        {

        }
    ```
- `GC`'nin görevi temizlik yapmaktır. Örnek olarak yukarıda bahsedilen gibi Heap içerisinde bulunan referanssız nesneleri bulup tamamen siler. 

    ```csharp
        GC.Collect();
    ```

<br>

# This Keyword

- Bir class içerisinde oluşturulan ifadeyi, aynı sınıf içerisinde belirtirken kullanılır.

<br>

# ArrayList ve List

- `ArrayList` yapısının dezavantajı olarak tip güvenliği söylenebilir. Bir `ArrayList` içerisine farklı tiplerden elemanlar eklenebilir.

- `ArrayList` yapısındaki bu dezavantajı kapatmak amacı ile geliştirilen yapı ise `List<>` yapısıdır. Bu yapı generic olduğundan tip güvenliği sağlanarak işlemler gerçekleştirilir.

<br>

# Kalıtım

- `Internal` erişim belirleyicisi ile tanımlanan ifadeler sadece ait oldukları `solution` içerisinde erişilebilir durumdalardır. Diğer `solutionlar` içerisinden erişim olmaz.

- Bir sınıf sadece bir sınıftan kalıtım alabilir ama bir sınıf birden falza sınıfa kalıtım verebilir.

- Hiyerarşik olarak kalıtım alınan yapılarda, en altta olan sınıftan nesne türetme isteği geldiği zaman, en üstteki ata sınıftan başlanarak istenen sınıfa gelene kadar bütün sınıflardan nesne oluşturulur. Örnek olarak: 

    ![nesnetalebi](https://user-images.githubusercontent.com/44196434/178977373-6b2d6e21-cd58-4ec0-9006-0bc37cf4dbcb.png)

<br>

# Override ve Virtual

- Başka sınıflara kalıtım veren bir sınıfta override edilebilir olarak işaretlenmek istenen ifadeler `virtual` olarak işaretlenir.

    Kalıtım alan bir sınıf içerisinde `override` yazıp, bir boşluk bırakırsak base sınıf içerisinde `virtual` olarak işaretlenen ifadeler bizlere seçenek olarak çıkmaktadır.

<br>

# Abstract Class

- `Absrtact` sınıf içerisinde `abstract` olarak işaretlenen metotlar veya proplar, bu sınıftan kalıtım alan her sınıfta kullanılmak zorundadır (Boş olabilir, çağrılması yeterlidir.). 

- `Abstract` metotların-propların gövdeleri tanımlandıkları sınıfın içerisinde yazılmazlar. Tanımlanan sınıfın içerisinde sadece metot imzaları atılır. Metotların gövdeleri kalıtımı alan sınıfın içerisinde yazılır (Override edilerek.).

- `Abstract` olarak işaretlenen ifadeler `private` olamaz.

- Eğer class içerisinde `abstract` ifade varsa, class ta `abstract` olmalıdır.

- `Abstract` sınıflardan nesne türetilemez. Referans noktası alınabilir.

<br>

# Interface

- Nesne oluşturulamaz ama referans noktası alınabilir.

- Bir sınıf birden fazla `Interface`'den kalıtım alabilir.

- İçerisinde sadece imzalar bulunabilir. Ayrıca imzaların erişim belirleyicileri bulunmaz.

- Bir sınıfın kalıtım aldığı birden fazla interface'lerde, aynı imzaya sahip metotlar olduğunda bir çakışma meydana gelir. Bu duruma `Name Hiding` olarak geçmektedir. Bu durumu çözmek için uygulanan explicit çözümü örneği olarak:

    ```csharp
        public interface IA
        {
            int x();
            int y();
        }

        public interface IB
        {
            int x();
            int z();
        }

        public class Ornek : IA, IB
        {
            public int IA.x()
            {
                .
                .
            }

            public int IB.x()
            {
                .
                .
            }
        }
    ```

