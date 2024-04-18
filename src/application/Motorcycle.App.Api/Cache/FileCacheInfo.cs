namespace Motorcycle.App.Api.Cache
{
    public class FileCacheInfo
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Length { get; set; }
        public string Base64Content { get; set; }
    }
}
