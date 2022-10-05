namespace BookStore.Models.Requests
{
    public class UpdateAuthorRequest
    {
        public int Id { get; set; }

        public string Name { get; init; } = string.Empty;

        public int Age { get; init; }

        public DateTime DateOfBirth { get; init; }

        public string Nickname { get; init; } = string.Empty;
    }
}
