using System.ComponentModel.DataAnnotations;

namespace SIGCIv2.Models
{
    public class AlumnoCursosExternos
    {
        public int IdAlumno { get; set; }
        public int AlumnoExp { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Discapacidad { get; set; }
        public string AprobReprob { get; set; }
        public string Adscripcion { get; set; }
        public string Edad { get; set; }
        public string Categoria { get; set; }
        public string Calidad { get; set; }
        [DataType(DataType.Date)]
        public DateTime EntregaConstancia { get; set; }
    }
}
