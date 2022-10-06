namespace BookStore.Models.Models
{
    public record Author : Person
    {
        public string? NickName { get; init; }
    }
}
