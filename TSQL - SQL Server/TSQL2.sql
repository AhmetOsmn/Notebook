use NORTHWND
--## INNER JOIN ##--

	-- Hangi personel hangi satislari yapmistir
	select * from Employees e inner join Orders o on o.EmployeeID = e.EmployeeID

	-- Hangi urun hangi kategoride
	select p.ProductName Urun, c.CategoryName Kategori from Products p 
	inner join Categories c 
	on c.CategoryID=p.CategoryID

	-- Beverages kategorisindeki urunler
	select p.ProductName Urun, c.CategoryName Kategori from Products P 
	inner join Categories c 
	on c.CategoryID = p.CategoryID where c.CategoryName='Beverages' 

	-- Beverages kategorisindeki urunlerin sayisi
	select COUNT(p.ProductName) from Products p 
	inner join Categories c 
	on c.CategoryID = p.CategoryID where c.CategoryName='Beverages' 

	-- Seafood kategorisindeki urunler
	select p.ProductName Urun, c.CategoryName Kategori from Products P 
	inner join Categories c 
	on c.CategoryID = p.CategoryID where c.CategoryName='Seafood' 

	-- Hangi satisi hangi calisan yapmis
	select o.OrderID ID, (e.FirstName+' '+e.LastName) Calisan  from Orders o 
	inner join Employees e 
	on e.EmployeeID = o.EmployeeID

	-- Hangi calisan kac satis yapmis
	select (e.FirstName+' '+e.LastName) Calisan, COUNT(o.OrderID)  from Orders o 
	inner join Employees e 
	on e.EmployeeID = o.EmployeeID group by (e.FirstName+' '+e.LastName)

	-- Faks numarasi NULL olmayan tedarikcilerden alinan urunler
	select s.CompanyName Tedarikci, p.ProductName Urun, s.Fax FaksNo from Suppliers s 
	inner join Products p 
	on p.SupplierID=s.SupplierID where s.Fax is not null

	-- Yukaridaki sorgu ile ayni islemi yapar
	select s.CompanyName Tedarikci, p.ProductName Urun, s.Fax FaksNo from Suppliers s 
	inner join Products p 
	on p.SupplierID=s.SupplierID where s.Fax <> 'Null' 

	-- Ikiden fazla tabloyu birlestirmek --

	-- 1997 Yilinda ve sonrasinda Nancy'nin satis yaptigi firmalarin isimleri
	select (e.FirstName+' '+e.LastName) Calisan, c.CompanyName SatisYapilanFirma, o.OrderDate SatisTarihi from Orders o 
	inner join Customers c on c.CustomerID = o.CustomerID
	inner join Employees e on e.EmployeeID = o.EmployeeID where e.FirstName='Nancy' and YEAR(o.OrderDate) > 1996

	-- Limited olan tedarikcilerden alinmis olan Seafood kategorisindeki urunlerin toplam satis tutari
	select SUM(p.UnitPrice * p.UnitsInStock) ToplamTutar from Products p
	inner join Suppliers s on s.SupplierID = p.SupplierID
	inner join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Seafood' and s.CompanyName like '%Ltd.%'

	-- Ayni tabloyu birlestirme --

	-- Personeller ve rapor verdigi kisiler
	select (e.FirstName+' '+e.LastName) RaporVeren, (emp.FirstName+' '+emp.LastName) RaporAlan from Employees e 
	inner join Employees emp on emp.EmployeeID = e.ReportsTo

	-- Hangi kategoride kac adet urun var
	select c.CategoryName Kategori, COUNT(p.ProductName) UrunMiktari  from Products p 
	inner join Categories c on p.CategoryID = c.CategoryID
	group by c.CategoryName

	-- Hangi calisan kac adet satis yapmis, satis adedi 100'den fazla olanlar ve personelin adinin bas harfi M olan kayitlar gelsin.
	select (e.FirstName+' '+e.LastName) Calisan, COUNT(*) SatisSayisi from Orders o 
	inner join Employees e on o.EmployeeID = e.EmployeeID where  e.FirstName like 'M%'
	group by (e.FirstName+' '+e.LastName) having COUNT(*) > 100  -- group by islemlerinde -where- group by'dan once gelir, having group by'dan sonra gelir.

	-- Seafood kategorisindeki urunlerin sayisi
	select c.CategoryName Kategori, COUNT(*) UrunSayisi from Products p 
	inner join Categories c on p.CategoryID = c.CategoryID where c.CategoryName = 'Seafood'
	group by c.CategoryName

	-- En cok satis yapan personel
	select top 1 (e.FirstName + ' ' + e.LastName) Calisan, COUNT(o.OrderID) SatisSayisi  from Orders o
	inner join Employees e on e.EmployeeID = o.EmployeeID
	group by (e.FirstName + ' ' + e.LastName)
	order by SatisSayisi desc

	-- Adinda 'a' harfi olan personellerin satis id'si 10500'den buyuk olan satislarinin toplam tutari (miktar * birim fiyati) ve bu satislarin yapildigi tarihler
	select (e.FirstName + ' ' + e.LastName) Calisan, SUM(od.Quantity * od.UnitPrice) Tutar, o.OrderDate Tarih from Orders o
	inner join Employees e on o.EmployeeID = e.EmployeeID 
	inner join [Order Details] od on o.OrderID = od.OrderID where o.OrderID > 10500 and e.FirstName like '%a%'
	group by (e.FirstName + ' ' + e.LastName), o.OrderDate

-- OUTER JOIN -- 
-- Inner Join'de eslesen veriler getiriliyordu. Outer Join'de ise eslesmeyen veriler getirilir.

	-- Left Join
	-- join ifadesinin solundaki tablodan butun verileri getirir. Sagindaki talodan eslesenleri yan yana, eslesmeyenleri NULL olarak getirir.
	select * from Oyuncu o left outer join Film f on f.FilmID = o.FilmID
	select * from Film f left outer join Oyuncu o on f.FilmID = o.FilmID
	-- veya 
	select * from Oyuncu o left join Film f on f.FilmID = o.FilmID
	select * from Film f left join Oyuncu o on f.FilmID = o.FilmID

	-- Right Join
	-- join ifadesinin sagindaki tablodan butun verileri getirir. Solundaki talodan eslesenleri yan yana, eslesmeyenleri NULL olarak getirir.
	select * from Oyuncu o right join Film f on f.FilmID = o.FilmID
	select * from Film f right join Oyuncu o on f.FilmID = o.FilmID
	select * from Film f left join Oyuncu o on f.FilmID = o.FilmID

	-- Full Join
	-- join ifadesinin iki yanindaki tablolardan veri esleslerde eslesmeselerde getirir
	select * from Oyuncu o full join Film f on f.FilmID = o.FilmID

	-- Cross Join
	-- iki tablo arasinda kartezyen carpimi yapar. Yani iki tablo arasindaki verileri tek tek birbirleri ile birlestirir.
	select COUNT(*) from Oyuncu
	select COUNT(*) from Film
	select COUNT(*) from Oyuncu o cross join Film f

	select o.Adi Oyuncu, f.FilmAdi Film from Oyuncu o cross join Film f -- cross join ile olusturulan tabloya -where- ile sart koyulamaz.

