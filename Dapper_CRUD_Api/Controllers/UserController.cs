using Datas.Repository;
using Datas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Datas.Models;

namespace Dapper_CRUD_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }

        //GetAll
        [HttpGet]
        public IEnumerable<UserInfo> GetAllEmp()
        {

            return userRepository.GetAll();
        }

        ////GetBy ID
        [HttpGet("{id}")]
        public UserInfo GetByIdEmp(int id)
        {

            return userRepository.GetById(id);
        }

        //Insert
        [HttpPost]
        public void Post([FromBody] UserInfo userdata)
        {
            if (ModelState.IsValid)
            {
                userRepository.Add(userdata);
            }

        }

        //Update
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserInfo userdata)
        {
            userdata.UserId = id;
            if (ModelState.IsValid)
            {
                userRepository.Update(userdata);
            }

        }

        //Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userRepository.Delete(id);

        }
    }
}
