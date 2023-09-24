using System.ComponentModel.DataAnnotations;

namespace SIGCIv2.Models
{
    public class CursosInternos
    {
        public int IdInterno { get; set; }

        [Required(ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Nombre del curso")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="La {0} es requerida")]
        [Display(Name = "Clave del curso")]
        public string Clave { get; set; }

        [Required(ErrorMessage ="La fecha de inicio es requerida")]
        [Display(Name = "Fecha de inicio del curso")]
        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es requerida")]
        [Display(Name = "Fecha de fin del curso")]
        [DataType(DataType.Date)]
        public DateTime Termino { get; set; }

        [Required(ErrorMessage = "El tipo de curso es requerido")]
        [Display(Name = "Tipo de curso")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El personal al que va ridigido el curso es requerido")]
        [Display(Name = "Personal al que va dirigido")]
        public string Dirigido { get; set; }
    }
}
