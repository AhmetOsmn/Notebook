using MassTransit;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Client.Console
{
    public class Program
    {
        static HttpClient _client;

        static async Task Main(string[] args)
        {
            while(true)
            {
                System.Console.WriteLine("Enter # of orders to send, or empty to quit: ");
                var line = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line)) break;

                if(!int.TryParse(line, out int limit)) limit = 1;

                var tasks = new List<Task>();

                _client = new HttpClient { Timeout = TimeSpan.FromMinutes(1)};

                for (int i = 0; i < limit; i++)
                {
                    var order = new OrderModel
                    {
                        Id = NewId.NextGuid(),
                        CustomerNumber = $"CUSTOMER{i}",
                        PaymentCardNumber = i % 4 == 0 ? "5999" : "4000-1234",
                        Notes = new string('*', 1000 * (i + 1))
                    };

                    tasks.Add(Execute(order));
                }

                await Task.WhenAll(tasks.ToArray());

                System.Console.WriteLine();
                System.Console.WriteLine("Results");
                foreach (var task in tasks.Cast<Task<string>>())
                {
                    System.Console.WriteLine(task.Result);
                }

            }
        }

        static readonly Random _random = new Random();

        private async static Task<string> Execute(OrderModel order)
        {
            try
            {
                var json = JsonConvert.SerializeObject(order);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await _client.PostAsync($"http://localhost:5286/api/Order", data);

                responseMessage.EnsureSuccessStatusCode();

                var result = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.StatusCode == HttpStatusCode.Accepted)
                {
                    await Task.Delay(2000);
                    await Task.Delay(_random.Next(6000));

                    var orderAddress = $"http://localhost:5286/api/Order?id={order.Id:D}";

                    var patchResponse = await _client.PatchAsync(orderAddress, data);

                    patchResponse.EnsureSuccessStatusCode();

                    var patchResult = await patchResponse.Content.ReadAsStringAsync();

                    do
                    {
                        await Task.Delay(5000);

                        var getResponse = await _client.GetAsync(orderAddress);

                        getResponse.EnsureSuccessStatusCode();

                        var getResult = await getResponse.Content.ReadFromJsonAsync<OrderStatusModel>();

                        if (getResult.State == "Completed" || getResult.State == "Faulted")
                            return $"ORDER: {order.Id:D} STATUS: {getResult.State}";

                        System.Console.Write(".");
                    }
                    while (true);
                }

                return result;
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);

                return exception.Message;
            }
        }
    }

    public class OrderModel
    {
        public Guid Id { get; set; }
        public string CustomerNumber { get; set; }
        public string PaymentCardNumber { get; set; }
        public string Notes { get; set; }
    }

    public class OrderStatusModel
    {
        public Guid OrderId { get; set; }
        public string State { get; set; }
    }
}