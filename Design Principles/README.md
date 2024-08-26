# Kaynak

- [Gençay Yıldız - Design Principles](https://youtube.com/playlist?list=PLQVXoXFVVtp2eAq33DVNxeoXLXj4VMYpT&si=3lN54LR1JKoc8pzy)

# Loose Coupling (Esnek Bağ) Nedir?

- OOP uygulamalarında işlevlerin gerçekleştirilebilmesi için nesneler üzerinden operasyonlar gerçekleştirilir. Bu nesneler kendi aralarında iş birliği yaparak, birbirlerine hizmet sunmakta ve her biri görevlerini yerine getirerek uygulamayı meydana getirmektedirler. Bu iş birlikleri neticesinde nesneler arasında ister istemez bağımlılıklar meydana gelebilir.

    Bir nesne, kullanıldığı diğer bir nesne hakkında ne kadar detaya/bilgiye sahipse, o nesneye olan bağımlılık o oranda artmakta ve olası bir değişiklik yahut onarılma durumunda bağımlı olan sınıfta da revizyon gerekecektir.

- Aslında SOLID kavramlarının odaklandığı, çözmeye çalıştığı temel prensip Loose Coupling'dir.
- Hedeflediğimiz şey nesneler arasındaki bağımlılıkların tamamen kaldırılması değil, bunun yapılabilmesi de pek mümkün değil. Hedefimiz nesneler arasındaki bağımlılıkların esnek/yönetilebilir olmasını sağlamak.
- Nesneler arasında bu esnek bağımlılığı için abstraction kullanarak sağlayabiliriz. C# için abstraction `abstract class` ile veya  `interface` ile yapılabilir.

    Sıkı bağımlılık örneği:
    ```cs
    public class MailSender
    {
        public void Send()
        {
            //Gmail gmail = new Gmail();
            //gmail.Send("ahmet","text");

            //Hotmail hotmail = new Hotmail();
            //hotmail.SendMail("ahmet","text");

            Yandex yandex = new Yandex();
            yandex.SendEMail("ahmet","text");
        }
    }

    public class Gmail
    {
        public void Send(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }

    public class Hotmail
    {
        public void SendMail(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }

    public class Yandex
    {
        public void SendEMail(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }
    ```

    Bu örnekte şunu görebiliriz. `MailSender` sınıfı bir mail gönderebilmek için bir mail server'a bağımlı. Oluşan dezavantajlar:
    - Burada biz kullandığımız mail server'ı değiştirmek istediğimizde MailSender direkt olarak tekrar düzenlenmek zorunda kalıyor. 
    - Kullanılan server hakkında detaylı bilgiye ihtiyacımız var, en basitinden örnekteki her server'ın mail gönderen fonksiyonunun ismi farklı.
  
    Esnek bağımlılık örneği:
    ```cs
    public class MailSender
    {
        public void Send(IMailServer mailServer)
        {
            mailServer.Send("ahmet","text");
        }
    }

    public interface IMailServer
    {
        public void Send(string to, string body);
    }

    public class Gmail: IMailServer
    {
        public void Send(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }

    public class Hotmail: IMailServer
    {
        public void Send(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }

    public class Yandex : IMailServer
    {
        public void Send(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }
  ```

  Bu örnekte ise artık MailSender bir IMailServer'a esnek bağımlı. Avantajları:
  - Server'ların mail gönderme işlemleri hakkında bilgi sahibi olmamıza gerek kalmıyor. 
  - Herhangi bir mail server içerisinde değişiklik yapılsa da MailSender bundan etkilenmiyor olacak.
  
<br/>

# Single Responsibility Principle (Tek Sorumluluk Prensibi) Nedir?

- OOP tasarımlarında bir sınıfın veya metodun tek bir sorumluluğa odaklı inşa edilmesi gerektiğini savunan prensiptir.
- Eğer bir sınıf veya metod birden fazla sebepten dolayı değiştirilmek zorunda kalınıyorsa, bu sınıfın bu prensibe uymadığını anlayabiliriz.
- Birden fazla sorumluluk üstlenen bir sınıf olduğunda, bu sorumluluklardan birisinde yapılacak değişiklik diğer sorumlulukları etkileyebilir. Bu durumlara **kırılgan tasarımlar** denmektedir.

    Örnek olarak SRP ile uyuşmayan bir örnek:
    ```cs
    public class Database
    {
        public string State { get; set; }
        public void Connect()
        {
            //
        }
        public void Disconnect()
        {
            //
        }
        public void GetPersons()
        {
            //
        }        
    }
    ```

    SRP persibine uygun bir örnek:
    ```cs
     public class Database
    {
        public string State { get; set; }
        public void Connect()
        {
            //
        }
        public void Disconnect()
        {
            //
        }         
    }

    public class PersonService
    {
        public void GetPersons()
        {
            //
        }     
    }
    ```    
<br/>

# Open/Closed Principle (Açık/Kapalı Prensibi) Nedir?

- Bu prensibe göre bir sınıf gereksinimlere göre değiştirmeye gerek kalmadan, genişletilebilir bir şekilde hazırlanmalıdır. Buradaki **open** aslında **genişletilmeye açık**, **closed** ise **değiştirilmeye kapalı** anlamına gelmektedir.
- Sınıflar için gereksinimlerin değişmesi gayet normal bir durumdur. Val olan bir operasyonu değiştirerek gereksinimi karşılamak yerine, ihtiaç duyulan yeni operasyonu sınıfa ekleyerek ilerlemeliyiz.

    Bu prensip ile çelişen örnek:
    ```cs
    public class ParaGonderici
    {
        var garantiBankası = new Garanti();

        garantiBankası.HesapNo = "123";
        garantiBankası.ParaGonder(500);

        //veya

        var ziraatBankası = new Ziraat();
        ziraatBankası.ParaTransferi(500, "123");
    }

    public class Garanti
    {
        public string HesapNo { get; set; }
        
        public void ParaGonder(int tutar)
        {
            //
        }
    }

    public class Ziraat
    {
        public bool ParaTransferi(int tutar, string hesapNo)
        {
            ///
        }
    }   
    ```
    Burada gereksinimlere göre farklı, yeni banklar ile çalışmamız gerekirse yeni banklar tanımlayıp onların kendi özelliklerini kullanabilmemiz için `ParaGonderici` sınıfını sürekli değiştiriyor olmamız gerekecektir.


    Bu prensibe uygun örnek:
    ```cs
    public class ParaGonderici
    {
        public void Gonder(IBanka banka, int tutar, string hesapNo)
        {
            banka.ParaTransferi(tutar, hesapNo);
        }
    }

    public interface IBanka
    {
        bool ParaTransferi(int tutar, string hesapNo);
    }

    public class Garanti : IBanka
    {
        public string HesapNo { get; set; }
        
        public void ParaGonder(int tutar)
        {
            //
        }

        public bool ParaTransferi(int tutar, string hesapNo)
        {
            ///
        }
    }

    public class Ziraat : IBanka
    {
        public bool ParaTransferi(int tutar, string hesapNo)
        {
            ///
        }
    }
    ```

    Örnek olarak gereksinimlerimizde bir değişiklik oldu ve artık Finans bankası ile de çalışabilmek istiyoruz. `ParaGonderici` sınıfı bu gereksinim değişikliğinden etkilenip değişikliğe uğramadan, kodumuzu genişleterek `IBanka` interface'i ile `Finans` sınıfını oluşturmamız yeterli olacaktır.

    Görüleceği gibi aslında bu prensip de **Loose Coupling**`'i destekleyen bir prensiptir.

<br/>

# Liskov Subsitution Principle (Liskov Yerine Geçme Prensibi) Nedir?

- Bu prensip, ortak bir referanstan türeyen nesnelerin hiçbir şeyi bozulmadan, patlamadan birbirleriyle değiştirilebilmesinin gerektiğini yani bir bir leri yerine geçebilmesi gerektiğini öneren prensiptir.
- Bir class eğer türediği referanstan elde ettiği member'lardan herhangi birisini kullanmıyorsa burada bir problem vardır. Bu prensibe göre referans alınan/sözleşme yapılan tanımdaki bütün memberlar kullanılıyor olmalı.
- Hiçbir alt sınıf uygulamış olduğu base referans'ın member'larını ihmal etmemelidir. Yani implement veya override edilen hiçbir metot boş kalmamalı, boş kalmasın diye exception fırlatılmamalı. Eğer bu şekilde bir kullanım yapılırsa, bu nesneler birbirlerinin yerine geçebilirler evet lakin çalışma anında hata almalar yaşanabilir kısaca patlamalar, çatlamalar olabilir.
- Liskov'un bize önerdiği şey, yaptığımız türetme işlemleri sonrasında başka bir yerde ekstra kontroller yapılmasına gerek duyulmasın. Örnek olarak A referansından türeyen B,C,D,E sınıfları olsun. Bu türeyen sınıflardan D'nin içerisinde A referansındaki `Metot1()` metodu kullanılmadığı için içerisinde bir exception fırlatılıyor veya içerisi boş bırakılıyor olsun. Developer bu durumu biliyorsa, bu A referansını kullandığı bir yerde **"eğer A referansı bir D objesi ise şöyle yap"** benzeri kontrol yapmak zorunda kalacaktır. Bu aslında bize yaptığımız çalışmada bir sorun olduğunu gösteren bir işlemdir. Liskov prensibi böyle bir durumun olmaması gerektiğini söyler.

    Örnek olarak alt kısımda bu prensibe uygun olmayan bir çalışmayı görebiliriz:
    
    ```cs
    public abstract class Cloud
    {
        public abstract void Translate();
        public abstract void MachineLearning();
    }

    public class AWS : Cloud
    {
        override public void MachineLearning()
        {
            Console.WriteLine("AWS MachineLearning");
        }

        override public void Translate()
        {
            Console.WriteLine("AWS Translate");
        }
    }

    public class Azure : Cloud
    {
        override public void MachineLearning()
        {
            Console.WriteLine("Azure MachineLearning");
        }

       
        override public void Translate()
        {
            throw new NotImplementedException();
        }

        // veya
        //override public void Translate()
        //{
            
        //}
      
    }

    public class Google : Cloud
    {
        override public void MachineLearning()
        {
            Console.WriteLine("Google MachineLearning");
        }

        override public void Translate()
        {
            Console.WriteLine("Google Translate");
        }
    }
    ```

    Burada Azure cloud nesnesinin `Translate()` operasyonunu yapmadığı görebiliyor fakat class içerisinde bu operasyon hala bulunuyor.

    Bir de bu örneği LSP için uygun hale getirerek inceleyelim:

    ```cs
    public abstract class Cloud
    {
        public abstract void MachineLearning();
    }

    public interface ITranslatable
    {
        void Translate();
    }

    public class AWS : Cloud, ITranslatable
    {
        override public void MachineLearning()
        {
            Console.WriteLine("AWS MachineLearning");
        }

        public void Translate()
        {
            Console.WriteLine("AWS Translate");
        }
    }

    public class Azure : Cloud
    {
        override public void MachineLearning()
        {
            Console.WriteLine("Azure MachineLearning");
        }      
    }

    public class Google : Cloud, ITranslatable
    {
        override public void MachineLearning()
        {
            Console.WriteLine("Google MachineLearning");
        }

        public void Translate()
        {
            Console.WriteLine("Google Translate");
        }
    }
    ```

    Görüldüğü üzere artık her class sadece gerçekten yapabildiği operasyonları barındırıyor, hiçbir class içerisinde gereksiz bir member yok.

<br/>

# Interface Segregation Principle (Arayüz Ayrım Prensibi) Nedir?

- Oluşturulan interface'ler/sözleşmeler berlirli bir olguya ait member'ları içermelidir. Bir sözleşme içerisinde anlamsız, farklı amaçlı luzumsuz member'lar olmasın.
- Farklı davranışlar farklı interface'lerde tanımlansın.

    Örnek olarak bu prensibe uymayan bir çalışma:

     ```cs
    public interface IPrinter
    {
        public void Print();
        public void Scan();
        public void Fax();
        public void PrintDuplex();
    }

    public class Brand1 : IPrinter
    {
       public void Print()
       {
            Console.WriteLine("Brand1 Print");
       }
       public void Scan()
       {
            Console.WriteLine("Brand1 Scan");
       }
       public void Fax()
       {
            Console.WriteLine("Brand1 Fax");
       }
       public void PrintDuplex()
       {
            Console.WriteLine("Brand1 PrintDuplex");
       }
    }

    public class Brand2 : IPrinter
    {
        public void Print()
        {
            Console.WriteLine("Brand2 Print");
        }
        public void Scan()
        {
            throw new NotSupportedException();
        }
        public void Fax()
        {
            Console.WriteLine("Brand2 Fax");
        }
        public void PrintDuplex()
        {
            throw new NotSupportedException();
        }      
    }

    public class Brand3 : IPrinter
    {
        public void Print()
        {
            Console.WriteLine("Brand3 Print");
        }
        public void Scan()
        {
            Console.WriteLine("Brand3 Scan");
        }
        public void Fax()
        {
            Console.WriteLine("Brand3 Fax");
        }
        public void PrintDuplex()
        {
            throw new NotSupportedException();
        }
    }
    ```

    Örnekte görüldüğü gibi interface'in düzgün tasarlanmaması class'ların gereksiz memeber içermesine neden oluyor, LSP için de yanlış bir durum ortaya çıkıyor. 

    ISP için uygun olan çalışma örneği:

     ```cs
    public interface IPrint
    {
        public void Print();
    }
    public interface IScan
    {
        public void Scan();
    }
    public interface IFax
    {
        public void Fax();     
    }
    public interface IPrintDuplex
    {
        public void PrintDuplex();     
    }

    public class Brand1 : IPrint, IScan, IFax, IPrintDuplex
    {
       public void Print()
       {
            Console.WriteLine("Brand1 Print");
       }
       public void Scan()
       {
            Console.WriteLine("Brand1 Scan");
       }
       public void Fax()
       {
            Console.WriteLine("Brand1 Fax");
       }
       public void PrintDuplex()
       {
            Console.WriteLine("Brand1 PrintDuplex");
       }
    }

    public class Brand2 : IPrint, IFax
    {
        public void Print()
        {
            Console.WriteLine("Brand2 Print");
        }

        public void Fax()
        {
            Console.WriteLine("Brand2 Fax");
        }     
    }

    public class Brand3 : IPrint, IScan, IFax
    {
        public void Print()
        {
            Console.WriteLine("Brand3 Print");
        }
        public void Scan()
        {
            Console.WriteLine("Brand3 Scan");
        }
        public void Fax()
        {
            Console.WriteLine("Brand3 Fax");
        }
    }
    ```

    Burada görüldüğü üzere artık class'lar gereksiz memeber tanımlamıyor ve LSK için uygun bir çalışma yapılmış oluyor. Burada önemli nokta interface'lerin bağımsız davranışlara göre düzgün ayrılmasıdır.

<br/>

# Dependency Inversion Principle (Bağımlılığın Tersine Çevrilmesi Prensibi) Nedir?

- Bir sınıfın herhangi bir türe olan bağımlılığını tersine çevirmeyi öneren prensiptir.
- Geliştiricinin bir türe bağımlı kalmasını değil, türlerin geliştiriciye bağımlı olduğunu savunur.
- Örnek olarak askeriyeyi düşünebiliriz. Mesela bir komutan işleri yaptırmak için sadece Ahmet isimli askeri kullanıyor olsun. Burada komutanın yapması gereken bütün işleri Ahmet'in karşılayabilmesi işleri zorlaştıracaktır. Örnek olarka komutan Ahmet'e bir görev verdi ve Ahmet çalışmaya başladı. Komutan elindeki diğer işleri yaptırabilmek için Ahmet'i beklemek zorunda kalır, yani Ahmet'e bağımlı kalır. Fakat burada komutan direkt olarak Ahmet'e değilde **Asker** kavramına bağımlı olsa, yani Asker olarak tanımlanan herhangi birisini işleri yaptırmak için kullanabiliyor olursa artık komutanın işlerini yaptırabilmesi daha kolay olacaktır. Bu işlem sonunda baktığımızda artık Asker'lerin komutana bağımlı olduğunu görürüz, yani bir komutan herhangi bir askeri seçerek iş yaptırabilir. İlk baştakı komutanın Ahmet isimli bir askere bağımlı olması durumu tersine çevrilmiş olur, artık asker kavramındaki herkes komutana bağımlıdır.
- Bir sınıfın somut class'lara bağımlı olmasını değil, o somut class'ların soyut kavramlarına (interface'lerine) bağımlı olmayı sağlarsak artık bu prensibe uyarak bağımlılığı tersine çevirmiş oluruz.

    Örnek olarak dependency inversion'a uygun bir çalışma:
    ```cs
    public class MailSender
    {
        public void Send(IMailServer mailServer)
        {
            mailServer.Send("ahmet","text");
        }
    }

    public interface IMailServer
    {
        public void Send(string to, string body);
    }

    public class Gmail: IMailServer
    {
        public void Send(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }

    public class Hotmail: IMailServer
    {
        public void Send(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }

    public class Yandex : IMailServer
    {
        public void Send(string to, string body)
        {
            // mail gönderme işlemleri.
        }
    }
  ```
