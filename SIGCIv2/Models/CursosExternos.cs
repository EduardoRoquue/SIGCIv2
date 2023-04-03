using System.ComponentModel.DataAnnotations;

namespace SIGCIv2.Models
{
    public class CursosExternos
    {
        public int IdCursoExterno { get; set; }
        public string NombreCurso { get; set; }

        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime Termino { get; set; }
        public string Horas { get; set; }
        public string Dias { get; set; }
        public string Inscritos { get; set; }
        public string Proveedor { get; set; }
        public string Objetivo { get; set; }
        public string Costo { get; set; }
        public string Tipo { get; set; }
        public string Modalidad { get; set; }
        public string Instructor { get; set; }
        public int Evaluacion { get; set; }

    }
}
