using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationWithADO.NET.Models;

namespace WebApplicationWithADO.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class uController : ControllerBase
    {
        private readonly IUserService _userService;
          //private readonly userService _userService;
        public uController(IUserService userService)
        {
            _userService=userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<user>>> GetUsers()
        {
           List<user> userlist=new List<user>();
           userlist = _userService.GetUsers();
           return userlist.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<user>> Getuser(int id)
        {

             user Specificuser = new user();
             Specificuser = _userService.GetUserbyid(id);
             return Specificuser;
        }

        //[HttpPut]
        //public async Task<IActionResult> Putusers(user user)
        //{

        //    var s=_userService.updateUser(user);
        //    return NoContent();
        //}

        [HttpPost]
        public async Task<IActionResult> Addusers(user user)
        {

            var s = _userService.addUser(user);
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Deleteuser(int id)
        //{

        //    _userService.DeleteUser(id);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMuser(string id)
        {
            _userService.DeleteUsers(id);
            return NoContent();
        }

        //[HttpPut]
        //public async Task<IActionResult> ActiveInActive([FromQuery] int[] ids)
        //{
        //    _userService.ActivateUser(ids);
        //    return NoContent();
        //}


        //[HttpPut]
        //public async Task<IActionResult> ActiveInActive(List<Tuple<int, bool>> user)
        //{

        //    var dt=_userService.ActivateUser1(user);
        //    return NoContent();
        //}

        [HttpPut]
        public async Task<IActionResult> ActiveInActive(List<user> user)
        {

            var dt = _userService.ActivateUserwithJason(user);
            return NoContent();
        }
    }
}
