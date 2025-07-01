using Infrastructure.Services.Storage.Azure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomImagesController : ControllerBase
    {
        private readonly AzureStorage _service = new AzureStorage();

        [HttpGet]
        public IActionResult GetFileUrl(string fileName)
        {
            string link = $"https://blobstoragetest222.blob.core.windows.net/images/{fileName}";
            return Ok(link);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(IFormFile file)
        {

            string blobName = await _service.UploadAsync(file);

            return Ok(blobName);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBlobAsync(string blobName)
        {
            return Ok(await _service.DeleteBlobAsync(blobName));
        }


        //https://blobstoragetest222.blob.core.windows.net/ - BasePath
        //pb502images/ - Container Name
        //7fd32b62-209c-42b6-845e-be46b896e901.png - Blob Name

    }
}
