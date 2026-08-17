using Microsoft.AspNetCore.Mvc;

namespace Medicare_API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        //[HttpGet]
        //[Route("GetUserById/{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var response = new UserModel { Id = id, UserName = "Harshit", Email = "harshitb2k@gmail.com" };
        //    return Ok(response);
        //}
    }
}
