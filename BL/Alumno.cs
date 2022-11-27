namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DpascualAlfaSolucionesContext context = new DL.DpascualAlfaSolucionesContext())
                
                {
                    DL.Alumno alumnoLinq = new DL.Alumno();

                    alumnoLinq.Nombre = alumno.Nombre;
                    alumnoLinq.ApellidoPaterno = alumno.ApellidoPaterno;
                    alumnoLinq.ApellidoMaterno = alumno.ApellidoMaterno;
                    alumnoLinq.Edad = alumno.Edad;
                    alumnoLinq.Genero = alumno.Genero;
                    alumnoLinq.FechaNacimiento = alumno.FechaNacimiento;
                    alumnoLinq.IdBeca = alumno.Beca.IdBeca;
                    //alumnoLinq.IdBecaNavigation = new DL.Beca();
                    //alumnoLinq.IdBecaNavigation.IdBeca = alumno.Beca.IdBeca;

                    if (alumnoLinq != null)
                    {
                        context.Alumnos.Add(alumnoLinq);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DpascualAlfaSolucionesContext context = new DL.DpascualAlfaSolucionesContext())
                {
                    var query = (from a in context.Alumnos
                                 where a.IdAlumno == alumno.IdAlumno
                                 select a).SingleOrDefault();
                    if (query != null)
                    {

                        query.Nombre = alumno.Nombre;
                        query.ApellidoPaterno = alumno.ApellidoPaterno;
                        query.ApellidoMaterno = alumno.ApellidoMaterno;
                        query.Edad = alumno.Edad;
                        query.FechaNacimiento = alumno.FechaNacimiento;
                        query.Genero = alumno.Genero;
                        query.IdBeca = alumno.Beca.IdBeca;


                        context.SaveChanges();
                        result.Correct = true;
                    }

                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DpascualAlfaSolucionesContext context = new DL.DpascualAlfaSolucionesContext())
                {
                    
                    var query = (from a in context.Alumnos
                                 where a.IdAlumno == IdAlumno
                                 select a).FirstOrDefault();

                  
                    if (query != null)
                    {
                        context.Alumnos.Remove(query);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DpascualAlfaSolucionesContext context = new DL.DpascualAlfaSolucionesContext())
                {
                    var alumnos = (from alumnoLinq in context.Alumnos
                                  join becaLinq in context.Becas on alumnoLinq.IdBecaNavigation.IdBeca equals becaLinq.IdBeca
                                  join becaLinq2 in context.Becas on alumnoLinq.IdBeca equals becaLinq2.IdBeca
                                   select new
                                   { 
                                       IdAlumno = alumnoLinq.IdAlumno,
                                       Nombre = alumnoLinq.Nombre,
                                       ApellidoPaterno = alumnoLinq.ApellidoPaterno,
                                       ApellidoMaterno = alumnoLinq.ApellidoMaterno,
                                       Edad = alumnoLinq.Edad,
                                       FechaNacimiento = alumnoLinq.FechaNacimiento,
                                       Genero = alumnoLinq.Genero,
                                       IdBeca = alumnoLinq.IdBeca,
                                       Tipo = alumnoLinq.IdBecaNavigation.Tipo

                                   }).ToList();

                    if (alumnos != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objAlumno in alumnos)
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = objAlumno.IdAlumno;
                            alumno.Nombre = objAlumno.Nombre;
                            alumno.ApellidoPaterno = objAlumno.ApellidoPaterno;
                            alumno.ApellidoMaterno = objAlumno.ApellidoMaterno;
                            alumno.Edad = objAlumno.Edad.Value;
                            alumno.FechaNacimiento = (DateTime)objAlumno.FechaNacimiento;
                            alumno.Genero = objAlumno.Genero.Value;
                            alumno.Beca = new ML.Beca();
                            alumno.Beca.IdBeca = objAlumno.IdBeca.Value;
                            alumno.Beca.Tipo = objAlumno.Tipo;
                           
                            result.Objects.Add(alumno);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DpascualAlfaSolucionesContext context = new DL.DpascualAlfaSolucionesContext())
                {
                    var query = (from alumnoLinq in context.Alumnos
                                 join becaLinq in context.Becas on alumnoLinq.IdBecaNavigation.IdBeca equals becaLinq.IdBeca
                                 join becaLinq2 in context.Becas on alumnoLinq.IdBeca equals becaLinq2.IdBeca
                                 where alumnoLinq.IdAlumno == IdAlumno
                                 select new
                                 {
                                     IdAlumno = alumnoLinq.IdAlumno,
                                     Nombre = alumnoLinq.Nombre,
                                     ApellidoPaterno = alumnoLinq.ApellidoPaterno,
                                     ApellidoMaterno = alumnoLinq.ApellidoMaterno,
                                     Edad = alumnoLinq.Edad,
                                     FechaNacimiento = alumnoLinq.FechaNacimiento,
                                     Genero = alumnoLinq.Genero,
                                     IdBeca = alumnoLinq.IdBeca,
                                     Tipo = alumnoLinq.IdBecaNavigation.Tipo
                                 }).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Alumno alumno = new ML.Alumno();

                        alumno.IdAlumno = query.IdAlumno;
                        alumno.Nombre = query.Nombre;
                        alumno.ApellidoPaterno = query.ApellidoPaterno;
                        alumno.ApellidoMaterno = query.ApellidoMaterno;
                        alumno.Edad = query.Edad.Value;
                        alumno.FechaNacimiento = (DateTime)query.FechaNacimiento;
                        alumno.Genero = query.Genero.Value;
                        alumno.Beca = new ML.Beca();
                        alumno.Beca.IdBeca = query.IdBeca.Value;
                        alumno.Beca.Tipo = query.Tipo;

                        result.Object = alumno;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }

    }
}