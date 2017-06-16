using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<User> Get()
        {
            return _userManager.GetAllUser();
        }
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userManager.GetUserById(id);
        }
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _userManager.AddUser(user);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            _userManager.UpdateUser(user);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userManager.DeleteUser(id);
        }
    }
}