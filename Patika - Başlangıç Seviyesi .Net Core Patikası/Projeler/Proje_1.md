
1. [22,27,16,2,18,6] -> Insertion Sort

    Yukarı verilen dizinin sort türüne göre aşamalarını yazınız.

    Big-O gösterimini yazınız.

    Dizi sıralandıktan sonra 18 sayısı hangi case kapsamına girer? Yazınız.

    Time Complexity:
    Average case: Aradığımız sayının ortada olması,
    Worst case: Aradığımız sayının sonda olması, 
    Best case: Aradığımız sayının dizinin en başında olması.

    ## Çözüm
    ---

    İnsertion Sort için adımlar:

        0. [22,27,16,2,18,6]

        1. [2,27,16,22,18,6]

        2. [2,6,16,22,18,27]

        3. [2,6,16,22,18,27]

        4. [2,6,16,18,22,27] (Dizi sıralanmış oldu)

    Burada Patika eğitiminde anlatılan şekilde sıraladım. İnternetteki diğer kaynaklarda farklı işem yapılıyor. Eğer öyle sıralamış olsaydık adımlar:

        0. [22,27,16,2,18,6]

        1. [22,27,16,2,18,6]  

        2. [22,27,16,2,18,6]  

        3. [16,22,27,2,18,6]   

        4. [2,16,22,27,18,6]   

        5. [2,16,18,22,27,6] 

        6. [2,6,16,18,22,27]        

    Şeklinde olacaktır. 
    
    <br>   

    Big-O gösterimi: `O(n^2)`

    Dizi sıralandıktan sonra 18 sayısı `Average Case`' e girer. Çünkü dizinin ortalarında-ortasında yer almaktadır.

---
<br>

2. [7,3,5,8,2,9,4,15,6] dizisinin Insertion Sort'a göre ilk 4 adımını yazınız. 

    ## Çözüm
    ---
    
        0. [7,3,5,8,2,9,4,15,6]

        1. [2,3,5,8,7,9,4,15,6]

        2. [2,3,5,8,7,9,4,15,6]

        3. [2,3,4,8,7,9,5,15,6]

        4. [2,3,4,5,7,9,8,15,6]

    Yukarıdaki adımlar Patika'daki eğitimde gösterilen şekilde yapıldığında oluşan adımlar. Anlatılan diğer yöntem ile sıralarsak ilk 4 adım:

        0. [7,3,5,8,2,9,4,15,6]

        1. [7,3,5,8,2,9,4,15,6]

        2. [3,7,5,8,2,9,4,15,6]

        3. [3,5,7,8,2,9,4,15,6]

        3. [3,5,7,8,2,9,4,15,6]

    Şeklinde olacaktır.








