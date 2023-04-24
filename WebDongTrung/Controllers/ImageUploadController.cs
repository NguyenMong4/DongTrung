using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Common.Utils;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment = null!;
        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost]
        public string Post([FromForm] FileUploadAPI objFile)
        {
            common obj = new()
            {
                _fileAPI = new FileUploadAPI()
            };
            try
            {
                if (objFile.Files != null && objFile.Files!.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "/images"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "/images/");
                    }
                    using FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "/images/" + objFile.Files.FileName);
                    objFile.Files.CopyTo(filestream);
                    filestream.Flush();
                    return "/images/" + objFile.Files.FileName;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return string.Empty;
        }
    }
}