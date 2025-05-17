using MongoDB.Bson.Serialization.Serializers;

namespace library.api.Application.Services.Files
{
    public interface IFileRepositoryService
    {
        Task Upload(string fileName, string contentType, byte[] contentBytes);

        Task<byte[]> GetFile(string fileName);
    }
}
