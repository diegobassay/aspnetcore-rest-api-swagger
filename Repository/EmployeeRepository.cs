using LiteDB;
using System;
using System.Collections.Generic;

using aspnetcore.api.swagger.Contracts;
using aspnetcore.api.swagger.Models;

namespace aspnetcore.api.swagger.Repository
{
    public class EmployeeRepository : IRepository<Employee>, IDisposable
    {
        private LiteDatabase liteDatabase;

        LiteCollection<Employee> employeesCollection;

        public EmployeeRepository()
        {
            this.liteDatabase = new LiteDatabase(@"EmployeeDB.db");
            this.employeesCollection = this.liteDatabase.GetCollection<Employee>("employees");
        }

        public IEnumerable<Employee> List()
        {
            return this.employeesCollection.FindAll();
        }

        public void Save(Employee entity)
        {
            this.employeesCollection.Insert(entity);
        }

        public void Delete(Employee entity)
        {
            this.employeesCollection.Delete(x => x.Id == entity.Id);
        }

        public void Update(Employee entity)
        {
            Employee employee = this.FindById(entity.Id);

            employee.Name = entity.Name;
            employee.Email = entity.Email;
            employee.Age = entity.Age;

            this.employeesCollection.Update(employee);
        }

        public Employee FindById(int Id)
        {
            return this.employeesCollection.FindById(Id);
        }

        public void Dispose()
        {
            if (this.liteDatabase != null)
            {
                this.liteDatabase.Dispose();
            }
            GC.SuppressFinalize(this);
        }

    }
}
