--TSQL harf b�y�k k���k harf duyarl� de�ildir.

--komut ile hangi veritaban�n�n kullanmak istedi�imizi belirtmek istiyorsak:
--use Northwind

--select: select yazd�ktan sonra se�ilmek istenen verileri ',' ile ay�rarak se�ebiliriz. Se�ilen verileri bir tablo olarak getirir.
select 3
select * from Employees

--alias atama
--bir sutunun ad�n� de�i�tirmek istersek kullan�r�z. Alians kullanmadan da de�i�tirebiliriz:
select 5 as Deger

--select 3 Deger
select 'Ahmet' isim, 'Sezgin' soyisim
select FirstName �simler, LastName Soyisimler from Employees
select 2023 [Se�im Tarihi] -- alians verirken veya bir tabloyu �a��r�rken, tablo ad� veya sutun ad� bo�luk karakteri i�eriyorsa [] kullanmal�y�z.

--sutunlar� birle�tirerek tek bir sutun halinde �ekmek istersek:
--ayn� tipte sutunlar
select FirstName+' '+LastName [Ad Soyad] from Employees

----farkl� tipteki sutunlar
select FirstName + ' ' + CONVERT(nvarchar,BirthDate) [�sim Tarih] from Employees
select FirstName + ' ' + cast(BirthDate as nvarchar) [�sim Tarih] from Employees

--where komutu ile sorgulara �art ekleriz
select * from Employees where City = 'London' -- �ehri London olan personeller
select * from Employees where ReportsTo < 3 -- rapor verdi�i ki�inin id'si 3ten k���k olan personeller
select * from Employees where City = 'London' and Country = 'uk' -- �ehri london olan ve �lkesi UK olan personeller
select * from Employees where TitleOfCourtesy = 'Mr.' or City = 'Seattle' -- �nvan� Mr. olan veya �ehri Seattle olan personeller
select * from Employees where FirstName = 'Robert' and LastName = 'King' -- ad� Robert olan ve soyad� King olan �al��an
select * from Employees where EmployeeID = 5 -- EmployeeID'si 5 olan personel
select * from Employees where EmployeeID > 5 -- EmployeeID'si 5'ten b�y�k olan b�t�n personeller
select * from Employees where YEAR(HireDate) = 1993 -- i�e ba�lama tarihleri 1993 olan personeller
select * from Employees where YEAR(HireDate) > 1993 -- 1993 y�l�ndan sonra i�e ba�layan personeller
select * from Employees where DAY(BirthDate) <> 27 -- do�um g�n� ay�n 27'si olmayan personeller
select * from Employees where DAY(BirthDate) != 27
select * from Employees where YEAR(BirthDate) > 1950 and YEAR(BirthDate) < 1965 -- do�um y�l� 1950-1965 aras�nda olan personeller
select * from Employees where YEAR(BirthDate) between 1950 and 1965 -- do�um y�l� 1950-1965 aras�nda olan personellerin -between- keyword'u ile g�sterilmesi
select FirstName from Employees where City = 'London' or City = 'Tacoma' or City = 'Kirkland' -- �ehri London, Tacoma veya Kirkland olan personellerin isimleri

-- -in- keyword'unu ko�ul durumlar�nda or, or, or gibi �oklu or kullan�ld���nda tercih ederiz. 
select FirstName from Employees where City in('London','Tacoma','Kirkland') -- �ehiri London, Tacoma ve Kirkland olan personellerin -in- keyword'u ile g�sterilmesi

-- -like- komutu ile sutunlar i�erisindeki verilere �artlar koyabiliriz.
select FirstName �sim, LastName Soyisim from Employees where FirstName like 'j%' -- isminin ba� harfi 'j' olan personellerin ad� ve soyad�
select FirstName �sim, LastName Soyisim from Employees where FirstName like '%y' -- isminin son harfi 'y' olan personellerin ad� ve soyad�
select * from Employees where FirstName like '%ert' -- isminin son 3 harfi 'ert' olan personeller
select * from Employees where FirstName like 'r%t' -- isminin ilk harfi 'r', son harfi 't' olan personeller
select * from Employees where FirstName like 'r%' and FirstName like '%t' -- yukar�daki kullan�m�n uzat�lm�� hali
select * from Employees where FirstName like '%an%' -- isminde 'an' ge�en personeller
select * from Employees where FirstName like 'n%an%' -- isminin ba� harfi 'n' olan ve ismin i�erisinde 'an' olan personeller
select * from Employees where FirstName like 'n%' and FirstName like '%an%' -- yukar�daki kullan�m�n uzat�lm�� hali  
select * from Employees where FirstName like 'a_d%' -- isminin ilk harfi 'a' olan, 2. harfi farketmez, 3. harfi 'd' olan personeller
select * from Employees where FirstName like 'm___a%' --isminin ilk harfi 'm', 2,3,4. harfi fark etmeyen, 5. harfi 'a' olan personeller
select * from Employees where FirstName like '[mnr]%' -- isminin ilk harfi m,n veya r olan personeller
select * from Employees where FirstName like '%[ai]%' -- isminin i�erisinde a veya i ge�en personeller 
select * from Employees where FirstName like '[a-k]%' -- isminin ba�harfi a-k aras�ndaki harfler olan personeller
select * from Employees where FirstName like '[^a]%' -- isminin ba�harfi a olmayan personeller
select * from Employees where FirstName like '[^an]%' -- isminin ba�harfi an olmayan personeller
select * from Employees where FirstName like '[_]%' -- isminin ba�harfi '_' olan personeller
select * from Employees where FirstName like 'k_%' escape 'k' -- isminin ba�harfi '_' olan personelleri k escape karakteri ile belirtiyoruz 

-- Aggregate Fonksiyonlar
select avg(EmployeeID) Ortalama from Employees -- personellerin ID'lerinin ortalamas�
select max(EmployeeID) MaxID from Employees -- en b�y�k EmployeeID
select min(EmployeeID) MinID from Employees -- en k���k EmployeeID
select count(*) from Employees -- verilen kolondaki veri say�s�
select count(FirstName) from Employees -- isim kolonundaki veri say�s�
select sum(Freight) from Orders -- nakliye �cretlerinin toplam�
