namespace BookStore.DTOs
{
    public class BookDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDateUtc { get; set; }
    }
}
