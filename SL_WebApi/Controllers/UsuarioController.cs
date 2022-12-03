using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        [Route("api/GetByUsername/{Username}")]
        public IActionResult Login(string Username)
        {
            ML.Result result = BL.Usuario.GetByUsername(Username);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
