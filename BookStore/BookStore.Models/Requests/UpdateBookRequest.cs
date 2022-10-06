namespace BookStore.Models.Requests
{
    public class UpdateBookRequest
    {
        public int Id { get; init; }

        public string Title { get; init; } = string.Empty;

        public int AuthorId { get; init; }
    }
}
