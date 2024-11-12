using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BookShopBusiness;
using BookShopRepository;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

namespace BookShopAPI.Controllers
{
    [Route("odata/[controller]")]
    public class UsersController : ODataController
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [EnableQuery]
        [HttpGet("GetAll")]
        public IActionResult GetUser()
        {
            var users = _usersRepository.GetAllUsers();

            return Ok(users);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetUserById([FromODataUri] int id)
        {
            var user = _usersRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _usersRepository.SaveUser(user);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] Users user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            try
            {
                var existingUser = _usersRepository.GetUserById(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                user.Password = existingUser.Password;

                _usersRepository.UpdateUser(user);
            }
            catch
            {
                if (_usersRepository.GetUserById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("Update Success");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromODataUri] int id)
        {
            var user = _usersRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _usersRepository.DeleteUser(user);
            return NoContent();
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] Users loginUser)
        {
            var user = _usersRepository.GetAllUsers()
                                       .FirstOrDefault(u => u.UserName == loginUser.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(user);
        }
    }
}
