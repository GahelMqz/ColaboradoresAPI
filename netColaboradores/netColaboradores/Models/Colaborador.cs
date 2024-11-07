using System;
using System.Collections.Generic;

namespace netColaboradores.Models;

public partial class Colaborador
{
    public int IdColaborador { get; set; }

    public string? Nombre { get; set; }

    public int? Edad { get; set; }

    public DateOnly? Birthday { get; set; }

    public bool? IsProfesor { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public virtual ICollection<Administrativo> Administrativos { get; set; } = new List<Administrativo>();

    public virtual ICollection<Profesor> Profesors { get; set; } = new List<Profesor>();
}
