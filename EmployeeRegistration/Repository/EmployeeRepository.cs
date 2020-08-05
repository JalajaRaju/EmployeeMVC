using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EmployeeRegistration.Models;

namespace EmployeeRegistration.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private MySQLDBConnection db = new MySQLDBConnection();

        public IEnumerable<EmployeeTable> GetAllEmployee()
        {
            return db.EmployeeTables.OrderBy(S => S.DateOfBirth).ToList();
        }


        public EmployeeTable IsRFCExists(string RFC, int? ID)
        {
            if( ID != null)
            return db.EmployeeTables.Where(x => x.RFC == RFC && x.ID != ID).FirstOrDefault();
            else
                return db.EmployeeTables.Where(x => x.RFC == RFC).FirstOrDefault();

        }

        public EmployeeTable GetEmployeeById(int empID)
        {
            return db.EmployeeTables.Where(s => s.ID == empID).FirstOrDefault();
        }


        public IEnumerable<EmployeeTable> GetEmployeeByName(string searchString)
        {
            return db.EmployeeTables.Where(s => s.Name.ToLower().Contains(searchString.ToLower().Trim())
                                   || s.LastName.ToLower().Contains(searchString.ToLower().Trim())
                                   || (s.Name.ToLower().Trim() + " " + s.LastName.ToLower().Trim()).Contains(searchString.ToLower().Trim())).ToList();
        }



        public int AddEmployee(EmployeeTable employee)

        {

            int result = -1;

            if (employee != null)
            {
                db.EmployeeTables.Add(employee);
                db.SaveChanges();
                result = employee.ID;
            }
            return result;

        }

        public int UpdateEmployee(EmployeeTable employee)
        {
            int result = -1;

            if (employee != null)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                result = employee.ID;
            }
            return result;
        }

        public void DeleteEmployee(int employeeId)
        {
            EmployeeTable employee = db.EmployeeTables.Find(employeeId);
            db.EmployeeTables.Remove(employee);
            db.SaveChanges();

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
