# Concepts

## Messages

- MassTransit'te mesajları ilk olarak .Net tarafında record, class veya interface türünde tanımlamamız gerekir (Code first) ve mesajlar referans türünde olmalıdır. 

    Tanımlanan mesajların içerisinde sadece prop'lar olmalıdır. Metotlar veya diğer elemanlar mesajların içerisinde olmamalıdır.

- MT'de mesajlar namespace'lerini içerecek şekilde kullanılırlar. 2 farklı projede aynı mesajı kullanmak istiyorsak namespace'lerinin eşleşmesi gerekir. Aksi durumda mesaj consume edilemez.
- Interface kullanıldığında MT arka tarafta dinamik bir class oluşturur ve mesajı kullanmamızı sağlar.

- Mesajları oluştururken bir base class oluşturup ondan farklı mesajlar üretmek ileride sorunlara yol açmaktadır. Bu nedenle base class'ları kullanmamaya dikkat etmeliyiz. 

- Mesajlara ait 3 adet attribute vardır:

    1. `EntityName:` Exchange veya Topic adı.
    2. `ExcludeFromTopology:` Direkt olarak consume edilmedikçe veya publish edilmedikçe herhangi bir Exchange veya Topic oluşturmaz.
    3. `ExcludeFromImplementedTypes:` Bu mesaj türü için bir Middleware Filter oluşturmaz.

- 2 ana mesaj türü vardır `Events` ve `Commands`. Bir mesaj için isim seçerken, mesajın türü mesajın zamanını belirlemelidir.
- `Commands:`

    Bir command, servise bir şey yapmasını söyler ve sadece bir consumer tarafından consume edilmelidir.

    İsimlendirme yaparken fiil-isim şeklinde isimlendirmeliyiz. Örnek olarak:

    - SubmitOrder
    - UpdateUserAddress

- `Events:`

    Eventler bir şeyin olduğunu haber verirler. Eventler *ConsumeContext, IPublishEndpoint veya IBus* tarafından `Publish` kullanılarak yayınlanırlar.

    İsimlendirme yaparken isim-fiil şeklinde geçmiş zaman kullanarak yapmalıyız. Örnek olarak:

    - UserAddressUpdated
    - OrderSubmitted

- Gönderilen her mesaj bir bakıma zarflanarak gönderilir. Bu zarf mesajın içeriğine ek olarak ekstra bilgileri içerir bunlar *Message Headers* olarak geçer. Bu bilgilere `ConsumeContext` üzerinden okunabilir veya `SendContext` üzerinden tanımlanabilir.

- Headers içerisinde yer alan *ConversationId, CorrelationId, ve InitiatorId* bilgileri farklı mesajları aynı görüşmede toplayabilmek için kullanılırlar.

    Örnek olarak bir consumer yanıt olarak bir mesaj yayınlarsa/gönderirse, bu consumer'ın tükettiği mesaj ile yayınladığı/gönderdiği mesajın *ConversationId*'leri aynı olacaktır.

    Eğer consume edilen mesajın *CorrelationId*'si varsa, bu id *InitiatorId*'ye kopyalanır.

    *CorrelationId* farklı şekillerde set edilebilir. Örnek olarak:

    ```cs 
    await endpoint.Send<SubmitOrder>(new { OrderId = InVar.Id }, sendContext =>
        sendContext.CorrelationId = context.Message.OrderId);

     // veya

    await endpoint.Send<SubmitOrder>(new
    {
        OrderId = context.Message.OrderId,
        __CorrelationId = context.Message.OrderId,76
    });
    ```

- Correlation Kuralları

    *CorrelationId* bazı kurallara uyularak da set edilebilir. Bazı kurallara bakalım:

    1. Eğer mesaj *Guid CorrelationId* prop'una sahip bir `CorrelatedBy<Guid>` interface'i implement ediyorsa bu değer kullanılır.
    2. Mesaj *Guid/Guid?* tipinde ve *CorrelationId, CommandId veya EventId* isminde bir prop'a sahipse onun değeri kullanılır.
    3. Eğer geliştirici mesaj türü için *CorrelationId Provider* kaydettiyse, değeri almak için bu provider kullanılır. Bu provider *Bus* oluşturulmadan önce kayıt edilmiş olmalıdır. Örnekler:

        ```cs
        // Eski yaklaşım, bütün Bus'lar tarafından erişilebilir.
        GlobalTopology.Send.UseCorrelationId<SubmitOrder>(x => x.OrderId);
        // Yeni yaklaşım, bütün Bus'lar tarafından erişilebilir.
        MessageCorrelation.UseCorrelationId<SubmitOrder>(x => x.OrderId);
        // Belirlenen bir Bus'ın config'i ile tanımlanır. Sadece bu Bus'a özeldir.
        cfg.SendTopology.UseCorrelationId<SubmitOrder>(x => x.OrderId);
        ```

- Saga Korelasyonu

    Saga'larda *CorrelationId* olmalıdır. Ayrıca bu id Saga Repo'su tarafından primary key olarak kullanılır. Bu mesajların Saga'lar ile ilişki kurabilmelerini sağlar.

    Yeni Saga instance'ları oluşturulurken, initialize edildikleri mesajın *CorrelationId*'si atanacaktır. 

- Identifiers

    MT'de identifier olarak Guidler kullanılır. Geliştirilen `NewId` sayesinde normal bir *Guid* ten daha performanslı olan bu tür identifier olarak kullanılır. 

## Consumers

- MT'de farklı consumer türleri vardır. Bunlar *consumer'lar, saga'lar, saga state machine'ler, routing slip activity'ler, handler'lar, ve job consumer'lar*dır.

- `Message Consumer:` En yaygın olan consumer türüdür. Örnek olarak:

    ```cs
    public interface IConsumer<in TMessage> :
        IConsumer
        where TMessage : class
    {
        Task Consume(ConsumeContext<TMessage> context);
    }
    ```

    Consume  ettiği her mesaj türü için `IConumser` generic interface'ini ve `Consume` metodunu ile tanımlama yapar. 

    `Consume` fonksiyonu çalışırken mesaj başka bir consumer tarafından kullanılamaz. Consum işlemi başarıyla tamamlanırsa mesaj onaylanır (*acknowledged*) ve kuyruktran kaldırılır. 

    Eğer consume işlemi hata alırsa veya iptal edilirse, consumer instance'ı serbest bırakılır. Eğer exception bir retry operasyonunu tetiklemiyorsa varsayılan olarak mesajı hata kuyruğuna taşır.

- `Batch Consumer:` Birden fazla mesajın birlikte consume edilmesinde kullanılırlar. Örnek olarak :

    ```cs
    class BatchMessageConsumer :
    IConsumer<Batch<Message>>
    {
        public async Task Consume(ConsumeContext<Batch<Message>> context)
        {
            for(int i = 0; i < context.Message.Length; i++)
            {
                ConsumeContext<Message> message = context.Message[i];
            }
        }
    }
    ```

- Skipped Messages

    Eğer bir consumer silinirse veya bağlantısı kesilirse mesaj ondan alınır ver *_skipped Queue* içerisine eklenir. Bu sayede mesaj ve içeriği kaybedilmemiş olur.


## Producers

- Bir mesajı produce etmenin iki farklı yolu vardır. Birisi `Sent` diğeri ise `Publish`'tir. Bu ikisi arasındaki farkı şöyle açıklayabiliriz: 
    
    `Sent` edilen bir mesaj sadece *DestinationAddress* ile belirtilen endpoint'e iletilir.
    `Publish` edilen mesaj ise bu mesaj türüne *subscribe* olan bütün consumer'lara yayınlanır.

    Burada şöyle bir açıklama yapabiliriz, *command'lar sent edilir*. *Event'lar ise publish edilir.*

- Publish örneği olarak alt kısımdaki kodlara bakabiliriz:

    ```cs
    public record OrderSubmitted
    {
        public string OrderId { get; init; }
        public DateTime OrderDate { get; init; }
    }

    public async Task NotifyOrderSubmitted(IPublishEndpoint publishEndpoint)
    {
        await publishEndpoint.Publish<OrderSubmitted>(new()
        {
            OrderId = "27",
            OrderDate = DateTime.UtcNow,
        });
    }

    // veya

    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        private readonly IOrderSubmitter _orderSubmitter;

        public SubmitOrderConsumer(IOrderSubmitter submitter)
            => _orderSubmitter = submitter;

        public async Task Consume(IConsumeContext<SubmitOrder> context)
        {
            await _orderSubmitter.Process(context.Message);

            await context.Publish<OrderSubmitted>(new()
            {
                OrderId = context.Message.OrderId,
                OrderDate = DateTime.UtcNow
            })
        }
    }
    ```

- Header içerisindeki value'ları anonim tip içerisinde `"__"` ile birlikte set edebiliriz. Örnek olarak:

    ```cs
    public record GetOrderStatus
    {
        public Guid OrderId { get; init; }
    }

    var response = await requestClient.GetResponse<OrderStatus>(new 
    {
        __TimeToLive = 15000,
        OrderId = orderId,
    });
    ```

    Yukarıdaki teknik olarak kötü bir örnek ama sadece nasıl kullanıldığını göstermek için var.

- Header içerisinde custom value'lar eklemek istersek şu şekilde ekleyebiliriz: `__Header_X_B3_TraceId = 42` böyle bir tanımlama yaptığımızda, mesajın header kısmında bu value `X-B3-TraceId` olarak yer alır.

## Exceptions

- Bir mesaj consume edilirken exception fırlatılırsa, mesaj `_error` kuyruğuna taşınır. Alınan exception'lar ile ilgili bilgiler ise mesajın headers kısmında yer alır.

- Retry:

    Bir mesajın consume edilememesinin farklı nedenleri olabilir. İşlemin başarısız olduğu durumlarda direkt olarak mesajı *_error* kuyruğuna göndermek yerine tekrar gönderme işlemini ayarlayabiliriz ve buna Retry politikası denir. Bu ayarlamayı endpoint üzerinde veya consumer üzerinden de yapabiliriz.
  
    Örnek olarak alt kısımdaki koda bakalım:

    ```cs
    services.AddMassTransit(x =>
    {
        x.AddConsumer<SubmitOrderConsumer>();

        x.UsingRabbitMq((context,cfg) =>
        {
            cfg.UseMessageRetry(r => r.Immediate(5));

            cfg.ConfigureEndpoints(context);
        });
    });
    ```

    Yukarıdaki örnekte retry politikası bütün *Bus* içerisinde geçerli olacak şekilde ayarlanıyor. Bu kullanım yerine receive endpoint seviyesinde bir retry politikası ayarlamak istersek alt kısımdaki örneğe bakabiliriz:

    ```cs
    services.AddMassTransit(x =>
    {
        x.AddConsumer<SubmitOrderConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.ReceiveEndpoint("submit-order", e =>
            {
                e.UseMessageRetry(r => r.Immediate(5));

                e.ConfigureConsumer<SubmitOrderConsumer>(context);
            });
        });
    });
    ```

    
    Retry politikaları mesajı kilit altına alırlar. Bu nedenle kısa ve geçici durumlar için kullanılmalılar. Uzun zaman aralıkları (1 saat gibi) ile ayarlanan retry politikaları sorunlara yol açmaktadır.

     Retry politikalarının tipleri

    - `None:` Tekrar etme.
    - `Immediate:` Belirlenen deneme sınırına kadar denemeye hemen başla.
    - `Interval:` Yeniden deneme sınırına kadar sabit bir gecikmeden sonra dene.
    - `Intervals:` Belirlenen her aralık için bir gecikmeden sonra dene.
    - `Exponential:` Belirlenen sınıra kadar üstel olarak artan gecikmelerle dene.
    - `Incremental:` Sürekli artan gecikmeler ile dene.

    Birden fazla consumer olduğunda veya saga olduğunda retry/redelivery işlemleri belirli consumerlar/sagalar için aktif olacak şekilde ayarlanabilir. 

- Exception Filters:

    Retry politikalarını her exception için kullanmak yerine bazı özel exception'lar fırlatıldığında uygulamak isteyebiliriz. Bunun için `Exception Filter`'ları kullanabiliriz.

    Filter içerisindeki *Handle* ve *Ignore* fonksiyonları ile exception'ları yönetmiş oluruz.

- Redelivery:

    Bazen mesajlar kuyruktan kaldırıldıktan bir süre sonra kuyruğa tekrar eklenirler. Bu duruma `Redelivery` denir. Bazı yerlerde bu işlem 2. seviye retry olarak da geçebilir.

- Outbox:

    Consumer mesaj ve gönderirse veya event yayınlarsa bunlar outbox'ta tutulur. Consume işlemi başarılı tamamlanırsa bu outbox'taki mesajlar ve eventler gerektiği şekilde gönderilir/yayınlanır, eğer bir exception fırlatılırsa outbox içerisindekiler discard edilir. Bunu yapabilmek için şöyle bir metot kullanmamız gerekir: `cfg.UseInMemoryOutbox()`

- Faults:

    Consumer normal bir şekilde işlemini tamamlamak yerine bir exception fırlattığında bir `Fault` üretilir. Bu fault içerisinde exception bilgileri, hata mesajı, ana bilgisayar bilgisi gibi bilgiler de yer alır. 

    Eğer headers içerisinde bir *FaultAddress* değeri verilmişse,fault direkt olarak oraya gönderilir. Eğer *FaultAddress* yoksa ama *ResponseAddress* değeri verilmişse fault bu adrese gönderilir. Aksi durumda fault yayınlanır ve abone durumdaki bütün consumer'lar bu fault'a erişebilir.

- Error Pipe:

    Hata alınan mesajları *_error* kuyruğuna taşımadan direkt olarak atmak için şu şekilde bir işlem yapılabilir:

    ```cs
    cfg.ReceiveEndpoint("input-queue", ec =>
    {
        ec.DiscardFaultedMessages();
    });
    ```

## Requests

- İstekleri yaparken *Request Client*'lardan faydalanırız. Örnek bir kullanım alt kısımdadır:
 
    ```cs
    public class RequestController :
    Controller
    {
        IRequestClient<CheckOrderStatus> _client;

        public RequestController(IRequestClient<CheckOrderStatus> client)
        {
            _client = client;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(string orderId, CancellationToken cancellationToken)
        {
            var response = await _client.GetResponse<OrderStatusResult>(new { orderId }, cancellationToken);

            return Ok(response.Message);
        }
    }
    ```

    Yukarıdaki gibi bir örnekte eğer istek iptal edilirse (canceled), client bir yanıt beklemeyi bırakır ve akış devam eder. Bu arada gönderilen istek tüketilene kadar veya expire süresi dolana kadar kuyrukta beklemeye devam eder. Mesajların expire süresi default olarak 30 saniyedir.

- Client Configuration

    Eğer mesaj türüne göre request client'ın ayarların değiştirmek istiyorsak alt kısımdaki örnekten faydalanabiliriz:

    ```cs
    services.AddMassTransit(x =>
    {
        // configure the consumer on a specific endpoint address
        x.AddConsumer<CheckOrderStatusConsumer>()
            .Endpoint(e => e.Name = 'order-status');
            
        // Sends the request to the specified address, instead of publishing it
        x.AddRequestClient<CheckOrderStatus>(new Uri("exchange:order-status"));
        
        x.UsingInMemory((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        }));
    });
    ```

- Request Headers

    İstek gönderdiğimiz context'in içerisine bir Header bilgisi eklemek istersek bunun farklı yolları vardır. Bir tanesi request pipeline'nına bir `Execute Filter` eklemektir. Örnek olarak:

    ```cs
    await client.GetResponse<OrderStatusResult>(new GetOrderStatus{ OrderId = orderId }, 
    x => x.UseExecute(context => context.Headers.Set("tenant-id", "some-value")));
    ```

    veya farklı bir yöntem olarak mesajı oluştururken içerisinde *object values overload* denen işlemi gerçekleştirmektir. Örnek olarak:

    ```cs
    await client.GetResponse<OrderStatusResult>(new 
    { 
        orderId,
        __Header_Tenant_Id = "some-value"
    });
    ```

- Multiple Response Types

    Request client'larının başka bir önemli özelliği ise birden fazla yanıt tiplerine destek vermeleridir. Örnek olarak:

    ```cs
    public class CheckOrderStatusConsumer : 
    IConsumer<CheckOrderStatus>
    {
        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            var order = await _orderRepository.Get(context.Message.OrderId);
            if (order == null)
                await context.RespondAsync<OrderNotFound>(context.Message);
            else        
                await context.RespondAsync<OrderStatusResult>(new 
                {
                    OrderId = order.Id,
                    order.Timestamp,
                    order.StatusCode,
                    order.StatusText
                });
        }
    }
    ```

    Yukarıdaki gibi *order==null* durumunda direkt olarak exception fırlatmak yerine `OrderNotFound` tipinde bir yanıt ta dönebiliriyoruz. Bu durumda client'a gelen response şu şekilde işletilebilir:

    ```cs
    var response = await client.GetResponse<OrderStatusResult, OrderNotFound>(new { OrderId = id});

    if (response.Is(out Response<OrderStatusResult> responseA))
    {
        // do something with the order
    }
    else if (response.Is(out Response<OrderNotFound> responseB))
    {
        // the order was not found
    }
    ```

    Bu arada yukarıdaki örneği switch yapısı ile de yapabiliriz.

- Accept Response Types

    Request client, yapılan istek için kabul ettiği mesaj türlerini context içerisinde tutmaktadır. Consumer döneceği yanıt türünün client tarafıntan kabul edilip edilmediğini bu alanı sorgulayarak öğrenebilir. Örnek olarak:

    ```cs
    public async Task Consume(ConsumeContext<CancelOrder> context)
    {
        var order = _repository.Load(context.Message.OrderId);
        if(order == null)
        {
            await context.ResponseAsync<OrderNotFound>(new { context.Message.OrderId });
            return;
        }

        if(order.HasShipped)
        {
            // check the request client response types
            if (context.IsResponseAccepted<OrderAlreadyShipped>())
            {
                await context.RespondAsync<OrderAlreadyShipped>(new { context.Message.OrderId, order.ShipDate });
                return;
            }
            else
                throw new InvalidOperationException("The order has already shipped"); // to throw a RequestFaultException in the client
        }

        order.Cancel();

        await context.RespondAsync<OrderCanceled>(new { context.Message.OrderId });
    }
    ```

- Concurrent Requests

    Birden fazla isteğin aynı anda yapılması ve birlikte yürütülmesi gerekiyorsa alt kısımdaki gibi yapılabilir:

    ```cs
    public class RequestController : Controller
    {
        IRequestClient<RequestA> _clientA;
        IRequestClient<RequestB> _clientB;

        public RequestController(IRequestClient<RequestA> clientA, IRequestClient<RequestB> clientB)
        {
            _clientA = clientA;
            _clientB = clientB;
        }

        public async Task<ActionResult> Get()
        {
            var resultA = _clientA.GetResponse(new RequestA());
            var resultB = _clientB.GetResponse(new RequestB());

            await Task.WhenAll(resultA, resultB);

            var a = await resultA;
            var b = await resultB;

            var model = new Model(a.Message, b.Message);

            return View(model);
        }
    }
    ```

    Farklı mesaj türleri için farklı client'lar oluşturulur. Ardından istekler yapılır ve iki isteğin de tamamlandığını kontrol edip kalan sürece devam edilebilir.

## Mediator

- Configuration

    Yeni bir mediator eklemek istersek alt kısımdaki örnekten faydalanabiliriz:

    ```cs
    services.AddMediator(cfg =>
    {
        cfg.AddConsumer<SubmitOrderConsumer>();
        cfg.AddConsumer<OrderStatusConsumer>();
    });
    ```

    Bir mediator eklendikten sonra başlatılmasına veya durdurulmasına gerek yoktur. Direkt olarak kullanılabilirler.

# Transports

## RabbitMQ

- RMQ decouple ve distribute sistemler sistemlerin aralarında mesajlaşmasını sağlar.

    Point-to-Point, Publish/Subscribe ve Request/Response gibi farklı mesajlaşma modellerini destekler.

    Ayrıca mesaj kalıcılığı, güvenilir teslimat, routing gibi özellikleri de kullanmamıza izin verir.

    Ek olarak sistemi yönetmemize ve takip etmemize yardımdı olan bir arayüze sahiptir. 

- Exchanges

    Gönderilmek istenen mesajları producer'dan alıp, bir veya birden daha fazla kuyruğa yönendiren nesnelere `Exchange` denir.

    - `Direct Exchange:` Mesajları *routing-key*'leri tam olarak eşleşen kuyruklara yönlendirirler.
    - `Fanout Exchange: ` Mesjaları subscribe durumundaki bütün kuyruklara yönlendirirler.
    - `Topic Exchange:` Mesajları *routing-key*'lerinin belirli bir pattern'e göre eşleştiği kuyruklara yönlendirirler.
    - `Headers Exchange:` Mesajları header'larına göre kuyruklara yönlendirirler.

    Oluşturulan exchange'i alt kısımdaki örnek gibi düzenleyebiliriz:

    ```cs
    cfg.Publish<OrderSubmitted>(x =>
    {
        x.Durable = false; // default: true
        x.AutoDelete = true; // default: false
        x.ExchangeType = "fanout"; // default, allows any valid exchange type
    });

    cfg.Publish<OrderEvent>(x =>
    {
        x.Exclude = true; // do not create an exchange for this type
    });
    ```

- Exchange Binding

    Bir exchange'i receive endpoint'e örnekteki gibi bind edebiliriz:

    ```cs
    cfg.ReceiveEndpoint("input-queue", e =>
    {
        e.Bind("exchange-name", x =>
        {
            x.Durable = false;
            x.AutoDelete = true;
            x.ExchangeType = "direct";
            x.RoutingKey = "8675309";
        });

        e.Bind<MessageType>();
    })
    ```

    Yukarıdaki örnekte birisi `input-queue / exchange-name` diğeri ise `input-queue / MessageType` olacak şekilde 2 adet bind oluşturulur.

    Oluşturulan binding üzerinde ayarlama yapmak istersek ilk örnekteki gibi yapabiliriz.

- Endpoint Address

    Endpoint adresleri alt kısımdaki query string parametrelerini desteklerler:

    - `temporary (bool):` Geçici endpoint.
    - `durable (bool):` Mesajları disk'e kaydet.
    - `autodelete (bool):` Bus durdurulduğunda sil.
    - `bind (bool):` Exchange'i kuyruğa bind et.
    - `queue (string):` Kuyruk isime bind et.

## Azure Service Bus 

- Service Bus ek olarak `partitioning  (bölümleme)` ve `auto-scaling (otomatik ölçeklendirme)` gibi özellikleri de sağlar.

    Bunun dışında teslim edilemeyem mesajların veya süresi dolmuş mesajların tutulduğu `dead letter queue` imkanını da sunar.

- Topic

    Aboneler, kendi filtresi olan bir konu için birden fazla abonelik oluşturabilir. Bu sayede sadece kendisini ilgilendiren mesajları almış olur. 

    Ekstra olarak topic'ler mesajların garantili bir şekilde sıralanmasını sağlayabilen *Session Based (Oturum Bazlı)* mesajlaşmayı da sağlar. 

- Partition Key

    Mesajları topiclere bölümlemek istediğimizde kullanabiliriz. Bir key ayarlarız ve seçtiğimiz key'e sahip olan bütün mesajlar aynı bölüme gönderilir. 

    Ayrıca çok fazla mesajımızın olduğu ve bunları gruplamak istediğimiz durumlarda mesajları dengeli olarak dağıtmamızı da sağlar.

## Amazon SQS

## ActiveMQ

## gRPC

## In Memory

## Kafka

# Kaynak

- [Masstransit.io](https://masstransit.io/documentation/concepts)
