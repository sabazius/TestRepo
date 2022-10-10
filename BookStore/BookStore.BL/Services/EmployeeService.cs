using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.Users;

namespace BookStore.BL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeesRepository _employeeRepository;
        //private readonly IUserInfoRepository _userInfoRepository;

        public EmployeeService(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<Employee> AddEmployee(Employee employee)
        {
            await _employeeRepository.AddEmployee(employee);
            return employee;
        }

        public async Task<bool> CheckEmployee(int employeeId)
        {
            return await _employeeRepository.CheckEmployee(employeeId);
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetEmployeeDetails();
        }

        public async Task<Employee?> GetEmployeeDetails(int employeeId)
        {
            return await _employeeRepository.GetEmployeeDetails(employeeId);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeRepository.UpdateEmployee(employee);
        }

        //public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        //{
        //    return await _userInfoRepository.GetUserInfoAsync(email, password);
        //}
    }
}
