-- String Fonksiyonlarý
select left(FirstName, 2) from Employees -- personellerin isimlerinin soldan baþlayarak 2 karakterlerini getirir.
select right(FirstName, 2) from Employees -- personellerin isimlerinin soldan baþlayarak 2 karakterlerini getirir.
select upper(FirstName) from Employees -- personellerin isimlerini büyük harflere çevirerek getirir.
select lower(FirstName) from Employees -- personellerin isimlerini küçük harflere çevirerek getirir.
select substring(FirstName,1,4) from Employees -- personellerin isimlerinin 1. indexten baþlayarak 4 karakterini getirir.
select ltrim('         Ahmet') -- soldan boþluklarý keser.
select rtrim('Ahmet        ') -- saðdan boþluklarý keser.
select reverse('ahmet') -- verilen deðeri tersine çevirir: temha.
select replace('ahmet osman','osman','sezgin') -- verilen deðeri, verilen 2. deðer ile deðiþtirir.
select charindex('t','ahmet osman sezgin') -- arnan karakterin, verilen ifadedeki yerini getirir (saymaya 1den baþlar): 5.
select FirstName, charindex('a',FirstName) from Employees -- çalýþanlarýn isimlerinde 'a' karakterinin yerini getirir.
select ContactName from Customers
select substring(ContactName,1,charindex(' ', ContactName))  from Customers -- þirket temsilcilerinin sadece isimlerini getirir.
select substring(ContactName, charindex(' ', ContactName), len(ContactName) - (charindex(' ', ContactName) - 1)) from Customers -- þirket temsilcilerinin sadece soyisimlerini getirir.

-- Sayýsal deðer iþlemleri
select 5+5
select 5*5
select 5-5
select 5/5

select pi() -- sayýsal iþlemlerde kullanmak için pi sayýsýný getirir
select sin(90) -- içerisine girilen deðerin sin deðerini getirir
select power(2,3) -- verilen deðerin, verilen üssünü alýr: 2 üzeri 3
select abs(-158) -- verilen deðerin mutlak deðerini alýr
select rand() -- her çaðrýldýðýnda 0-1 aralýðýnda rastgele bir sayý üretir
select floor(12.9) -- küsüratlý sayýlarý tam sayýlara yuvarlar: 12.9 -> 12

-- Tarih fonksiyonlarý
select GETDATE() -- güncel olarak tarihi ve saati getirir
select DATEADD(DAY, 999, GETDATE()) -- verilen tarihe 999 gün ekler oluþan tarihi getirir
select DATEADD(MONTH, 2, GETDATE()) -- verilen tarihe 2 ay ekler ve oluþan tarihi getirir
select DATEADD(YEAR, 10, GETDATE()) -- verilen tarihe 10 yýl ekler ve oluþan tarihi getirir

select DATEDIFF(DAY, '2022-08-10 17:30:00', GETDATE()) -- iki tarih arasýndaki gün farkýný getirir
select DATEDIFF(MONTH, '2016-09-15 17:30:00', GETDATE()) -- iki tarih arasýndaki ay farkýný getirir
select DATEDIFF(YEAR, '2016-09-15 17:30:00', GETDATE()) -- iki tarih arasýndaki yýl farkýný getirir
 
select DATEPART(DW, GETDATE()) -- verilen tarihteki günün, haftanýn kaçýncý günü olduðunu verir
select DATEPART(MONTH, GETDATE()) -- verilen tarihteki ayýn, yýlýn kaçýncý ayý olduðunu verir
select DATEPART(DAY, GETDATE()) --verilen tarihteki günün, ayýn kaçýncý günü olduðunu verir
 
 -- Top komutu
select top 2 * from Employees -- personeller tablosundaki ilk 2 personeli getirir

-- Distinct komutu
-- Örnek olarak personeller tablosunda 4 adet London bulunuyorken, distinct kullandýðýmýzda London sadece bir kez gelecektir.
select distinct City from Employees -- personeller tablosunda olan þehirleri birerkez olacak þekilde getirir.

-- Group By iþlemi
-- eðer select sorgusunda bir adet normal sutun ve bir aggregate fonksiyon çaðrýlýyorsa, sutunun gruplanmasý gerekecektir.
select CategoryID, COUNT(*) from Products group by CategoryID -- ürünler tablosunda CategoryID'leri gruplayarak hangi ID'den kaçar tane var onu getirir.
select CategoryID, COUNT(*) from Products where CategoryID > 3 group by CategoryID -- Üstteki sorgunun -where- ile þartlandýrýlmýþ hali.
select EmployeeID, COUNT(*) from  Orders group by EmployeeID -- hangi çalýþanýn kaç adet sipariþi var onu getirir. 
select EmployeeID, SUM(Freight) from Orders group by EmployeeID -- hangi çalýþan toplam ne kadar nakliye ücreti ödemiþ

-- Having Komutu
-- -where- ile aralarýndaki fark: -where- tablo sutunlarý için þartlandýrma yaparken kullanýlýr. -having- ise aggregate fonksiyonlar üzerinde þartlandýrma yaparken kullanýlýr.
-- -where- group by'dan önce yazýlýr, -having- group by'dan sonra yazýlýr.
select CategoryID, COUNT(*) from Products where CategoryID > 3 group by CategoryID having COUNT(*) > 6

-- Tablolarý ilkel yöntemler ile birleþtirme
select * from Employees, Orders -- böyle birleþtirme yapýldýðýnda iki tablo arasýnda satýr farký olduðunda, satýr sayýsý az olan tabloya diðer tablonun satýr sayýsýna eþitleninceye kadar yeni satýr eklenir ve bu satýrlara NULL deðerler atanýr.
select * from Employees e, Orders o where e.EmployeeID = o.EmployeeID -- tablolara alians atayarak tablolarýn sutunlarýna kolaylýk eriþebiliriz.