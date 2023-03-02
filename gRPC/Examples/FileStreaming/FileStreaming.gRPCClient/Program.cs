using Google.Protobuf;
using Grpc.Net.Client;

namespace FileStreaming.gRPCUploadClient
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7120");
            var client = new FileService.FileServiceClient(channel);

            // dosya yolu verilmeli
            string file = @"C:";

            using FileStream fileStream = new FileStream(file, FileMode.Open);
            var content = new BytesContent
            {
                FileSize = fileStream.Length,
                ReadedByte = 0,
                Info = new FileInfo
                {
                    FileName = Path.GetFileNameWithoutExtension(fileStream.Name),
                    FileExtension = Path.GetExtension(fileStream.Name)
                }
            };

            var upload = client.FileUpload();
            byte[] buffer = new byte[2048];

            while ((content.ReadedByte = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                content.Buffer = ByteString.CopyFrom(buffer);
                await upload.RequestStream.WriteAsync(content);
            }

            await upload.RequestStream.CompleteAsync();
            fileStream.Close();
        }
    }
}