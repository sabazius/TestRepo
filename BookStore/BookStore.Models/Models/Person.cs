namespace BookStore.Models.Models
{
    public record  Person
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public int Age { get; init; }

        public DateTime DateOfBirth { get; init; }
    }
}
