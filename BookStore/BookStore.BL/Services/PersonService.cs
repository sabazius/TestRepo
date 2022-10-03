using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person?> GetAllUsers()
        {
            return _personRepository.GetAllUsers();
        }

        public Person? GetById(int id)
        {
           return _personRepository.GetById(id);
        }

        public Person? AddUser(Person person)
        {
            return _personRepository.AddUser(person);
        }

        public Person? UpdateUser(Person person)
        {
            return _personRepository.UpdateUser(person);
        }

        public Person? DeleteUser(int userId)
        {
            return _personRepository.DeleteUser(userId);
        }
    }
}