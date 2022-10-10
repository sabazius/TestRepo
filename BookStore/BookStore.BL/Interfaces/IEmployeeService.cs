using BookStore.Models.Models.Users;

namespace BookStore.BL.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();

        Task<Employee?> GetEmployeeDetails(int employeeId);

        Task<Employee> AddEmployee(Employee employee);

        Task UpdateEmployee(Employee employee);

        Task DeleteEmployee(int id);

        Task<bool> CheckEmployee(int employeeId);

        //Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
