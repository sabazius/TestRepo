using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger,
            IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet("Get")]
        public IEnumerable<Person> Get()
        {
            return _personService.GetAllUsers();
        }

        [HttpGet("GetById")]
        public Person? Get(int id)
        {
            return _personService.GetById(id);
        }

        [HttpPost]
        public Person? Add([FromBody] Person person)
        {
            return _personService.AddUser(person);
        }

        [HttpPut]
        public Person? Update([FromBody] Person person)
        {
            return _personService.UpdateUser(person);
        }

        [HttpDelete]
        public Person? Delete(int id)
        {
            return _personService.DeleteUser(id);
        }
    }
}