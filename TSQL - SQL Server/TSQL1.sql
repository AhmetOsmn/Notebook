use NORTHWND
-- TSQL harf buyuk kucuk harf duyarli degildir.

-- Komut ile hangi veritabanini kullanmak istedigimizi belirtmek istiyorsak: -use Northwind-

-- select: -select- yazdiktan sonra secilmek istenen verileri ',' ile ayirarak secebiliriz. Secilen verileri bir tablo olarak getirir.
select 3
select * from Employees

-- Alias Atama --
-- Bir sutunun adini degistirmek istersek kullaniriz. Alias kullanmadan da degistirebiliriz:
select 5 as Deger
select 5 Deger
select 'Ahmet' isim, 'Sezgin' soyisim
select FirstName Isimler, LastName Soyisimler from Employees
select 2023 [Secim Tarihi] -- alias verirken veya bir tabloyu cagirirken, tablo adi veya sutun adi bosluk(' ') karakteri iceriyorsa [] kullanmaliyiz.

-- Sutunlari birlestirerek tek bir sutun halinde cekmek istersek:
-- Ayni tipte sutunlar
select FirstName+' '+LastName [Ad Soyad] from Employees

-- Farkli tipteki sutunlar
select FirstName + ' ' + CONVERT(nvarchar,BirthDate) [Isim Tarih] from Employees
select FirstName + ' ' + cast(BirthDate as nvarchar) [Isim Tarih] from Employees

-- -where- komutu ile sorgulara sart ekleriz
select * from Employees where City = 'London' -- sehri London olan personeller
select * from Employees where ReportsTo < 3 -- rapor verdigi kisinin id'si 3ten kucuk olan personeller
select * from Employees where City = 'London' and Country = 'uk' -- sehri london olan ve ulkesi UK olan personeller
select * from Employees where TitleOfCourtesy = 'Mr.' or City = 'Seattle' -- unvani Mr. olan veya sehri Seattle olan personeller
select * from Employees where FirstName = 'Robert' and LastName = 'King' -- adi Robert olan ve soyadi King olan calisan
select * from Employees where EmployeeID = 5 -- EmployeeID'si 5 olan personel
select * from Employees where EmployeeID > 5 -- EmployeeID'si 5'ten buyuk olan butun personeller
select * from Employees where YEAR(HireDate) = 1993 -- ise baslama tarihleri 1993 olan personeller
select * from Employees where YEAR(HireDate) > 1993 -- 1993 yilindan sonra ise baslayan personeller
select * from Employees where DAY(BirthDate) <> 27 -- dogum gunu ayin 27'si olmayan personeller
select * from Employees where DAY(BirthDate) != 27
select * from Employees where YEAR(BirthDate) > 1950 and YEAR(BirthDate) < 1965 -- dogum yili 1950-1965 arasinda olan personeller
select * from Employees where YEAR(BirthDate) between 1950 and 1965 -- dogum yili 1950-1965 arasinda olan personellerin -between- keyword'u ile gosterilmesi
select FirstName from Employees where City = 'London' or City = 'Tacoma' or City = 'Kirkland' -- sehri London, Tacoma veya Kirkland olan personellerin isimleri

-- -in- keyword'unu kosul durumlarinda or, or, or gibi coklu or kullanildiginda tercih ederiz. 
select FirstName from Employees where City in('London','Tacoma','Kirkland') -- sehiri London, Tacoma ve Kirkland olan personellerin -in- keyword'u ile gosterilmesi

-- -like- komutu ile sutunlar icerisindeki verilere sartlar koyabiliriz.
select FirstName Isim, LastName Soyisim from Employees where FirstName like 'j%' -- isminin bas harfi 'j' olan personellerin adi ve soyadi
select FirstName Isim, LastName Soyisim from Employees where FirstName like '%y' -- isminin son harfi 'y' olan personellerin adi ve soyadi
select * from Employees where FirstName like '%ert' -- isminin son 3 harfi 'ert' olan personeller
select * from Employees where FirstName like 'r%t' -- isminin ilk harfi 'r', son harfi 't' olan personeller
select * from Employees where FirstName like 'r%' and FirstName like '%t' -- yukaridaki kullanimin uzatilmis hali
select * from Employees where FirstName like '%an%' -- isminde 'an' gecen personeller
select * from Employees where FirstName like 'n%an%' -- isminin bas harfi 'n' olan ve ismin icerisinde 'an' olan personeller
select * from Employees where FirstName like 'n%' and FirstName like '%an%' -- yukaridaki kullanimin uzatilmis hali  
select * from Employees where FirstName like 'a_d%' -- isminin ilk harfi 'a' olan, 2. harfi farketmez, 3. harfi 'd' olan personeller
select * from Employees where FirstName like 'm___a%' --isminin ilk harfi 'm', 2,3,4. harfi fark etmeyen, 5. harfi 'a' olan personeller
select * from Employees where FirstName like '[mnr]%' -- isminin ilk harfi m,n veya r olan personeller
select * from Employees where FirstName like '%[ai]%' -- isminin icerisinde a veya i gecen personeller 
select * from Employees where FirstName like '[a-k]%' -- isminin basharfi a-k arasindaki harfler olan personeller
select * from Employees where FirstName like '[^a]%' -- isminin basharfi a olmayan personeller
select * from Employees where FirstName like '[^an]%' -- isminin basharfi an olmayan personeller
select * from Employees where FirstName like '[_]%' -- isminin basharfi '_' olan personeller
select * from Employees where FirstName like 'k_%' escape 'k' -- isminin basharfi '_' olan personelleri k escape karakteri ile belirtiyoruz 

-- Aggregate Fonksiyonlar --
select avg(EmployeeID) Ortalama from Employees -- personellerin ID'lerinin ortalamasi
select max(EmployeeID) MaxID from Employees -- en buyuk EmployeeID
select min(EmployeeID) MinID from Employees -- en kucuk EmployeeID
select count(*) from Employees -- verilen kolondaki veri sayisi
select count(FirstName) from Employees -- isim kolonundaki veri sayisi
select sum(Freight) from Orders -- nakliye ucretlerinin toplami

-- String Fonksiyonlari --
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
select FirstName, charindex('a',FirstName) from Employees	-- calisanlarin isimlerinde 'a' karakterinin yerini getirir.
select ContactName from Customers
select substring(ContactName,1,charindex(' ', ContactName))  from Customers -- sirket temsilcilerinin sadece isimlerini getirir.
select substring(ContactName, charindex(' ', ContactName), len(ContactName) - (charindex(' ', ContactName) - 1)) from Customers -- sirket temsilcilerinin sadece soyisimlerini getirir.

-- Sayisal deger islemleri --
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

-- Tarih fonksiyonlari --
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
-- Eger select sorgusunda bir adet normal sutun ve bir aggregate fonksiyon cagiriliyorsa, sutunun gruplanmasi gerekecektir.
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

