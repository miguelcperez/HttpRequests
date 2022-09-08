namespace HttpRequest.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string PublishDate { get; set; }
        public IEnumerable<string> Authors { get; set; }
    }
}
