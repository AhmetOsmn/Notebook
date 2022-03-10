# Docker

Bir proje içerisinde farklı servisler kullanılabilir. Her servisin kendisine ait olan bağımlılıkları ve kullandığı kütüphaneler olacaktır. Bu servislerin kütüphaneleri ve bağımlılıkları birbirlerini etkileyebilir. Ayrıca servislerin çalışabilmesi için uygun işletim sistemi üzerinde olmaları gerekmektedir. 

Bizim istediğimiz en iyi durum, bütün servislerin kütüphanelerinin ve bağımlılıklarının birbirinden ayrı olması ve birbirlerini etkilememesidir.
Eğer her servis kullandığı kütüphaneler ve bağımlılıkları ile birer paket içerisinde olursa aslında istediğimiz durumu elde ediyoruz.

-----------------ss1 gelecek------------------------


## Container Nedir?

Kendilerine air Prosesleri, Servisleri ve Ağları vardır. Aynı işletim sistemi veya VM üzerinde çalışırlar. İzole edilmiş ortamlardı.
2 Adet componentimiz olduğunda, ikisine de kendilerine ait olan bağımlılıkları ve kütüphaneleri koyduğumuzda bunlar birere ***Container*** olarak düşünülebilir. Bunun sonucunda bu iki component-container birbirlerine etki etmezler, bir nevi izole edilmiş olurlar.
Container'ların çalıştığı yerlerde sadece bir işletim sistemi vardır ve container'lar bu işletim sistemin kernel'ini (çekirdeğini) kullanırlar.

Container türleri olarak ***LXC, LXD, LXCFS*** vardır. Docker bunlardan ***LXC*** türünü kullanmaktadır. Container'lar kullanılması ve kontrolü zor olan yapılardır. Docker bizlere bu container'ların kullanılmasını kolaylaştırır ve kontrol edilebilir hale getirir.

