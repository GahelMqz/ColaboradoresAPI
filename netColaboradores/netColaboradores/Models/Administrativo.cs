using System;
using System.Collections.Generic;

namespace netColaboradores.Models;

public partial class Administrativo
{
    public int IdAdministrativo { get; set; }

    public int? Fkcolaborador { get; set; }

    public string? Correo { get; set; }

    public string? Puesto { get; set; }

    public decimal? Nomina { get; set; }

    public virtual Colaborador? FkcolaboradorNavigation { get; set; }
}
