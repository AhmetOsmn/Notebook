# Kaynaklar

- [Github Nedir? - Microsoft](https://docs.microsoft.com/tr-tr/training/modules/introduction-to-github/2-what-is-github)
- [GitHub'a Giriş](https://github.com/skills/introduction-to-github)

<br>

# Giriş

Merhaba, bu yazı içerisinde GitHub ve nasıl kullanıldığı hakkında bilgiler vereceğiz.

<br>

# Git Nedir?

- Git bir versiyon kontrol sistemidir. Yani basit olarak açacak olursak: Projenin geliştirilme sürecinde işaretlenen tarihteki haline erişme ihtiyacımız olabilir. Örnek olarak bir proje geliştirdiğimizi düşünelim. Proje zaman ilerledikçe büyüyecek, değişecek ve daha karmaşık hale gelecektir. Bu süreç içerisinde bazı nedenlerden dolayı projenin `x` gün önceki haline dönmemiz veya bir kod parçasının `x` gün önceki haline erişmemiz gerekebilir. Git teknolojisi aslında bizim bu ihtiyacımız karşılamak için çok uygun bir teknolojidir. İlerleyen kısımlarda bahsedilecek olan `commit`ler sayesinde, projenin geçmişte `commit` atılmış haline kolayca erişebiliyoruz. Eğer Git'i kullanmıyorsak böyle bir ihtiyacı giderebilmek için projeyi geliştirme aşamasında v1, v2, v3, vb. şekilde klasörlememiz gerekecektir.

- Git kullanımının başka bir güzel avantajı projeyi takım olarak geliştirmeyi kolaylaştırır. Örnek olarak 4 modülden oluşan bir proje geliştirdiğimizi düşünelim. Takımdaki 4 kişi birer modül seçer ve geliştirmeye başlar. Git içerisinde bulunan `branch` yapısı ile oldukça basit bir şekilde ayrı çalışmalar yapılabilir ve en son modüller tamamlandıkça `merge` edilerek proje içerisinde birleştirilirler ve proje tamamlanmış olur. 


- Son olarak ilk başlarda GitHub ile Git'in aynı şey olduğu sanılsa da aslında Git başlı başına bir teknolojidir, GitHub ise bu teknolojiyi kullanıcıların kullanımı için hazırlanan arayüzler ile Git kullanımını kolaylaştırmayı hedefleyen bir servistir. Bunu şu şekilde görebiliriz, GitHub ve GitLab ortak olarak Git teknolojisini kullanır ama kendileri ayrı servislerdir. İkisininde kendilerine göre avantajları vardır ve kullanıcı ihtiyacına göre Git teknolojisini istediği servis üzerinden kullanabilir.

<br>

# GitHub Nedir?

- Yukarıda bahsedildiği gibi GitHub, Git teknolojisini kullanıcının arayüzler ile kullanmasını sağlayan bir bulut servisidir. Projeleri uzak depolarda geliştirmemizi sağlar. 

- Üst kısımda verilen 2 örnek tahmin edileceği üzere GitHub ile rahatça gerçekleştirilebilir. 

- GitHub içerisinde eklediğimiz projeleri gizli veya herkese açık olarak paylaşabiliriz. Bu tamamen kullanıcının isteğine bağlıdır. Eğer kullanıcı geliştirdiği projeyi diğer insanların görmesini istemiyorsa, GitHub servisini sadece versiyon kontrol sistemi olarak istiyorsa projeyi gizli olarak servise ekleyebilir.

    Kullanıcı geliştirdiği projeyi herkese açık olarak ta paylaşabilir. İnsanların kendisinin geliştirdiği projeleri görmesini, bu projelere katkıda bulunulmasını isteyebilir. Bu kısımda Açık Kaynak (Open Source) Proje kavramı giriyor. Açık Kaynak konusu ayrı bir konu olduğu için detaylarına girmiyoruz.

- GitHub üzerinde herkese açık olarak bir proje paylaştığımız zaman, diğer kullanıcılar bu projeyi kendi bilgisayarlarına kolaylıkla indirebilirler.

<br>

# GitHub üzerinden neler yapılabilir?

- `Sorunlar Alanı: ` Projenin kullanıcıları ile projenin geliştiricileri arasındaki iletişim genelde bu alan üzerinden olur. Kullanıcılar projeye eklenmesini istedikleri özellikleri, gördükleri hataları vb. durumları bu alandan projeyi geliştiren kişilere belirtirler. 

- `Dal (branch): ` Bu kısım aslında geliştiricileri ilgilendiren kısımdır. Projeyi yukarıda örneklendirdiğimiz şekilde, takım olarak proje geliştirirken dallar üzerinde çalışmak aynı anda geliştirme yaparken kodların çakışmasını önler. Bu çakışma olayı `conflict` olarak adlandırılır. 

    Genelde projelerde sabit,kararlı olan bir dal olur, geliştiriciler tamamladıkları kendi dallarını bu kararlı dal ile birleştirirler. Birleştirme işleminden önce kararlı dala gelen yan dal önce kontrollerden geçer ve onaylandığı zaman kararlı dal ile birleştirilir.

    Dallanmaları kullanılan IDE üzerinden veya GitHub'ın kendi içerisinden takip edip, dallar arasında işlemler yapabiliriz.

- `Commit'ler: `Commit'leri projenin geliştirme sürecinde proje içerisine atılan işaretler olarak düşünebiliriz. Proje üzerinde değişikliker yaptığımızda commit'ler atarak kendizmiz için veya takımımız için projede geriye dönüş noktaları oluşturmuş oluruz. Ayrıca commit sistemi projenin gelişimini daha okunabilir ve daha takip edilebilir hale getirir.

- `Pull Requests: ` Projedeki kararlı dala birleştirilmeye hazır bir yan dal eklenmek isteniyorsa *pull request* oluşturulur. Buradan anlaşılacağı üzere takımdaki herhangi birisinin bitirdiği dalı direkt olarak kararlı dal'a birleştirmesi engellenir (Bunun için **Onay Gerekli** olarak ayarlanmalıdır).

     Kararlı dal'a olabildiğince herhangi bir sorun çıkartmayacağını düşündüğümüz ve testlerden geçmiş, kontrol edilmiş kodların eklenmesini istiyoruz. Bu nedenle yan dallar kararlı dal ile birleştirilmeden önce (genelde takım liderleri tarafından veya projeye hakim bir kişi tarafından) kontrollerden geçer. 

    Yukarıdaki kontrol aşamasında eğer yan dal ile kararlı dal arasında bir çakışma (conflict) oluşursa GitHub zaten uyarı verecektir. Böyle durumlarda çakışmaların olduğu alanlardaki kodlar incelenir ve çakışmayı ortadan kaldıran düzenlemer yapılır ve iki dal birleştirilir (merge edilir).

<br>

# GitHub'ı Kullanalım

- Projelerde GitHub hizmetlerini kullanmanın farklı yolları vardır. Git Bash kullanarak, Git Desktop kullanarak, Visual Studio veya VS Code gibi programları kullanarak, eğer Linux tabanlı bir işletim sisteminiz var ise direkt console üzerinden vb. yollar ile GitHub hizmetlerini kullanabiliriz. Şimdilik `Git Bash` ve `Visual Studio` örneklerini göstereceğiz.

<br>

### Git Bash

- 