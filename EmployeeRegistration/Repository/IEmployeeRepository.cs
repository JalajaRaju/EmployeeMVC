using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeRegistration.Models;

namespace EmployeeRegistration.Repository
{
    interface IEmployeeRepository : IDisposable
    {
        IEnumerable<EmployeeTable> GetAllEmployee();
        EmployeeTable GetEmployeeById(int ID);
        IEnumerable<EmployeeTable> GetEmployeeByName(string empName);
        EmployeeTable IsRFCExists(string RFC, int? ID);
        int AddEmployee(EmployeeTable employeeEntity);
        int UpdateEmployee(EmployeeTable employeeEntity);
        void DeleteEmployee(int employeeId);
    }
}
