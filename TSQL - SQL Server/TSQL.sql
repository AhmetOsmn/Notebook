--TSQL harf büyük küçük harf duyarlý deðildir.

--komut ile hangi veritabanýnýn kullanmak istediðimizi belirtmek istiyorsak:
--use Northwind

--select: select yazdýktan sonra seçilmek istenen verileri ',' ile ayýrarak seçebiliriz. Seçilen verileri bir tablo olarak getirir.
select 3
select * from Employees

--alias atama
--bir sutunun adýný deðiþtirmek istersek kullanýrýz. Alians kullanmadan da deðiþtirebiliriz:
select 5 as Deger

--select 3 Deger
select 'Ahmet' isim, 'Sezgin' soyisim
select FirstName Ýsimler, LastName Soyisimler from Employees
select 2023 [Seçim Tarihi] -- alians verirken veya bir tabloyu çaðýrýrken, tablo adý veya sutun adý boþluk karakteri içeriyorsa [] kullanmalýyýz.

--sutunlarý birleþtirerek tek bir sutun halinde çekmek istersek:
--ayný tipte sutunlar
select FirstName+' '+LastName [Ad Soyad] from Employees

----farklý tipteki sutunlar
select FirstName + ' ' + CONVERT(nvarchar,BirthDate) [Ýsim Tarih] from Employees
select FirstName + ' ' + cast(BirthDate as nvarchar) [Ýsim Tarih] from Employees

--where komutu ile sorgulara þart ekleriz
select * from Employees where City = 'London' -- þehri London olan personeller
select * from Employees where ReportsTo < 3 -- rapor verdiði kiþinin id'si 3ten küçük olan personeller
select * from Employees where City = 'London' and Country = 'uk' -- þehri london olan ve ülkesi UK olan personeller
select * from Employees where TitleOfCourtesy = 'Mr.' or City = 'Seattle' -- ünvaný Mr. olan veya þehri Seattle olan personeller
select * from Employees where FirstName = 'Robert' and LastName = 'King' -- adý Robert olan ve soyadý King olan çalýþan
select * from Employees where EmployeeID = 5 -- EmployeeID'si 5 olan personel
select * from Employees where EmployeeID > 5 -- EmployeeID'si 5'ten büyük olan bütün personeller
select * from Employees where YEAR(HireDate) = 1993 -- iþe baþlama tarihleri 1993 olan personeller
select * from Employees where YEAR(HireDate) > 1993 -- 1993 yýlýndan sonra iþe baþlayan personeller
select * from Employees where DAY(BirthDate) <> 27 -- doðum günü ayýn 27'si olmayan personeller
select * from Employees where DAY(BirthDate) != 27
select * from Employees where YEAR(BirthDate) > 1950 and YEAR(BirthDate) < 1965 -- doðum yýlý 1950-1965 arasýnda olan personeller
select * from Employees where YEAR(BirthDate) between 1950 and 1965 -- doðum yýlý 1950-1965 arasýnda olan personellerin -between- keyword'u ile gösterilmesi
select FirstName from Employees where City = 'London' or City = 'Tacoma' or City = 'Kirkland' -- þehri London, Tacoma veya Kirkland olan personellerin isimleri

-- -in- keyword'unu koþul durumlarýnda or, or, or gibi çoklu or kullanýldýðýnda tercih ederiz. 
select FirstName from Employees where City in('London','Tacoma','Kirkland') -- þehiri London, Tacoma ve Kirkland olan personellerin -in- keyword'u ile gösterilmesi

-- -like- komutu ile sutunlar içerisindeki verilere þartlar koyabiliriz.
select FirstName Ýsim, LastName Soyisim from Employees where FirstName like 'j%' -- isminin baþ harfi 'j' olan personellerin adý ve soyadý
select FirstName Ýsim, LastName Soyisim from Employees where FirstName like '%y' -- isminin son harfi 'y' olan personellerin adý ve soyadý
select * from Employees where FirstName like '%ert' -- isminin son 3 harfi 'ert' olan personeller
select * from Employees where FirstName like 'r%t' -- isminin ilk harfi 'r', son harfi 't' olan personeller
select * from Employees where FirstName like 'r%' and FirstName like '%t' -- yukarýdaki kullanýmýn uzatýlmýþ hali
select * from Employees where FirstName like '%an%' -- isminde 'an' geçen personeller
select * from Employees where FirstName like 'n%an%' -- isminin baþ harfi 'n' olan ve ismin içerisinde 'an' olan personeller
select * from Employees where FirstName like 'n%' and FirstName like '%an%' -- yukarýdaki kullanýmýn uzatýlmýþ hali  
select * from Employees where FirstName like 'a_d%' -- isminin ilk harfi 'a' olan, 2. harfi farketmez, 3. harfi 'd' olan personeller
select * from Employees where FirstName like 'm___a%' --isminin ilk harfi 'm', 2,3,4. harfi fark etmeyen, 5. harfi 'a' olan personeller
select * from Employees where FirstName like '[mnr]%' -- isminin ilk harfi m,n veya r olan personeller
select * from Employees where FirstName like '%[ai]%' -- isminin içerisinde a veya i geçen personeller 
select * from Employees where FirstName like '[a-k]%' -- isminin baþharfi a-k arasýndaki harfler olan personeller
select * from Employees where FirstName like '[^a]%' -- isminin baþharfi a olmayan personeller
select * from Employees where FirstName like '[^an]%' -- isminin baþharfi an olmayan personeller
select * from Employees where FirstName like '[_]%' -- isminin baþharfi '_' olan personeller
select * from Employees where FirstName like 'k_%' escape 'k' -- isminin baþharfi '_' olan personelleri k escape karakteri ile belirtiyoruz 

-- Aggregate Fonksiyonlar
select avg(EmployeeID) Ortalama from Employees -- personellerin ID'lerinin ortalamasý
select max(EmployeeID) MaxID from Employees -- en büyük EmployeeID
select min(EmployeeID) MinID from Employees -- en küçük EmployeeID
select count(*) from Employees -- verilen kolondaki veri sayýsý
select count(FirstName) from Employees -- isim kolonundaki veri sayýsý
select sum(Freight) from Orders -- nakliye ücretlerinin toplamý








