using System.Data.SqlClient;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.SQLRepositories
{
    public class EmployeesRepository : IEmployeesRepository, IUserInfoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookSqlRepository> _logger;

        public EmployeesRepository(IConfiguration configuration, ILogger<BookSqlRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Employee>("SELECT * FROM Employee");
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.GetById {e.Message}", e);
                }
            }
            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var employee =  await conn.QueryAsync<Employee>("SELECT * FROM Employee WHERE ID = @Id", new {Id = id});

                    return employee.FirstOrDefault();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.GetById {e.Message}", e);
                }
            }

            return null;
        }

        public async Task AddEmployee(Employee employee)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    string query =
                        @"INSERT INTO Employee 
                        ([EmployeeID]
                       ,[NationalIDNumber]
                       ,[EmployeeName]
                       ,[LoginID]
                       ,[JobTitle]
                       ,[BirthDate]
                       ,[MaritalStatus]
                       ,[Gender]
                       ,[HireDate]
                       ,[VacationHours]
                       ,[SickLeaveHours]
                       ,[rowguid]
                       ,[ModifiedDate])
                        VALUES 
                        (EmployeeID
                       ,NationalIDNumber
                       ,EmployeeName
                       ,LoginID
                       ,JobTitle
                       ,BirthDate
                       ,MaritalStatus
                       ,Gender
                       ,HireDate
                       ,VacationHours
                       ,SickLeaveHours
                       ,rowguid
                       ,ModifiedDate)";

                    var result = await conn.ExecuteAsync(query, employee);
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.Add {e.Message}", e);
                }
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    string query =
                       @"UPDATE [dbo].[Employee]
                       SET [EmployeeID] = @EmployeeID
                          ,[NationalIDNumber] = @NationalIDNumber
                          ,[EmployeeName] = @EmployeeName
                          ,[LoginID] = @LoginID
                          ,[JobTitle] = @JobTitle
                          ,[BirthDate] = @BirthDate
                          ,[MaritalStatus] = @MaritalStatus
                          ,[Gender] = @Gender
                          ,[HireDate] = @HireDate
                          ,[VacationHours] = @VacationHours
                          ,[SickLeaveHours] = @SickLeaveHours
                          ,[rowguid] = @rowguid
                          ,[ModifiedDate] = @ModifiedDate
                     WHERE EmployeeID = @EmployeeID";

                    var result = await conn.ExecuteAsync(query, employee);
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.Add {e.Message}", e);
                }
            }
        }

        public async Task DeleteEmployee(int id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                var sqlStatement = "DELETE Employee WHERE Id = @Id";
                await conn.ExecuteAsync(sqlStatement, new { Id = id });
            }
        }

        public async Task<bool> CheckEmployee(int id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var employee = await conn.QueryAsync<Employee>("SELECT * FROM Employee WHERE ID = @Id", new { Id = id });

                    return employee.FirstOrDefault() != null ? true : false;
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.GetById {e.Message}", e);
                    return false;
                }
            }
        }

        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryAsync<UserInfo>(
                        "SELECT * FROM UserInfo WHERE UserName = @username AND Password = @password", new
                        {
                            UserName = email,
                            Password = password
                        });

                    return result.FirstOrDefault();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.GetById {e.Message}", e);
                    return null;
                }
            }
        }
    }
}
