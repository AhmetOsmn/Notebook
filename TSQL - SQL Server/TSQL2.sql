-- String Fonksiyonlar�
select left(FirstName, 2) from Employees -- personellerin isimlerinin soldan ba�layarak 2 karakterlerini getirir.
select right(FirstName, 2) from Employees -- personellerin isimlerinin soldan ba�layarak 2 karakterlerini getirir.
select upper(FirstName) from Employees -- personellerin isimlerini b�y�k harflere �evirerek getirir.
select lower(FirstName) from Employees -- personellerin isimlerini k���k harflere �evirerek getirir.
select substring(FirstName,1,4) from Employees -- personellerin isimlerinin 1. indexten ba�layarak 4 karakterini getirir.
select ltrim('         Ahmet') -- soldan bo�luklar� keser.
select rtrim('Ahmet        ') -- sa�dan bo�luklar� keser.
select reverse('ahmet') -- verilen de�eri tersine �evirir: temha.
select replace('ahmet osman','osman','sezgin') -- verilen de�eri, verilen 2. de�er ile de�i�tirir.
select charindex('t','ahmet osman sezgin') -- arnan karakterin, verilen ifadedeki yerini getirir (saymaya 1den ba�lar): 5.
select FirstName, charindex('a',FirstName) from Employees -- �al��anlar�n isimlerinde 'a' karakterinin yerini getirir.
select ContactName from Customers
select substring(ContactName,1,charindex(' ', ContactName))  from Customers -- �irket temsilcilerinin sadece isimlerini getirir.
select substring(ContactName, charindex(' ', ContactName), len(ContactName) - (charindex(' ', ContactName) - 1)) from Customers -- �irket temsilcilerinin sadece soyisimlerini getirir.

-- Say�sal de�er i�lemleri
select 5+5
select 5*5
select 5-5
select 5/5

select pi() -- say�sal i�lemlerde kullanmak i�in pi say�s�n� getirir
select sin(90) -- i�erisine girilen de�erin sin de�erini getirir
select power(2,3) -- verilen de�erin, verilen �ss�n� al�r: 2 �zeri 3
select abs(-158) -- verilen de�erin mutlak de�erini al�r
select rand() -- her �a�r�ld���nda 0-1 aral���nda rastgele bir say� �retir
select floor(12.9) -- k�s�ratl� say�lar� tam say�lara yuvarlar: 12.9 -> 12

-- Tarih fonksiyonlar�
select GETDATE() -- g�ncel olarak tarihi ve saati getirir
select DATEADD(DAY, 999, GETDATE()) -- verilen tarihe 999 g�n ekler olu�an tarihi getirir
select DATEADD(MONTH, 2, GETDATE()) -- verilen tarihe 2 ay ekler ve olu�an tarihi getirir
select DATEADD(YEAR, 10, GETDATE()) -- verilen tarihe 10 y�l ekler ve olu�an tarihi getirir

select DATEDIFF(DAY, '2022-08-10 17:30:00', GETDATE()) -- iki tarih aras�ndaki g�n fark�n� getirir
select DATEDIFF(MONTH, '2016-09-15 17:30:00', GETDATE()) -- iki tarih aras�ndaki ay fark�n� getirir
select DATEDIFF(YEAR, '2016-09-15 17:30:00', GETDATE()) -- iki tarih aras�ndaki y�l fark�n� getirir
 
select DATEPART(DW, GETDATE()) -- verilen tarihteki g�n�n, haftan�n ka��nc� g�n� oldu�unu verir
select DATEPART(MONTH, GETDATE()) -- verilen tarihteki ay�n, y�l�n ka��nc� ay� oldu�unu verir
select DATEPART(DAY, GETDATE()) --verilen tarihteki g�n�n, ay�n ka��nc� g�n� oldu�unu verir
 
 -- Top komutu
select top 2 * from Employees -- personeller tablosundaki ilk 2 personeli getirir

-- Distinct komutu
-- �rnek olarak personeller tablosunda 4 adet London bulunuyorken, distinct kulland���m�zda London sadece bir kez gelecektir.
select distinct City from Employees -- personeller tablosunda olan �ehirleri birerkez olacak �ekilde getirir.

-- Group By i�lemi
-- e�er select sorgusunda bir adet normal sutun ve bir aggregate fonksiyon �a�r�l�yorsa, sutunun gruplanmas� gerekecektir.
select CategoryID, COUNT(*) from Products group by CategoryID -- �r�nler tablosunda CategoryID'leri gruplayarak hangi ID'den ka�ar tane var onu getirir.
select CategoryID, COUNT(*) from Products where CategoryID > 3 group by CategoryID -- �stteki sorgunun -where- ile �artland�r�lm�� hali.
select EmployeeID, COUNT(*) from  Orders group by EmployeeID -- hangi �al��an�n ka� adet sipari�i var onu getirir. 
select EmployeeID, SUM(Freight) from Orders group by EmployeeID -- hangi �al��an toplam ne kadar nakliye �creti �demi�

-- Having Komutu
-- -where- ile aralar�ndaki fark: -where- tablo sutunlar� i�in �artland�rma yaparken kullan�l�r. -having- ise aggregate fonksiyonlar �zerinde �artland�rma yaparken kullan�l�r.
-- -where- group by'dan �nce yaz�l�r, -having- group by'dan sonra yaz�l�r.
select CategoryID, COUNT(*) from Products where CategoryID > 3 group by CategoryID having COUNT(*) > 6

-- Tablolar� ilkel y�ntemler ile birle�tirme
select * from Employees, Orders -- b�yle birle�tirme yap�ld���nda iki tablo aras�nda sat�r fark� oldu�unda, sat�r say�s� az olan tabloya di�er tablonun sat�r say�s�na e�itleninceye kadar yeni sat�r eklenir ve bu sat�rlara NULL de�erler atan�r.
select * from Employees e, Orders o where e.EmployeeID = o.EmployeeID -- tablolara alians atayarak tablolar�n sutunlar�na kolayl�k eri�ebiliriz.
