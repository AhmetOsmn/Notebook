# Kaynaklar

- [React Dersleri](https://youtube.com/playlist?list=PL8IHDq7oEkgFKYIoNuubfZMuhhgEukkAg)
- [PROTOTURK](https://youtube.com/playlist?list=PLfAfrKyDRWrGXWpnJdyC4yXIW6v-PcFu-)

# JSX Nedir?

- JSX, HTML ile birlikte JS kullanımını kolaylaştıran eklentidir. 

    Örnek olarak alt kısıma bakabiliriz:

    ```js   
    <h1 className="title">Selam</h1>
    ```

    Yukarıda tanımlanan `h1` etiketi, Babel derleyicisi ile alt kısımdaki şekle dönüştürülür:

    ```js
    React.createElement(
        'h1',
        {className: 'title'},
        'Selam'
    );
    ```

    Babel, yeni teknolojiler ile yazılan JS kodları, tüm tarayıcıların çalıştırabileceği eski JS kodlarına çevirir.

# Element

- Elementler React içerisindeki en küçük yapılardır. 

- React'ta bir element oluşturduğumuzda arka tarafta `ReacDOM.render()` kullanılır. Eğer bir element'i güncellersek ise aynı fonksiyon, yeni element ile tekrar tetiklenir. Burada önemli olan kısım bütün element'i kontrol etmek yerine sadece değişen kısımlar güncellenir. 

# Bileşen (Component)

- Bileşenler (component'ler) ise elementlerden oluşan yapılardır. Birbirinden bağımsız olarak farklı yerlerde kullanılabilen arayüz parçalarına denir.

- İki şekilde bileşen oluşturulabilir:

    - Sınıf (class) bileşenleri,
    - Fonksiyonel (function) bileşenler

# Props (Properties)

- Component'ler arasında bilgi aktarmamızı sağlayan, custom olarak oluşturduğumuz elementlerdir (Her zaman büyük harf ile başlamalı).

- Prop'lar sadece okunabilirler, değiştirilemezler.

- Örnek olarak:

    ```js
    <Selam isim="Ahmet" />
    ```

    `Selam` component'i içerisine bir isim değeri gönderiyoruz. Bu değere `Selam` component'i içerisindeki `props` yapısını kullanarak erişebiliyoruz.

    ```js
    export default function App(){
        return (
            <div className="App">
                <h1>React Dersleri</h1>
                <Selam isim="Ahmet"/>
            </div>
        );
    }

    function Selam(props){
        return <div>Selam {props.isim}!</div>;
    }

    ```

- Props değerlerine default değerler atanabilir.

    ```js
    function Selam(props){
        return 
            <div>
                <p>Selam {props.isim || "misafir"}!</p>
                {props.children}
            </div>;
    }
    ```

- Eğer bir component içerisine başka bir element göndermek istersek ise şu şekilde kullanabiliriz:

    ```js
    export default function App(){
        return (
            <div className="App">
                <h1>React Dersleri</h1>
                <Selam isim="Ahmet">
                    <h1>Child element</h1>
                </Selam>
            </div>
        );
    }

    function Selam(props){
        return 
            <div>
                <p>Selam {props.isim}!</p>
                {props.children}
            </div>;
    }

    ```

# State

- Dinamik bilgileri içerisinde tutan, özel bir React için özel bir JS objesidir. State'ler oluşturuldukları bileşenlere aittirler.

- State sadece okunur değildir, state değişliği yapılabilir. Bir bileşenin state'i değiştiğinde o bileşen güncellenmiş olur ve ekranda tekrar görüntülenir.

- Function componenet'larda state `useState()` fonksiyonu ile oluşturulur. Bu fonksiyon 2 elemanlı dizi döndürür. İlk eleman oluşturulan state'tir, ikinci eleman ise bu state'i güncellememizi sağlayan fonksiyon'dur.

    ```js
    const [isim, setIsim] = useState("");
    ```

    React'in state değişikliklerini yakalayabilmesi için güncellemelerin bu fonksiyonlar ile yapılması gerekiyor. Eğer isim direkt olarak '=' şeklinde güncellenirse React bu durumu yakalayamaz ve yeninden görüntüleme yapamaz.

# Yaşam Döngüleri (Life-cycle methods)

- Mounting (& unmounting): Component'i ekrana asmak (tablo asmak gibi) düşünebiliriz.
    
# useEffect Kullanımı

- 2 tane parametresi vardır. Birincisi çalıştırılacak olan fonksiyondur. İkincisi ise fonksiyona gönderilecek olan *dependency array* dir (dizi eleman olarak state veya props alabilir). 

    Bu array içerisine girilen elemanlar değiştiğinde, 1. parametredeki fonksiyon çalıştırılır.

- Eğer dependency array boş olarak verilirse, 1. parametredeki fonksiyon sadece bileşen ilk yüklendiğinde çalışır (componentDidMount gibi).

- Eğer diziye eleman verirsek, 1. parametredeki fonksiyon ilk yüklenmede ve dizideki elemanların her güncellenmesinde çalışır (componentDidUpdate'ten farkı ilk yüklemede de çalışmasıdır).

- Eğer dizi hiç girilmeyip sadece 1. parametredeki fonksiyon verilirse, her state değişiminde veya props değiştiğinde fonksiyon çalıştırılır (componentDidUpdate, şartsız).

- Eğer fonksiyonun içerisinde `return` ile bir fonksiyon döndürülürse, 1. parametredeki fonksiyon component ekrandan kaldırılmadan önce çalışır (componentWillUnmount).

# Form'lar

- Form elemanların içeriği, bileşen seviyesindeki state'de saklanır.

- Form'lardaki değişiklikler state üzerinden yapılır. Yapılan her değişiklik anlık olarak state'e kayıt edilir.

# State'i Yukarı Taşımak (Lifting state up)

- Eğer bir state parçası birden fazla bileşen tarafından kullanılıyorsa, en yakın bir üst ortak bileşene taşınmalıdır. 

    Üst bileşende bulunan ve alt bileşenler tarafından kullanılan state için `single source of truth (doğrunun tek kaynağı)` denir.

# useRef() ve forwardRef()

- Bir JSX elementini referans etmek istediğimizde `useRef()` kullanabiliriz. Örnek olarak:

    ```js
    function UseRefUsage() {

        const inputRef = useRef();

        const focusInput = () => {
            inputRef.current.focus();
        }

        return (
            <>
                <input type="text" ref={inputRef} />
                <button onClick={focusInput}>Focus</button>
            </>
        );
    }
    ```

- Eğer bir component'i referans etmek istersek ise `forwardRef()` kullanabiliriz. Örnek olarak:

    ```js
    function Input(props, ref){
        return <input type="text" ref={ref} {...props} />
    }

    Input = forwardRef(Input);

    //veya 👇 

    //const Input = forwardRef((props, ref) => {
    //    return <input ref={ref} type="text" {...props} />
    //}) 


    function ForwardRefUsage() {

        const inputRef = useRef();

        const focusInput = () => {
            inputRef.current.focus();
        }

        return (
            <>         
                <Input ref={inputRef} />
                <button onClick={focusInput}>Focus</button>
            </>
        );
    }

    ```

# useReducer()

- Karmaşık projelerde özellikle çok fazla state kullanılmaya başlanıldığında, bu yönetimi daha düzenli geliştirme yapabilmek için tercih edilir.

    ```js
    // const [todos, setTodos] = useState([]);
    // const [todo, setTodo] = useState('');

    // 👆 yerine 👇 

    const [state, dispatch] = useReducer(reducer, {
        todos: [],
        todo: ""
    });
    ```

    ***useReducer(`reducer`, {})*** kısmındaki `reducer` bir fonksiyondur. Bu fonksiyon içerisine **state ve action** gönderilerek yönetim sağlanır.  

    ***useReducer(reducer, `{}`)*** kısmındaki `{}` ise. Normalde state kullanarak kontrol ettiğimiz state parçalarının initial değerleridir.

    Örnek bir `reducer` fonksiyonu:

    ```js
    function reducer(state, action) {
        switch (action.type) {
            case 'SET_TODO':
                return {
                    ...state,
                    todo: action.value
                }

            case 'ADD_TODO':
                return {
                    ...state,
                    todo: '',
                    todos: [
                        ...state.todos,
                        action.todo
                    ]
                }
        }
    }
    ```

# Memoization

- Bir component'in performanslı bir biçimde render edilmesini sağlar. Örnek olarak X component'i içerisinde Y component'i kullanılıyor olsun. X component'i her state değişikliğinde render edileceğinden Y componentinde bir değişiklik yapılmasa bile (Y component'ine bir state gönderimi olmadığını düşünüyoruz) gereksiz olarak render edilecek. 

    Bu gibi durumlarda `memo` ile Y nin gereksiz yere render edilmesini önleyebiliriz. Örnek olarak:

    ```js
    import { memo } from "react";

    function Y() {

        console.log("Y rendered");

        return (
            <div>test</div>
        );
    }

    export default memo(Y);
    ```

    Yukarıdaki şekilde olduğu gibi kullanımda artık X component'i her render edildiğinde Y render edilmeyecektir.

- Eğer biz bir component'e prop olarak fonksiyon gönderirsek bu sefer `memo` işlevsiz kalacaktır. Değişmediğini düşündüğüm fonksiyonları da bellekte tutmak için `useCallBack()` fonksiyonunu kullanmalıyız.