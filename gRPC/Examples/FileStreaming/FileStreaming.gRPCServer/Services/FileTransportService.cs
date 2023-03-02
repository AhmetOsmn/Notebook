using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static FileStreaming.gRPCServer.FileService;

namespace FileStreaming.gRPCServer.Services
{
    public class FileTransportService : FileServiceBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileTransportService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public override async Task<Empty> FileUpload(IAsyncStreamReader<BytesContent> requestStream, ServerCallContext context)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "files");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileStream fileStream = null;
            try
            {
                int count = 0;
                decimal chunkSize = 0;

                while (await requestStream.MoveNext())
                {
                    var current = requestStream.Current;

                    if (count++ == 0)
                    {

                        fileStream = new FileStream($"{path}/{current.Info.FileName}{current.Info.FileExtension}", FileMode.CreateNew);
                        fileStream.SetLength(current.FileSize);
                    }

                    var buffer = current.Buffer.ToByteArray();

                    await fileStream.WriteAsync(buffer, 0, buffer.Length);

                    Console.WriteLine($"{Math.Round(((chunkSize += current.ReadedByte) * 100) / current.FileSize)}%");
                }
            }
            catch
            {
                throw new InvalidOperationException("Hata oluştu");
            }

            await fileStream.DisposeAsync();
            fileStream.Close();
            return new Empty();
        }

        public override async Task FileDownload(FileInfo request, IServerStreamWriter<BytesContent> responseStream, ServerCallContext context)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "files");

            using FileStream fileStream = new FileStream($"{path}/{request.FileName}{request.FileExtension}", FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[2048];

            BytesContent content = new BytesContent
            {
                FileSize = fileStream.Length,
                Info = new FileInfo
                {
                    FileName = Path.GetFileNameWithoutExtension(fileStream.Name), FileExtension = Path.GetExtension(fileStream.Name)
                },
                ReadedByte = 0
            };

            while ((content.ReadedByte = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                content.Buffer = ByteString.CopyFrom(buffer);
                await responseStream.WriteAsync(content);
            }

            fileStream.Close();
        }
    }
}