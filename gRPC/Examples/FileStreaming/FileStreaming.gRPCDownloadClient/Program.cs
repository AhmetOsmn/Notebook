using Grpc.Net.Client;

namespace FileStreaming.gRPCDownloadClient
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7120");
            var client = new FileService.FileServiceClient(channel);

            // dosya yolu verilmeli
            string downloadPath = @"C:";

            var fileInfo = new FileInfo
            {
                FileExtension = ".pdf",
                FileName = "fileTransportTest"
            };

            FileStream fileStream = null;

            var request = client.FileDownload(fileInfo);

            CancellationTokenSource cancellationTokenSource = new();

            int count = 0;
            decimal chunkSize = 0;
            while(await request.ResponseStream.MoveNext(cancellationTokenSource.Token))
            {
                if(count++ == 0)
                {
                    fileStream = new FileStream($@"{downloadPath}\{request.ResponseStream.Current.Info.FileName}{request.ResponseStream.Current.Info.FileExtension}",FileMode.CreateNew);
                    fileStream.SetLength(request.ResponseStream.Current.FileSize);
                }

                var buffer = request.ResponseStream.Current.Buffer.ToByteArray();
                await fileStream.WriteAsync(buffer, 0, request.ResponseStream.Current.ReadedByte);
                Console.WriteLine($"{Math.Round(((chunkSize += request.ResponseStream.Current.ReadedByte) * 100) / request.ResponseStream.Current.FileSize)}%");
            }

            Console.WriteLine("Dosya indirildi..");
            await fileStream.DisposeAsync();
            fileStream.Close();
            Console.ReadLine();
        }
    }
}