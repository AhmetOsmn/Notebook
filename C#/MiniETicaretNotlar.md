# Selamlar

Selamlar, alt kısımdaki kodlar Gençay Yıldız hocanın [şu](https://youtube.com/playlist?list=PLQVXoXFVVtp1DFmoTL4cPTWEWiqndKexZ) eğitim serisinden çıkartılmıştır. 

# 2

## Onion Architecture

- **Onion arch.** başlamadan önce, sürekli olarak kullanılan **N Layer Arch.** biraz inceleyelim:

![nlayer](./Images/MiniETicaret/basedNLayerArch.png)

    - Bu mimariyi kullanmamak için sebeplere baktığımızda karşımıza ilk çıkan şey değişikliğe dirençli bir yapıda olmasıdır.
    - Katmanlar arasında sıkı bağımlılık mevcuttur.
    - Büyük ölçekli ve karmaşık projeler için yetersizdir.
    - Data Access Katmanı merkezidir. Bu da uygulamada verilerin geliş tarzına bir bağımlılık oluşturur. Eğer verinin gelişi ile ilgili bir değişikliğe gidilmek istenirse oldukça masraflı bir durum ortaya çıkar.


