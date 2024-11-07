using System;
using System.Collections.Generic;

namespace netColaboradores.Models;

public partial class Profesor
{
    public int IdProfesor { get; set; }

    public int? Fkcolaborador { get; set; }

    public string? Correo { get; set; }

    public string? Departamento { get; set; }

    public virtual Colaborador? FkcolaboradorNavigation { get; set; }
}
