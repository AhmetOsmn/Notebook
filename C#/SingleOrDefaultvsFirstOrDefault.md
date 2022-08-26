# Single & SingleOrDefault
- Eger yapilan sorguda sadece bir veri gelmesini istiyorsak bu fonksiyonlar kullanilabilir.
- ***Single:*** Geriye sadece tek bir veri dondurur. Eger sorgu sonucunda birden fazla veri geliyorsa veya hic veri gelmiyorsa, exception firlatir. 
- ***SingleOrDefault:*** Geriye sadece tek bir veri dondurur. Eger hic veri gelmiyorsa, veri tipine uygun olan ***Default*** degeri geri dondurur. Sorgu sonucunda birden fazla veri geliyorsa exception firlatir.
- ***First***  fonksiyonlarindan farki su sekildedir: Biz bazi verilerin veri setimizde kesinlikle tek olmasini isteriz.

    Ornek olarak veri tabaninda *Kullanici* tablosunun oldugunu dusunelim. Bu tablo icerisinde bir mail'in birden fazla olmamasi gerekir.Yani Ahmet'in mail'i ile tablodaki baska birinin mail'i ayni olamaz. Bu tip durumlari kontrol altina almak istedigimiz zaman ***Single & SingleOrDefault*** fonksiyonlarini kullaniriz.
    
- C# Kullanimina bir ornek ve TSQL karsiligi su sekildedir:

    ```csharp
    // C#
    var result = db.Customers.SingleOrDefault(customer => customer.ID == 22);
    ```
    
    ```sql
    -- TSQL
    select * from Customers where CustomerID = 22
    ``` 

<br>

# First & FirstOrDefault
- Eger yapilan sorguda sadece bir veri gelmesini istiyorsak bu fonksiyonlar kullanilabilir
- ***First:*** Yapilan sorgudan gelen verilerden ilk veriyi getirir. Eger hic veri gelmiyor ise exception firlatir.
- ***FirstOrDefault:*** Yapilan sorgudan gelen verilerden ilk veriyi getirir. Eger hic veri gelmiyor ise, istenen verinin tipine uygun olan ***Default*** degeri geri dondurur.
- ***Single*** fonksiyonlarindan farki su sekildedir:
- C# Kullanimina bir ornek ve TSQL karsiligi su sekildedir: Getirmek istedigimiz verinin veri seti icerisinde tek olmasi veya birden fazla olmasinin bir onemi yoktur. 

    Ornek olarak veri tabaninda *Kullanici* tablosunun oldugunu dusunelim. Bu tablo icerisinde 'Ahmet' adinda birden fazla kullanici olabilir. Hedefimiz ismi Ahmet olan kullanicilardan ilk kisiyi getirmek gibi bir durum ise ***First & FirstOrDefault*** fonksiyonlarini kullaniriz.
  
- Iki metotda TSQL tarafında su sekilde bir kod olusturur: 
 
    ```csharp
    // C#
    var result = db.Customers.FirstOrDefault(customer => customer.ID == 22);
    ```
    
    ```sql
    -- TSQL
    select top 1 * from Customers where CustomerID = 22
    ``` 
