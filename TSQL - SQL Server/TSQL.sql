--TSQL harf buyuk kucuk harf duyarl� degildir.

--komut ile hangi veritabanini kullanmak istedigimizi belirtmek istiyorsak:
--use Northwind

--select: select yazdiktan sonra secilmek istenen verileri ',' ile ayirarak secebiliriz. Secilen verileri bir tablo olarak getirir.
select 3
select * from Employees

--alias atama
--bir sutunun adini degistirmek istersek kullaniriz. Alians kullanmadan da degistirebiliriz:
select 5 as Deger

--select 3 Deger
select 'Ahmet' isim, 'Sezgin' soyisim
select FirstName Isimler, LastName Soyisimler from Employees
select 2023 [Secim Tarihi] -- alians verirken veya bir tabloyu cagirirken, tablo adi veya sutun adi bosluk(' ') karakteri iceriyorsa [] kullanmaliyiz.

--sutunlari birlestirerek tek bir sutun halinde cekmek istersek:
--ayni tipte sutunlar
select FirstName+' '+LastName [Ad Soyad] from Employees

----farkli tipteki sutunlar
select FirstName + ' ' + CONVERT(nvarchar,BirthDate) [Isim Tarih] from Employees
select FirstName + ' ' + cast(BirthDate as nvarchar) [Isim Tarih] from Employees

--where komutu ile sorgulara sart ekleriz
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

-- Aggregate Fonksiyonlar
select avg(EmployeeID) Ortalama from Employees -- personellerin ID'lerinin ortalamasi
select max(EmployeeID) MaxID from Employees -- en buyuk EmployeeID
select min(EmployeeID) MinID from Employees -- en kucuk EmployeeID
select count(*) from Employees -- verilen kolondaki veri sayisi
select count(FirstName) from Employees -- isim kolonundaki veri sayisi
select sum(Freight) from Orders -- nakliye ucretlerinin toplami

