Kaynak: https://medium.com/@cmdgn

# Index Nedir
- Index'ler db içerisindeki tablolarda tanımlanan ve aranan veriye daha hızlı erişim sağlayan db nesneleridir.

- Verileri sıralı tutarak bulunmasını kolaylaştırır.

     Örnek olarak bir excel tablosunda 100 kişilik bir isim listesi olsun. Bu liste içerisinde **Okan** ismini bulmamız gerekiyor olsun. Eğer liste sıralı değilse 100 isim içerisinde tek tek bütün isimlere bakarak **Okan** ismini bulmamız gerekecek. Eğer listeyi sıralarsak aradığımız isme ne kadar yakın veya uzak olduğumuzu ölçebiliriz ve ona göre arama şeklimizi yönlendiririz.

- SQL Server'da bir tabloya Index tanımlarsak, o tabloda bulunan veriler **tree** yapısında tutulacak şekilde düzenlenir-organize edilir.

    Veriyi bütün veri kümesi içinde tek tek aramak, **tree** yapısında olan bir veri kümesine göre çok daha uzun sürecektir. 

<br>

# Clustered Table
- Bir tablo üzerinde Clustered Index tanımlı bir kolon varsa, tablo da **Clustered Table** olarak adlandırılır. 

<br>

# Index Çeşitleri
- Temel olarak 2 çeşit Index tipi vardır. **Clustered** ve **Non-clustered**. Eğer tree yapısının en alt node'unda (leaf node) tutulan şey verinin kendisi ise ***Clustered Index*** olur. Eğer Leaf node verinin kendisini değil de, verinin nerede olduğunun bilgisini tutuyorsa ***Non-clustered Index*** olur.

    - # Clustered Index
        - Clustered Index'ler tablodaki verileri fiziksel olarak sıralar.
        - Bir tablo üzerinde sadece 1 ader Clustered Index tanımlanabilir.
        - Tablo içerisinde en çok sorgu gelen kolon'a Clustered Index atamalıyız.
        - Index'leme yapılmış bir kolonda değişiklik olduğunda bütün verilerin tekrar sıralanması gerekeceğinden, Index ataması yapılan kolonun olabildiğince az değişmesi performans açısından önemlidir.

    <br>

    - # Non-clustered Index
        - Non-clustered Index'ler verileri fiziksel olarak değil, mantıksal olarak sıralarlar.
        - Bir tablo üzerinde en fazla 999 adet Non-clustered Index tanımlanabilir.
        - Veriye doğrudan erişemezler, Heap veya bir Clustered Index üzerinden erişebilirler.

    <br>

    - Index'leme yapılan tablolarda INSERT, DELETE, UPDATE işlemlerinden sonra verilerin tekrar düzenlenmesi gerekeceğinden maliyet artışına neden olur. Bu nedenle Index atamalarını dikkatli ve düşünerek yapmalıyız.

<br>

# Unique Index
- Bir kolondaki verilerin tekrarını engellemek istiyorsak ve bu kolondaki verilere hızlı ulaşmak istiyorsak kullanılır.
- Primary Key tanımladığımızda veya Unique Constraint tanımladığımızda otomatik olarak Unique Index tanımlanmış olur.
- Unique Index tanımlanan kolona sadece bir kere ***null*** değer eklenebilir.
-Hem Clustered hem de Non-clustered Index'ler Unique Index olarak tanımlanabilir.

<br>

# Filtered Index
- Tüm tabloye Index tanımlamak yerine, belirli koşullara uyan veriye Index tanımlaması yapılır.
- Maliyeti düşüktür ve performansı arttırır.
- Normal bir Non-clustered Index'e göre daha az yer kaplar.

<br>

# Composite Index
- Tabloda tanımlanan Index bir kolon üzerinden değil de, birden fazla kolon üzerinden tanımlandıysa ***Composite Index*** denir.
- Hem Clustered hem de Non-clustered Index'ler Composite Index olarak tanımlanabilir.
- Bu Index tipinde tanımlı kolonların yazılma sıraları önemlidir.
- Performansı arttırabilmek için tekil veri sayısı çok olan, veri çeşitliliği fazla olan kolon başa yazılmalıdır.

<br>

# Covered Index
- 

<br>

# Column Store Index
- Yukarıdaki Index türleri satır bazlı tutuluyordu. Bu Index ise kolon bazlı tutulur.
- Depolama maliyeti diğer Index türlerine göre daha azdır.
- Veri yazmanın az, okumanın çok olduğu sistemlerde ve karmaşık filtreleme ve gruplama yapılan, büyük veri grupları içerisinde kullanılır.

<br>

# Full-text Index
- Özellikle büyük boyutlu veri içeren alanlarda hızlı aramalar yapabilmek için kullanılır.
- SQL Server'in sunduğu bir servistir.