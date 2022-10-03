using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person?> GetAllUsers();

        Person? GetById(int id);

        Person? AddUser(Person person);


        Person? UpdateUser(Person person);


        Person? DeleteUser(int userId);
    }
}
