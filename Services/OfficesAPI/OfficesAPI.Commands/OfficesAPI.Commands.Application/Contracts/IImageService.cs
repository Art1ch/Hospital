using Microsoft.AspNetCore.Http;

namespace OfficesAPI.Commands.Application.Contracts;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task DeleteImageAsync(string imageUrl);
}
