using System;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using aspnetcore.api.swagger.Models;
using aspnetcore.api.swagger.Repository;

namespace aspnetcore.api.swagger.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        /// <summary>Lista todos os funcionários</summary>
        /// <returns>Os itens da entidade Employee</returns>
        /// <response code="200">Retorna os itens Os itens da entidade Employee</response>
        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            List<Employee> ListEmployees = new List<Employee>();

            using (EmployeeRepository Repository = new EmployeeRepository())
            {
                ListEmployees.AddRange(Repository.List());
            }

            return Ok(ListEmployees);
        }

        /// <summary>Obtém um item funcionário pela chave primária</summary>
        /// <returns>Entidade do tipo Employee</returns>
        /// <response code="200">Retorna objeto Employee</response>
        /// <response code="400">Se houver algum problema</response>
        /// <response code="404">Se o funcionário não for encontrado</response>
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Employee> Get(int Id)
        {
            Employee Employee = new Employee();

            try
            {
                using (EmployeeRepository repository = new EmployeeRepository())
                {
                    Employee E = repository.FindById(Id);

                    if (E == null) 
                        return NotFound();

                    Employee.Id = E.Id;
                    Employee.Name = E.Name;
                    Employee.Email = E.Email;
                    Employee.Age = E.Age;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
                return BadRequest();
            }

            return Ok(Employee);
        }

        /// <summary>Modifica os informações do funcionário</summary>
        /// <returns>Objeto do tipo Employee</returns>
        /// <response code="200">Retorna objeto Employee com dados modificados</response>
        /// <response code="400">Se houver algum problema</response>
        /// <response code="404">Se o funcionário não for encontrado</response>
        /// <response code="422">Se a objeto enviado for inválido</response>
        [HttpPut("{Id}")]
        public ActionResult<Employee> Put(int Id, [FromBody] Employee Employee)
        {
            try
            {
                if(Employee.Id == 0 || Employee.Name == null || Employee.Email == null || Employee.Age == 0)
                    return StatusCode(422, Employee);
                
                using (EmployeeRepository Repository = new EmployeeRepository())
                {
                    Employee E = Repository.FindById(Id|Employee.Id);

                    if (E == null)
                        return NotFound();

                    E.Name = Employee.Name;
                    E.Email = Employee.Email;
                    E.Age = Employee.Age;

                    Repository.Update(E);
                }
            }
            catch (Exception e) {
                Console.WriteLine("{0}", e);
                return BadRequest();
            }

            return Ok(Employee);
        }

        /// <summary>Remove um registro de funcionário</summary>
        /// <returns>Objeto do tipo Employee</returns>
        /// <response code="200">Mensagem de sucesso</response>
        /// <response code="422">Se o id enviado for inválido</response>
        [HttpDelete("{Id}")]
        public ActionResult<ExpandoObject> Delete(int Id)
        {
            if (Id == 0)
                return StatusCode(422);

            using (EmployeeRepository Repository = new EmployeeRepository())
            {
                Employee Employee = new Employee
                {
                    Id = Id
                };
                Repository.Delete(Employee);
            }

            dynamic Obj = new ExpandoObject();
            Obj.Msg = "Employee "+Id+" removed!";
            return Ok(Obj);
        }

        /// <summary>Grava um novo registro de funcionário</summary>
        /// <returns>Objeto do tipo Employee</returns>
        /// <response code="200">Retorna objeto Employee com a chave primária criada</response>
        /// <response code="400">Se houver algum problema</response>
        /// <response code="422">Se a objeto enviado for inválido</response>
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee Employee)
        {
            try
            {
                if (Employee.Name == null || Employee.Email == null || Employee.Age == 0)
                    return StatusCode(422, Employee);

                using (EmployeeRepository Repository = new EmployeeRepository())
                {
                    Repository.Save(Employee);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
                return BadRequest();
            }

            return Ok(Employee);
        }

    }
}
