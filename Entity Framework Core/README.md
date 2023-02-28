# Selamlar

Gençay Hoca'nın yayınladığı [EF Core eğitiminden](https://www.youtube.com/watch?v=dbI-kostQWo&list=PLQVXoXFVVtp1o3nq3-IXv42bPaFlzroBE&index=1) aldığım notlara alt kısımdan erişebilirsiniz.

<br>

# 1 - ORM Nedir (Object Relational Mapping - Nesne İlişkisel Eşleme)?

- Projenin veritabanı ile iletişime geçebilmesi için bir connection oluşturulmalı ve bu connection üzerinden veritabanı komutları çalıştırılmalıdır.

    Proje içerisinde direkt olarak yazılan sql komutları kirli kod yazılmasına, güvenlik açıklarına, bağımlılığa ve zor yönetilebilirliğe neden olur. 

    Özellikle veritabanı bağımlılığı oluşan ön önemli dezavantajdır.


- ORM yaklaşımı yukarıdaki dezavantajları çözebilmek için geliştirilmiş bir yaklaşımdır. Veritabanı ile veri transferi yapılırken, OOP'den olabildiğince faydalanmayı hedefler ve veritabanı bağımlılığını ve bilgisini azaltır.

    Proje içerisinde katı veritabanı sorgularını kullanmak yerine, veritabanında var olan objelerin (veritabanı, tablolar ve veriler) projede de OOP neseneleri olarak kullanılabilmesini sağlar.

    Yapılması gereken işlemler artık direkt olarak veritabanı komutları üzerinden yapılmak yerine, nesneler üzerinden yapılır.

- ORM kullandığımızda kullandığımız veritabanına uygun sorgular otomatik olarak oluşturulur.Bu durum geliştiricinin, projede kullanılan veritabanı bilgisinin çok iyi olmadan da proje geliştirilebilmesini de sağlar.

<br>

# 2 - Neden ORM Kullanmalıyız?


