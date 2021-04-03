using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MyApp.Model;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.Remoting.Messaging;

namespace MyApp.Db.DbOperations
{
    public class EmployeeRepository
    {
        public int AddEmployee(EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities())
            {
                Employee emp = new Employee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Code = model.Code

                };
                if (model.Address != null)
                {
                    emp.Address = new Address()
                    {
                        Details = model.Address.Details,
                        Country = model.Address.Country,
                        State = model.Address.State
                    };
                }
                context.Employee.Add(emp);
                context.SaveChanges();
                return emp.id;

            }

        }
        public List<EmployeeModel> GetAllEmployees()
        {
            using (var context = new EmployeeDBEntities())
            {
                var result = context.Employee.Select(x => new EmployeeModel()
                {
                    Id = x.id,
                    AddressId = x.AddressId,
                    Code = x.Code,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = new AddressModel()
                    {
                        Details = x.Address.Details,
                        Country = x.Address.Country,
                        State = x.Address.State
                    }
                }
                    )
                    .ToList();
                return result;
               
            }
            
        }
        public EmployeeModel GetEmployee(int id)
        {
            using (var context = new EmployeeDBEntities())
            {
                var result = context.Employee.Where(x => x.id == id)
                    .Select(x => new EmployeeModel()
                    {
                        Id = x.id,
                        AddressId = x.AddressId,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = new AddressModel()
                        {
                            Id = x.Address.Id,
                            Details = x.Address.Details,
                            Country = x.Address.Country,
                            State = x.Address.State
                        }
                    }).FirstOrDefault();
                return result;
            }
        }
        public bool UpdateEmployee(int id, EmployeeModel model)
        {
            using(var context=new EmployeeDBEntities())
            {
                var employee = context.Employee.FirstOrDefault(x => x.id == id);
                    if (employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.Code = model.Code;
                   
                }
                context.SaveChanges();
                return true;
            }
        }
        public bool DeleteEmployee(int id)
        {
            using(var context=new EmployeeDBEntities())
            {
                var employee = context.Employee.FirstOrDefault(x => x.id == id);
                if(employee!= null)
                {
                    context.Employee.Remove(employee);
                    context.SaveChanges();
                }
                return true;
            }
        }
    }
    
}

