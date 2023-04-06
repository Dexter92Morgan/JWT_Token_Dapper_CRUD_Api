using Datas;
using Datas.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Dapper_CRUD_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeController()
        {
            employeeRepository = new EmployeeRepository();
        }

        //GetAll
        [HttpGet]
        public IEnumerable<Employee> GetAllEmp()
        {

            return employeeRepository.GetAll();
        }

        ////GetBy ID
        [HttpGet("{id}")]
        public Employee GetByIdEmp(int id)
        {

            return employeeRepository.GetById(id);
        }

        //Insert
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Add(employee);
            }

        }

        //Update
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
            employee.EmpId = id;
            if (ModelState.IsValid)
            {
                employeeRepository.Update(employee);
            }

        }

        //Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeRepository.Delete(id);

        }

    }
}
