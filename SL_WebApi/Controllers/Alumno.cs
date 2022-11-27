using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Alumno : ControllerBase
    {
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();

            ML.Result result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        [Route("GetById/{IdAlumno}")]
        public IActionResult GetById(int IdAlumno)
        {
            ML.Result result = BL.Alumno.GetById(IdAlumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ML.Alumno alumno)
        {
            var result = BL.Alumno.Add(alumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("Delete/{IdAlumno}")]
        public IActionResult Delete(int IdAlumno)
        {

            ML.Result result = BL.Alumno.Delete(IdAlumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("update/{IdAlumno}")]
        public IActionResult Put(int IdAlumno, [FromBody] ML.Alumno alumno)
        {
            alumno.IdAlumno = IdAlumno;
            ML.Result result = BL.Alumno.Update(alumno);

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
