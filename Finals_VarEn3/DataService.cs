using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using Finals_VarEn3.Models;


namespace Finals_VarEn3
{


    public class DataService
    {
        private readonly string _connectionString = "Server=VLAD;Database=Zachet; Integrated Security=true;TrustServerCertificate=True;";

        public IEnumerable<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Id, FullName, Salary, Position FROM Employee", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Salary = reader.GetDecimal(2),
                        Position = reader.GetInt32(3)
                    });
                }
            }
            return employees;
        }

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [User] (Login, Password, EmployeeId, RoleId) VALUES (@Login, @Password, @EmployeeId, @RoleId)", connection);
                command.Parameters.AddWithValue("@Login", user.Login);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@EmployeeId", user.EmployeeId);
                command.Parameters.AddWithValue("@RoleId", user.RoleId);
                command.ExecuteNonQuery();
            }
        }
    }

}