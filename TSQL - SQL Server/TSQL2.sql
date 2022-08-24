-- String Fonksiyonlari
select left(FirstName, 2) from Employees -- personellerin isimlerinin soldan baslayarak 2 karakterlerini getirir.
select right(FirstName, 2) from Employees -- personellerin isimlerinin soldan baslayarak 2 karakterlerini getirir.
select upper(FirstName) from Employees -- personellerin isimlerini buyuk harflere cevirerek getirir.
select lower(FirstName) from Employees -- personellerin isimlerini kucuk harflere cevirerek getirir.
select substring(FirstName,1,4) from Employees -- personellerin isimlerinin 1. indexten baslayarak 4 karakterini getirir.
select ltrim('         Ahmet') -- soldan bosluklari keser.
select rtrim('Ahmet        ') -- sagdan bosluklari keser.
select reverse('ahmet') -- verilen degeri tersine cevirir: temha.
select replace('ahmet osman','osman','sezgin') -- verilen degeri, verilen 2. deger ile degistirir.
select charindex('t','ahmet osman sezgin') -- arnan karakterin, verilen ifadedeki yerini getirir (saymaya 1den baslar): 5.
select FirstName, charindex('a',FirstName) from Employees -- calisanlarin isimlerinde 'a' karakterinin yerini getirir.
select ContactName from Customers
select substring(ContactName,1,charindex(' ', ContactName))  from Customers -- sirket temsilcilerinin sadece isimlerini getirir.
select substring(ContactName, charindex(' ', ContactName), len(ContactName) - (charindex(' ', ContactName) - 1)) from Customers -- sirket temsilcilerinin sadece soyisimlerini getirir.

-- Sayisal deger islemleri
select 5+5
select 5*5
select 5-5
select 5/5

select pi() -- sayisal islemlerde kullanmak icin pi sayisini getirir
select sin(90) -- icerisine girilen degerin sin degerini getirir
select power(2,3) -- verilen degerin, verilen ussunu alir: 2 uzeri 3
select abs(-158) -- verilen degerin mutlak degerini alir
select rand() -- her cagirildiginda 0-1 araliginda rastgele bir sayi uretir
select floor(12.9) -- kusuratli sayilari tam sayilara yuvarlar: 12.9 -> 12

-- Tarih fonksiyonlari
select GETDATE() -- guncel olarak tarihi ve saati getirir
select DATEADD(DAY, 999, GETDATE()) -- verilen tarihe 999 gun ekler olusan tarihi getirir
select DATEADD(MONTH, 2, GETDATE()) -- verilen tarihe 2 ay ekler ve olusan tarihi getirir
select DATEADD(YEAR, 10, GETDATE()) -- verilen tarihe 10 yil ekler ve olusan tarihi getirir

select DATEDIFF(DAY, '2022-08-10 17:30:00', GETDATE()) -- iki tarih arasindaki gun farkini getirir
select DATEDIFF(MONTH, '2016-09-15 17:30:00', GETDATE()) -- iki tarih arasindaki ay farkini getirir
select DATEDIFF(YEAR, '2016-09-15 17:30:00', GETDATE()) -- iki tarih arasindaki yil farkini getirir
 
select DATEPART(DW, GETDATE()) -- verilen tarihteki gunun, haftanin kacinci gunu oldugunu verir
select DATEPART(MONTH, GETDATE()) -- verilen tarihteki ayin, yilin kacinci ayi oldugunu verir
select DATEPART(DAY, GETDATE()) --verilen tarihteki gunun, ayin kacinci gunu oldugunu verir
 
 -- Top komutu
select top 2 * from Employees -- personeller tablosundaki ilk 2 personeli getirir

-- Distinct komutu
-- Ornek olarak personeller tablosunda 4 adet London bulunuyorken, distinct kullandigimizda London sadece bir kez gelecektir.
select distinct City from Employees -- personeller tablosunda olan sehirleri birerkez olacak sekilde getirir.

-- Group By islemi
-- eger select sorgusunda bir adet normal sutun ve bir aggregate fonksiyon cagiriliyorsa, sutunun gruplanmasi gerekecektir.
select CategoryID, COUNT(*) from Products group by CategoryID -- urunler tablosunda CategoryID'leri gruplayarak hangi ID'den kacar tane var onu getirir.
select CategoryID, COUNT(*) from Products where CategoryID > 3 group by CategoryID -- ustteki sorgunun -where- ile sartlandirilmis hali.
select EmployeeID, COUNT(*) from  Orders group by EmployeeID -- hangi calisanin kac adet siparisi var onu getirir. 
select EmployeeID, SUM(Freight) from Orders group by EmployeeID -- hangi calisan toplam ne kadar nakliye ucreti odemis

-- Having Komutu
-- -where- ile aralarindaki fark: -where- tablo sutunlari icin sartlandirma yaparken kullanilir. -having- ise aggregate fonksiyonlar uzerinde sartlandirma yaparken kullanilir.
-- -where- group by'dan once yazilir, -having- group by'dan sonra yazilir.
select CategoryID, COUNT(*) from Products where CategoryID > 3 group by CategoryID having COUNT(*) > 6

-- Tablolari ilkel yontemler ile birlestirme
select * from Employees, Orders -- boyle birlestirme yapildiginda iki tablo arasinda satir farki oldugunda, satir sayisi az olan tabloya diger tablonun satir sayisina esitleninceye kadar yeni satir eklenir ve bu satirlara NULL degerler atanir.
select * from Employees e, Orders o where e.EmployeeID = o.EmployeeID -- tablolara alians atayarak tablolarin sutunlarina kolaylikla erisebiliriz.
