using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using library.api.Application.Services.Files;
using Microsoft.Extensions.Options;

namespace library.api.Infraestructure.Files
{
    public class S3BucketFilesRepository : IFileRepositoryService
    {
        private readonly IOptions<AWSS3Settings> _settings;

        public S3BucketFilesRepository(IOptions<AWSS3Settings> settings)
        {
            _settings = settings;
        }

        public async Task<byte[]> GetFile(string fileName)
        {
            var getRequest = new GetObjectRequest
            {
                BucketName = _settings.Value.BucketName,
                Key = fileName
            };

            var client = GetClient();

            var response = await client.GetObjectAsync(getRequest);
            using var ms = new MemoryStream();
            response.ResponseStream.CopyTo(ms);
            ms.Position = 0;
            var bytes = ms.ToArray();
            return bytes;
        }

        public async Task Upload(string fileName, string contentType, byte[] contentBytes)
        {
            var client = GetClient();
            using var stream = new MemoryStream(contentBytes);

            var putRequest = new PutObjectRequest
            {
                BucketName = _settings.Value.BucketName,
                Key = fileName,
                InputStream = stream,
                ContentType = contentType
            };
            var response = await client.PutObjectAsync(putRequest);
        }

        private IAmazonS3 GetClient()
        {
            var awsS3Settings = _settings.Value;
            var awsCredentials = new BasicAWSCredentials(awsS3Settings.AccessKey, awsS3Settings.SecretKey);
            var s3Client = new AmazonS3Client(awsCredentials, Amazon.RegionEndpoint.USEast2);
            return s3Client;
        }
    }
}
