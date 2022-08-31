use NORTHWND
-------------------------------## DML (Data Manuplation Language) Komutlari ##-------------------------------

-- Tablodan veri secmek: SELECT
-- Tabloya veri ekleme: INSERT
-- Tablodaki veriyi guncelleme: UPDATE
-- Tablodan veri silme: DELETE


------------------------------- SELECT ------------------------------- 
select * from Employees

------------------------------- INSERT -------------------------------
-- Tablolarin veri gonderilmeyen kolonlarina otomatik olarak NULL atanir (Belirli kosullarda).
insert Employees (FirstName, LastName) values('Ahmet','Sezgin')

-- Kolon belirtmeden direkt degerleri vererek te veri eklemesi yapabiliriz. 
-- Burada kolonlarin siralamasi tablonun kolonlarinin Object Explorer icerisinde gorundugu siradadir.
insert Employees values ('Bayraktar', 'Baris',null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null)

-- into komutu ile de yazilabilir. Eski kullanimdir ama hala kullanilabilir.
insert into Employees (FirstName, LastName) values('Ahmet','Sezgin')

-- Not Null olarak isaretlenen kolonlar bos birakilamaz, bu kolonlara veri eklenmek zorundadir.

-- Auto Increment (Otomatik artan) olarak isaretlenen kolonlara veri gonderilmez

-- Ayn� tabloya arka arkaya veriler eklemek istiyorsak verileri ',' ile ayirarak ekleyebiliriz.
insert Employees (FirstName, LastName) values ('Ahmet','Sezgin'), ('Osman','Sezgin'), ('Ali','Kaya') 

-- Bir tablodan cekilen verileri, veri tipleri ve kolonlar uyumlu ise baska bir tabloya ekleyebiliriz.
insert OrnekPersoneller select FirstName, LastName from Employees

-- Bir tablodan istedigimiz kolonlardaki verileri cekip ayri bir tablo olarak kaydedebiliriz.
select FirstName,LastName, Region into NewEmployees from Employees


------------------------------- UPDATE -------------------------------
-- Update sorgularinda sorguya bir sart eklenmezse tablonun ilgili kolonundaki butun veriler guncellenecegi icin veri kayiplari yasanabilir.
-- Employees tablosundaki butun Title kolonunu 'new title' olarak set eder.
update Employees set Title = 'new title'

-- Employees tablosundaki EmployeeID'si 22 olan kisinin Title kolonu 'new title' olacaktir.
update Employees set Title = 'new title' where EmployeeID=22

-- Join yapilarinin kullanildigi durumlarda da tablolarda guncellemeler yapilabilir.
update Products set ProductName = c.CategoryName from Products p 
inner join Categories c 
on c.CategoryID=p.CategoryID where ProductID= 1

-- Sub Query'den gelen veri ile guncelleme yapilabilir.
update Products set ProductName = (select FirstName from Employees where EmployeeID=2) where ProductID=2

-- Top komutunu kullanarak veri guncellemesi yapilabilir.
update top(5) Products set ProductName = 'test'
select * from Products

------------------------------- DELETE -------------------------------
-- Delete sorgularinda sorguya bir sart eklenmezse tablodaki butun veriler silinecektir.
delete from Products where ProductID = 82

-- Bir tablodaki butun verileri sildigimizde, identity degeri 0'a donmez. 
-- O tabloya eleman eklendigi zaman identity kaldigi yerden devam eder.
-- Hem tablodaki verileri silmek, hem de identity'i 0'lamak istiyorsak 'Truncate' komutu kullanilir.
