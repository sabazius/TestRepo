namespace BookStore.Models.Models
{
    public record Person
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Age { get; init; }

        public DateTime DateOfBirth { get; init; }
    }
}