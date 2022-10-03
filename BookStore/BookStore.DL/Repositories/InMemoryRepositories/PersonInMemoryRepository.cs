using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class PersonInMemoryRepository : IPersonRepository
    {
        private static List<Person?> _users = new List<Person?>()
        {
            new Person()
            {
                Id = 1,
                Name = "Pesho",
                Age = 20
            },
            new Person()
            {
                Id = 2,
                Name = "Kerana",
                Age = 23
            }
        };


        public PersonInMemoryRepository()
        {
        }

        public IEnumerable<Person?> GetAllUsers()
        {
            return _users;
        }

        public Person? GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public Person? AddUser(Person person)
        {
            try
            {
                _users.Add(person);
            }
            catch (Exception e)
            {
                return null;
            }

            return person;
        }

        public Person? UpdateUser(Person person)
        {
            var existingUser = _users.FirstOrDefault(x => x.Id == person.Id);

            if (existingUser == null) return null;

            _users.Remove(existingUser);

            _users.Add(person);

            return person;
        }

        public Person? DeleteUser(int userId)
        {
            if (userId <= 0) return null;

            var user = _users.FirstOrDefault(x => x.Id == userId);

            _users.Remove(user);

            return user;
        }
    }
}