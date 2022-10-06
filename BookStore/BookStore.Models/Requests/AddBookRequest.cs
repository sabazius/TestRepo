namespace BookStore.Models.Requests
{
    public class AddBookRequest
    {
        public string Title { get; init; } = string.Empty;

        public string AuthorName { get; init; } = string.Empty;
    }
}
