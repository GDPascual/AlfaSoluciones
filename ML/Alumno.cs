using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        [DisplayName("Nombre:")]
        public string? Nombre { get; set; }
        [DisplayName("Apellido Paterno:")]
        public string? ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno:")]
        public string? ApellidoMaterno { get; set; }
        [DisplayName("Edad:")]
        [Range(15, 18)]
        public int Edad { get; set; }
        [DisplayName("Genero:")]
        public bool Genero { get; set; }
        [DisplayName("Fecha de nacimiento:")]
        public DateTime FechaNacimiento { get; set; }
        public List<object>? Alumnos { get; set; }
        public ML.Beca? Beca { get; set; }
    }
}