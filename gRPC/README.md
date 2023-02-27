# gRPC (Google Remote Procedure Call)

- Selamlar herkese. gRPC öğrenmeye başladım ve bu süreçte aldığım notları ve yaptığım örnek projeleri burada paylaşıyor olacağım. İlk olarak yukarıdaki başlıkta `'g'` harfinin `"Google"` olduğundan emin değilim ama Google geliştirdiği için öyle yazmamız yanlış olmaz sanırım.

- RPC yapılanması aslında yeni olmayan bir yapılanmadır. Popüler olmamasının sebebi REST'e göre yavaş olması ve belki de yetersiz kalması. Google 2015-2016 yıllarında RPC yapısını ele aldı ve kendisi bu yapının üzerinde çalışmalar yapıp geliştirmeye başladı. Gelinen son durumda `gRPC` ile hazırlanan sistemler, `REST` ile hazırlanan sistemlere göre önemli ölçüde hız farkına açmış durumda. Ama burada farklı senaryolara göre tercih etme olduğundan en hızlısı en iyisidir diyemiyoruz.

- gRPC'nin temel olarka bize sağladığı fayda şudur: Uzak sunucuda var olan bir fonksiyonları kendi ortamımızdaymış gibi kullanmamızı sağlar. 

- Microservice mimarilerinin dez avantajı olarak görülebilecek bir şey vardır. Monolithic mimarideki projelere göre daha yavaş kalırlar. Bunu olabildiğince azaltabilmek için şu şekilde bir yol izlenebilir: Mimari içerisinde sadece diğer servisler ile iletişimde olan ve dışarısı ile hiçbir şekilde iletişimde olmayan servisleri `gRPC` ile kullanabiliriz. Dışarısı ile iletişim halinde olan servisler `REST` olarak kalmaya devam edebilir. Bu sayede iç servislerin binary formatta iletişim kurması, microservice mimarisinin getirdiği yavaşlık sorununu azaltmak için güzel bir fırsata dönüşmüş olur.

- Transfer etmek istediğimiz verileri binary serialization ve deserialization protokolü olan `Protocol Buffers (Protobuf)` ile iletebiliriz. Ayrıca Protobuf dil bağımsızdır.

    <b>Biz proto dosyası içerisinde fonksiyon ve gerekli türleri tanımladıktan sonra projeyi build ettiğimizde, arka tarafta kullandığımız dile uygun olarak nesneler ve metotlad oluşturuluyor. Sonrasında yazdığımız servisler ile bu nesnelere/metodlara erişerek gerekli işlemleri gerçekleştiriyoruz. Burada şunu tekrar vurgulamak istiyorum, client ve server taraflarındaki `proto` dosyalarının tanımlamaları içermesi gerekmektedir.</b>

- `REST`'te sıkça kullanılan JSON ve XML formatları verileri text olarak tutarlar. Protobuf verileri binary olarak tutmamızı sağladığı için önemli ölçüde hız ve performans artışına neden olur.

- `gRPC`'de client ve server arasında bir çeşit sözleşme (contract) olmalıdır. Bu sözleşmeyi iki tarafta da aynı tanımlamalara sahip olan `proto` dosyaları ile sağlarız. Bu sözleşme içerisinde iletilecek mesajın türü, iletim yöntemi gibi tanımlamalar yapılır.

- `gRPC`'de client'lar RPC'nin `DEADLINE_EXCEEDED` hatası alıp sonlanmadan önce ne kadar bekleyebileceğini server'a belirtebilirler.

    Ayrıca server'lar RPC'nin zaman aşımına uğrayıp uğramadığını veya RPC'nin tamamlanması için ne kadar zaman gerektiğini sorgulayabilirler.

- `gRPC`'de client veya server RPC'yi iptal edebilir. İptal işleminde RPC hemen sonlanır fakat sonlanmasına kadar yapılan işlemler geri alınamaz. Yani Rollback sistemi yoktur.

- `proto` dosyalarında field'lara herhangi bir değer verilmezse field'ın sahip olduğu tipin default değeri kullanılır.

# gRPC vs REST

- `REST` servislerde geriye response dönülebilmesi için gönderilen request datasının tamamen işlenmesini beklememiz gerekir.

    `gRPC` servislerde ise gelen requesti parça parça işleyip, geriye parça parça yanıt dönmemiz mümkündür (`Data Stream`).

- `gRPC` servislerde encoding ve decoding işlemleri client tarafında yapılır, bu yük server'dan alınmış olur.
- `gRPC` servislerinde türler ve diller arasında dönüşümler yapmaya gerek yoktur. Bu türler protokol üzerinde önceden kaydedilmiştir. Bu sayede `Protobuf` hedef dile uygun bir şekilde dönüşümleri kolayca yapar.
- `gRPC` servislerinde dezavantaj olarak şunu söylebiliriz. IIS ve Azure App Service ile birlikte kullanılamıyor fakat Kestrel ile kullanılabilir. Ayrıca `REST` servislerde tarayıcı desteği varken, `gRPC` servisler için tarayıcı desteği kısmi olarak vardır.

# gRPC İletişim Türleri

- `Unary:` Client tarafından bir istek gönderilir, server tarafında bu istek işlenir ardından geriye bir yanıt döndürülür.
- `Server Streaming:` Client tarafından bir istek gönderilir, server bu isteğe parça parça yanıt verir. Buradaki parçalar sıralı olacaktır.    
- `Client Streaming:` Client tarafından istek parça parça olarka gönderilir, server bu parça istekleri işler ve sonrasında geriye tek bir yanıt döndürür.
- `Bidirectional Streaming:` Client tarafından istek parça parça gönderilir, server tarafında gelen parça istekler işlenir ve geriye parça yanıtlar döndürülür.

# gRPC Servis Oluşturma

- Unary Fonksiyon:

    ```cs
    public override Task<ExampleResponse> UnaryCall(ExampleRequest request,
    ServerCallContext context)
    {
        var response = new ExampleResponse();
        return Task.FromResult(response);
    }
    ```

- Server Streaming Fonksiyon:

    Server stream işlemine başladıktan sonra client tarafından herhangi bir mesaj veya veri gönderilemez.

    ```cs
    public override async Task StreamingFromServer(ExampleRequest request,
    IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
    {
        for (var i = 0; i < 5; i++)
        {
            await responseStream.WriteAsync(new ExampleResponse());
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
    ```

    Bazı server stream fonksiyonları client tarafından iptal edilmedikce çalışacak şekilde kullanılabilir. Örnek olarak:

    ```cs
    public override async Task StreamingFromServer(ExampleRequest request,
        IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            await responseStream.WriteAsync(new ExampleResponse());
            await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
        }
    }
    ```

    Bu tarz fonksiyonlar client tarafından gönderilen `CancellationToken` ile sonlandırılabilirler.

    Thread ile kullanım örneği:

    ```cs
    public override async Task StreamingFromServer(ExampleRequest request,
    IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
    {
        var writeTask = Task.Run(async () =>
        {
            for (var i = 0; i < 5; i++)
            {
                await responseStream.WriteAsync(new ExampleResponse());
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        });

        await PerformLongRunningWorkAsync();

        await writeTask;
    }
    ```

- Client Streaming Fonksiyon:

    ```cs
    public override async Task<ExampleResponse> StreamingFromClient(
    IAsyncStreamReader<ExampleRequest> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            var message = requestStream.Current;
            // ...
        }
        return new ExampleResponse();
    }
    ```

    C# 8 veya daha yükseğini kullanıyorsak `await foreach` yapısını uygulayabiliriz. Örnek olarak:

    ```cs
    public override async Task<ExampleResponse> StreamingFromClient(
    IAsyncStreamReader<ExampleRequest> requestStream, ServerCallContext context)
    {
        await foreach (var message in requestStream.ReadAllAsync())
        {
            // ...
        }
        return new ExampleResponse();
    }
    ```

- Bidirectional Streaming Fonksiyon:

    `requestStream` parametresi client tarafından gönderilen mesajları okumak için kullanılır. Eğer servis isterse `responseStream` ile geriye mesaj gönderebilir.

    ```cs
    public override async Task StreamingBothWays(IAsyncStreamReader<ExampleRequest> requestStream,
        IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
    {
        await foreach (var message in requestStream.ReadAllAsync())
        {
            await responseStream.WriteAsync(new ExampleResponse());
        }
    }
    ```

    Burada biraz daha karmaşık yapı kurarak, gelen parça isteğe direkt olarak parça yanıt dönebiliriz. Örnek olarak:

    ```cs
    public override async Task StreamingBothWays(IAsyncStreamReader<ExampleRequest> requestStream,
        IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
    {
        // Read requests in a background task.
        var readTask = Task.Run(async () =>
        {
            await foreach (var message in requestStream.ReadAllAsync())
            {
                // Process request.
            }
        });
        
        // Send responses until the client signals that it is complete.
        while (!readTask.IsCompleted)
        {
            await responseStream.WriteAsync(new ExampleResponse());
            await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
        }
    }
    ```

    Burada `requestStream` ve `responseStream`'i birbirinden ayrı thread'lerde kullanmamız güvenli olan yöntemdir. Örnek olarak:

    ```cs
    public override async Task DownloadResults(DataRequest request,
    IServerStreamWriter<DataResult> responseStream, ServerCallContext context)
    {
        var channel = Channel.CreateBounded<DataResult>(new BoundedChannelOptions(capacity: 5));

        var consumerTask = Task.Run(async () =>
        {
            // Consume messages from channel and write to response stream.
            await foreach (var message in channel.Reader.ReadAllAsync())
            {
                await responseStream.WriteAsync(message);
            }
        });

        var dataChunks = request.Value.Chunk(size: 10);

        // Write messages to channel from multiple threads.
        await Task.WhenAll(dataChunks.Select(
            async c =>
            {
                var message = new DataResult { BytesProcessed = c.Length };
                await channel.Writer.WriteAsync(message);
            }));

        // Complete writing and wait for consumer to complete.
        channel.Writer.Complete();
        await consumerTask;
    }
    ```


- Client tarafından direkt olarak mesaj ile veri göndermek yerine `RequestHeaders` ile de veri gönderimi yapabiliriz. Örnek olarak: 

    ```cs
    public override Task<ExampleResponse> UnaryCall(ExampleRequest request, ServerCallContext context)
    {
        var userAgent = context.RequestHeaders.GetValue("user-agent");
        // ...

        return Task.FromResult(new ExampleResponse());
    }
    ```

# gRPC Protobuf Mesajı Oluşturma

- Protobuf gönderilen ve alınan mesajları dilden bağımsız olarak tanımlayabilmemizi sağlayan formattır.

- Protobuf içerisinde tanımladığımız mejalar .Net araçları ile otomatik olarak C# türlerine dönüştürülürler.

- Protobuf içerisinde prop'lara null atayamayız. Sayısal prop'lar 0, string'ler boş string alabilir. Null atamayı denersek exception fırlatacaktır.

    Nullable prop'lar kullanmak istiyorsak Protobuf içerisinde bir kütüphane ekleyip onun ile kullanabiliriz. Örnek olarak:

    ```py
    import "google/protobuf/wrappers.proto";

    message Person {
        // ...
        google.protobuf.Int32Value age = 5;
    }
    ```

    Burada `google.protobuf.Int32Value` olarak verilen prop, C# tarafında `int?` olarak oluşturulur.

- Protobuf içerisinde liste tanımlamak istersek `repeated` kelimesini prop'a ön ek olarak eklememiz gerekiyor. Örnek olarak 

    ```cs
    message Person {
    // ...
    repeated string roles = 8;
    }

    |
    |
    V

    public class Person
    {
        // ...
        public RepeatedField<string> Roles { get; }
    }
    ```

    Bu tipteki prop'lar üzerinde LINQ sorguları yapılabilir, array veya list'e de dönüştürülebilirler. 

    Not olarak bu prop'ler set edilemezler. Yeni elemanlar var olan listeye eklenmek zorundadır. Örnek olarak: 

    ```cs
    var person = new Person();

    // Add one item.
    person.Roles.Add("user");

    // Add all items from another collection.
    var roles = new [] { "admin", "manager" };
    person.Roles.Add(roles);
    ```

- C#'taki sözlük tipini burada şu yapı karşılar:

    ```
    message Person {
    // ...
    map<string, string> attributes = 9;
    }
    ```

- Protobuf içerisinde `oneof` ile bir propr oluşturulursa, C# tarafında bir enum oluşturulur ve set edilen değer C# tarafında da enum olarak atanır. Eğer protobuf tarafında set edilmediyse, C# tarafında default değer veya null atanır.

# .Net Client ile gRPC Çağrısı Yapmak

- Client'lar `.proto` dosyasından üretilirler. Bu dosya içerisinde tanımlanan fonksiyonlara bu clientlar üzerinden erişebiliriz. 

    Bir client üretebilmemiz için elimizde bir `channel` olmalıdır. Client oluşturmadan önce bir channel oluştururuz ve sonrasında bu channel'ı client'ı üretirken kullanırız. Örnek olarak:

    ```cs
    var channel = GrpcChannel.ForAddress("https://localhost:5001");
    var client = new Greet.GreeterClient(channel);
    ```

- Channel oluşturma işlemi masraflı olduğundan çok fazla gerçekleştirmek istenmeyen bir işlemdir. Yapacağımız gRPC çağrılarında var olan channel'ları kullanmamız performans açısından avantaj sağlayacaktır.

    gRPC client'ları ise az masraflı nesnelerdir. Bu nedenle bunların tekrar tekrar kullanılmasına veya ön belleğe alınmasına gerek yoktur. 

- Client oluşturmanın tek yolu yukarıda gösterildiği gibi `GrpcChannel.ForAddress("")` değildir. Eğer .Net Core ile çalışıyorsak `gRPC client factory` yöntemini de kullanabiliriz. Bu yöntemin detayı sonraki kısımlarda yer alıyor olacak.

- Proto dosyası içerisinde Unary olarak tanımlanan fonksiyonlar, .Net tarafında hem senkron olarak hem de asenkron olarak kullanılabilecek şekilde 2 adet oluşturulurlar.

- Server Streaming çağrılarında `ResponseStream.MoveNext()` fonksiyonu servisten gelen verileri okur. Eğer servis tarafında işlem tamamlandıysa bu fonksiyon `false` yanıtını döner ve *while* döngüsünden çıkmamızı sağlar.

    ```cs
    var client = new Greet.GreeterClient(channel);
    using var call = client.SayHellos(new HelloRequest { Name = "World" });

    while (await call.ResponseStream.MoveNext())
    {
        Console.WriteLine("Greeting: " + call.ResponseStream.Current.Message);
        // "Greeting: Hello World" is written multiple times
    }    
    ```

    C# 8 ve sonrasını kullanıyorsak alt kısımdaki yöntemi tercih edebiliriz:

    ```cs
    var client = new Greet.GreeterClient(channel);
    using var call = client.SayHellos(new HelloRequest { Name = "World" });

    await foreach (var response in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine("Greeting: " + response.Message);
        // "Greeting: Hello World" is written multiple times
    }
    ```

- Client Streaming fonksiyonlarda client `RequestStream.WriteAsync` ile mesajlarını gönderir. Gönderme işlemi bittiğinde `RequestStream.CompleteAsync()` ile server tarafına bilgi verir ve server gönderilen mesajları işleyip geriye yanıt döndürür.

- Bir fonksiyon çağrısı yaparken bu çağrı için `deadline` tanımlaması yapabiliriz. Bunun sağladığı en önemli fayda, hatalı çalışan bir fonksiyon çağrısının sonsuza kadar çalışmasının önüne geçer ve bu sayede gereksiz kaynak kullanımını engellemiş olur. Örnek bir kullanım:

    ```cs
    var client = new Greet.GreeterClient(channel);

    try
    {
        var response = await client.SayHelloAsync(
            new HelloRequest { Name = "World" },
            deadline: DateTime.UtcNow.AddSeconds(5));
        
        // Greeting: Hello World
        Console.WriteLine("Greeting: " + response.Message);
    }
    catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
    {
        Console.WriteLine("Greeting timeout.");
    }
    ```



# Kaynak

- [Microsoft](https://learn.microsoft.com/en-us/aspnet/core/grpc/basics?view=aspnetcore-7.0)
- [Gençay Yıldız](https://www.gencayyildiz.com/blog/grpc-nedir-ne-amacla-ve-nasil-kullanilir/#:~:text=gRPC%3B%20CPU%2C%20memory%20ve%20bandwidth,i%C3%A7in%20tercih%20edilebilecek%20bir%20teknolojidir.)
- [Dream Factory](https://blog.dreamfactory.com/grpc-vs-rest-how-does-grpc-compare-with-traditional-rest-apis/)
- [Protobuf.dev](https://protobuf.dev/getting-started/csharptutorial/)
- [Luka Stosic Medium](https://medium.com/@lukastosic/part-1-grpc-its-fairly-simple-c-example-d4b266c4c72e)

