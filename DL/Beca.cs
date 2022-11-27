using System;
using System.Collections.Generic;

namespace DL;

public partial class Beca
{
    public byte IdBeca { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<Alumno> Alumnos { get; } = new List<Alumno>();
}
