namespace WebDongTrung.Common.Utils
{
    public class FileUploadAPI
    {
        public IFormFile? Files { get; set; }
    }
    public class common
    {
       public FileUploadAPI _fileAPI { get; set; } = null!;
    }
}