namespace BookStore.Models.Requests
{
    public class MyClass
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
    public class ValidatorTestRequest
    {
        public IEnumerable<MyClass> MyCollection { get; set; } = Enumerable.Empty<MyClass>();
    }
}
