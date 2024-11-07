using Microsoft.EntityFrameworkCore;

namespace netColaboradores.Models
{
    public class ColaboradorRequest
    {

        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsProfesor { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; } // Usado solo si es profesor
        public string Puesto { get; set; }       // Usado solo si es administrativo
        public decimal? Nomina { get; set; }     // Usado solo si es administrativo

    }
}
