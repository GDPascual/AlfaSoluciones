using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Alumno : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public Alumno(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();


            ML.Result resultAlumno = new ML.Result();
            resultAlumno.Objects = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("Alumno/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                        resultAlumno.Objects.Add(resultItemList);
                    }
                }
                alumno.Alumnos = resultAlumno.Objects;
            }
            return View(alumno);

        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {

            ML.Alumno alumno = new ML.Alumno();
            alumno.Beca = new ML.Beca();
            ML.Result resultBecas = BL.Beca.GetAll();

            if (IdAlumno == null)
            {
                //Add
                alumno.Beca.Becas = resultBecas.Objects;
                return View(alumno);
            }
            else
            {
                //Actualizar

                //ML.Result result = BL.Alumno.GetById(IdAlumno.Value);

                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);
                        var responseTask = client.GetAsync("Alumno/GetById/" + IdAlumno);
                        responseTask.Wait();
                        var resultAlumno = responseTask.Result;
                        if (resultAlumno.IsSuccessStatusCode)
                        {
                            var readTask = resultAlumno.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            ML.Alumno resultItemList = new ML.Alumno();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(readTask.Result.Object.ToString());
                            alumno.Beca = new ML.Beca();

                            alumno = resultItemList;
                            alumno.Beca.Becas = resultBecas.Objects;

                            return View(alumno);
                        }
                        else
                        {
                            //
                        }
                    }

                    catch (Exception ex)
                    {
                        //
                    }

                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            if (alumno.IdAlumno == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                   

                    var postTask = client.PostAsJsonAsync("Alumno/add", alumno);
                    postTask.Wait();

                    var resultAseguradora = postTask.Result;
                    if (resultAseguradora.IsSuccessStatusCode)

                    {
                        ViewBag.Mensaje = "El alumno se registro correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "El alumno no se ha registrado correctamente" + result.ErrorMessage;
                    }
                }
            }
            else
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    var postTask = client.PostAsJsonAsync("Alumno/update/" + alumno.IdAlumno, alumno);
                    postTask.Wait();

                    var resultAseguradora = postTask.Result;
                    if (resultAseguradora.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "El alumno se ha actualizado correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "El alumno no se ha actualizado correctamente" + result.ErrorMessage;
                    }
                }

            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdAlumno)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);
                    var responseTask = client.DeleteAsync("Alumno/Delete/" + IdAlumno);
                    responseTask.Wait();
                    var resultAlumno = responseTask.Result;
                    if (resultAlumno.IsSuccessStatusCode)
                    {

                        return View("Modal");
                    }
                    else
                    {
                        //
                    }
                }

                catch (Exception ex)
                {
                    //
                }

                return View("Modal");
            }

        }

    }
}
