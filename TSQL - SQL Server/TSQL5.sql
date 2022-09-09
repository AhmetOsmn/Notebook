-------------------------------------------------------- DDL Komutlari --------------------------------------------------------
-- DDL Komutlari db nesneleri yaratmayi, duzenlemeyi ve silmeyi saglar.
-- DDL Komurlari: Create, Alter, Drop
-- Create: Db veya db nesnesi yaratmak icin kullanilir.
-- Alter: Create ile olusturulmus bir db nesnesini guncellemek icin kullanilir.
-- Drop: Create ile olusturulmus bir db nesnesini guncellemek icin kullanilir.

---- Create ----
-- Db, Table, View, StoreProcedure, Trigger vb. db nesnelerini olusturur.
-- Kullanim sekli: CREATE [NESNE] [NESNE ADI]

-- Default ayarlar ile bir db olusturmak icin:
Create Database OrnekDB

-- Db'yi verdigimiz ayarlara gore olusturmak icin:
create database CreateDbExample
on
(
	name = 'gg', --db'nin arka plandaki fiziksel ismi.
	filename = 'D:\gg.mdf', -- db'nin fiziksel dizini.
	Size = 5, -- db'nin baslangic boyutu (mb).
	Filegrowth = 3 -- db'nin boyutu doldukca arttirilacak boyut miktari.
)

-- Create ile Tablo olusturma --
create table OrnekTablo
(
	Kolon1 int,
	Kolon2 nvarchar(max),
	Kolon3 money
)

create table OrnekTablo2
(
	-- Kolon isimlerinde bosluk karakteri olacaksa [] icerisinde tanimlanirlar.
	[Kolon 1] int,
	[Kolon 2] nvarchar(max),
	[Kolon 3] money
)

create table OrnekTablo3
(
	-- Kolonu primary key olarak tanimlamak ve baslangic ve artis degerlerini vermek.
	ID int primary key identity(1,1),
	Kolon1 nvarchar(50),
	Kolon2 nvarchar(50)
)

---- ALTER ----
-- Db nesnelerini guncellemek icin kullanilir.
-- Kullanim sekli: CREATE [NESNE] [NESNE ADI]

-- Alter ile var olan tabloya yeni bir kolon eklemek
alter table OrnekTablo add Kolon4 nvarchar(50)
select * from OrnekTablo

-- Var olan bir tablonun kolonunu guncellemek
alter table OrnekTablo
alter Column Kolon4 int

-- Var olan bir tablonun kolonunu silmek
alter table OrnekTablo
drop Column Kolon4 

-- Alter ile tabloya Constraint eklemek --
alter table OrnekTablo 
add constraint OrnekConstraint Default 'Bos' for Kolon2

-- Alter ile tabloya Constraint silmek --
alter table OrnekTablo 
drop constraint OrnekConstraint 

-- Alter gibi bir tablonun ismini SP_RENAME ile degistirmek --
sp_rename 'OrnekTablo', 'YeniTablo'

-- sp_rename ile kolon ismi guncelleme --
sp_rename 'YeniTablo.Kolon1','KolonNew','Column'


---- DROP ----
-- Create ile olusturulan db nesnelerini siler.
-- Kullanimi Drop Nesne NesneAdi

Drop table YeniTablo
