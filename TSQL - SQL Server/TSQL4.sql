-------------------------------------------------------- @@Identity Komutu --------------------------------------------------------
-- Ilgili db icerisinde yapilan en son insert isleminin Identity degerini getirir.

insert Categories(CategoryName) values('Test Kategori')
select @@IDENTITY

-------------------------------------------------------- @@Rowcount Komutu --------------------------------------------------------
-- Yapilan islemden etkilenen elemanlarin sayisini getirir.

select * from Employees
select @@ROWCOUNT

-------------------------------------------------------- DBCC Checkident() Komutu --------------------------------------------------------
-- Tablo icerisindeki identity alanina mudahale etmemizi saglar. 
-- Ornek olarak bir identity degerini x sayisinden devam ettirmek istersek su sekilde yapabiliriz:
-- Hata almamak icin bu metodu kullanirken x sayisinin tablodaki en buyuk identity degerinden buyuk olmasi gerekir.

DBCC Checkident(Categories, reseed, 50)
select * from Categories

insert Categories(CategoryName) values('Test Kategori 2')
select * from Categories

-------------------------------------------------------- NULL Kontrolleri --------------------------------------------------------
-- Swicth yapisina benzer bir yapi kurarak NULL degerleri kontrol altina alabiliriz.
select FirstName,
case
	when Region is null then 'Bolge bilinmiyor'
	else Region
end
from Employees

-- Coalesce Komutu --
-- Girilen kolonda bulunan NULL degerleri, verilen 2.parametreye cevirir.
-- NULL olmayan degerleri oldugu gibi getirir.

select FirstName, Coalesce(Region, 'Bolge bilinmiyor') from Employees

-- IsNull Komutu --
-- Girilen kolonda bulunan NULL degerleri, verilen 2.parametreye cevirir.
-- NULL olmayan degerleri oldugu gibi getirir.
select FirstName, isnull(Region, 'Bolge bilinmiyor') from Employees

-- NULLIF Komutu --
-- Metoda verilen 1. parametre, 2. parametreye esit ise NULL dondurur.

select nullif(0,0)

-- Ornek problem. Urunler tablosundaki urunlerin stok sayilarinin ortalamasini almak istiyoruz ama 0 olan degerlerin ortalamaya katilmasini istemiyoruz.
-- Yukaridaki problemi cozmek icin NULLIF komutunu kullanabiliriz.

select avg(UnitsInStock ) from Products

select AVG(nullif(UnitsInStock,0)) from Products

-------------------------------------------------------- DB'de tablolari sorgulama --------------------------------------------------------
-- Icerisinde bulunulan db'deki tablolari getirir.
select * from sys.tables

-- Ilgili tablonun Primary Key'i var ise 1, yok ise 0 doner.
select OBJECTPROPERTY(OBJECT_ID('Categories'),'TableHasPrimaryKey')
