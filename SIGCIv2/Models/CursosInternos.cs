using System.ComponentModel.DataAnnotations;

namespace SIGCIv2.Models
{
    public class CursosInternos
    {
        public int IdInterno { get; set; }

        public string Nombre { get; set; }
        public string Clave { get; set; }

        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime Termino { get; set; }
        public string Tipo { get; set; }
        public string Dirigido { get; set; }
    }
}
