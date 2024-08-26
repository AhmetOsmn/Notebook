# Auto Mapper Kütüphanesi

- Kurulmasi gereken 2 adet paket alt kisimdadir:

    ![image](https://user-images.githubusercontent.com/44196434/187048071-d84af6e7-b374-48cc-9d50-dfdcc6e3731f.png)


- Yazdigimiz projelerde proje disarisindan veri alirken, veya elimizdeki veri setinden verileri gosterirken DTO, View Model'ler vb. yapilari kullaniriz. Bu tur yapilarin amaclarindan birisi sudur:

    Ornek olarak elimizde bir **Urun** tablosu olsun. Bir kullanicinin sisteme urun ekleme islemi yapmasi gerektigi bir durumda oldugumuzu dusunelim. Bizim db icerisindeki **Urun** tablomuzdaki ozelliklerden sadece sinirlandirdigimiz kisimlarin kullanici tarafindan doldurulmasini isteriz. 
    
    Mesela bir urunun ID ozelligi kullanici tarafindan degil, sistemde otomatik olarak verilmesini istiyoruz. Bu tur senaryolarda Bir `UrunEkleDTO` tarzinda sinif olusturup, kullaniciyi sinirlandirmis oluruz.

    ```csharp
    public class Urun
    {
        [Key]
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public int KategoriID { get; set; }

        [ForeignKey("KategoriID")]
        public Kategori Kategori { get; set; }
    }
    ```

    ```csharp
    public class UrunEkleDTO
    {
        public string UrunAdi { get; set; }
        public int KategoriID { get; set; }
    }
    ```
    
- Proje icerisinde yukaridakine benzer orneklerle karsilasiriz. Bu tarz durumlarla karsilastigimizda DTO class'lari ile ana class'lar arasinda donusumler yapilmasi gerekir. Bu donusumleri kendimiz ozel class'lar yazarak da yapabiliriz. Ornek olarak:

    ```csharp
    public static class Mapping
    {
        public static Urun UrunDTOToUrun(UrunDTO dto)
        {
            return new Urun()
            {
                UrunAdi = dto.UrunAdi,
                KategoriID = dto.KategoriID
            };
        }
    }
    ```
- Ornekteki gibi kendi class'larimizi olusturarak ve kendi metotlarimizi yazarak mapping islemlerini yapabiliriz. Ama proje buyudukce ve karmasik hale geldikce, sinif sayisi ve DTO sayisi artinca, siniflarin icerisindeki property sayilari artinca bu siniflari yazmak yerine `AutoMapper` kutuphanesini kullanmamiz oldukca fayda saglar.

- `AutoMapper` kutuphanesinin amaci verilen ornekten de anlasildigi gibi nesneleri birbirlerine map'lemektir. Yani bir nesnenin property'lerini diger nesnenin property'lerine atama yapar. 
- `AutoMapper` Tip uyumlulugu olan ve isimlendirmeleri uyumlu olan property'leri otomatik olarak mapler. Bazi durumlarda kutuphanenin sagladigi metotlari kullanarak da mapping yapmamiz gerekir. `AutoMapper` class'ina bir ornek verelim:

    ```csharp
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Urun, UrunEkleDTO>();
            CreateMap<UrunEkleDTO, Urun>();
        }
    }
    
    ```

- Mapping yapilacak siniflar bu sekilde bir `MappingProfile` sinifinin icerisinde tanimlanir. Daha sonrasinda proje icerisinde bir `IMapper` nesnesi olsuturulur ve mapping yapilir. Ornek olarak:

    ```csharp
    public class Test
    {
        private readonly IMapper _mapper;

        public Test(IMapper mapper)
        {
            _mapper = mapper;
        }

        UrunEkleDTO dto = new UrunEkleDTO(){  UrunAdi="test urun", KategoriID=22};
        Urun urun = _mapper.Map<Urun>(dto);
    }
    ```

# Kaynak

[Gençay Yıldız - AutoMapper](https://www.gencayyildiz.com/blog/asp-net-coreda-automapper-kullanimi/)
