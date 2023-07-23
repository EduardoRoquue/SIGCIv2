using System.ComponentModel.DataAnnotations;

namespace SIGCIv2.Models
{
    public class Instructor
    {
        
        public int IdInstructor { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Expediente del instructor")]
        [RegularExpression(@"[0-9]{1,5}", ErrorMessage = "Ingrese solo numeros y un máximo de 5 caractéres")]
        public int Expediente { get; set; }

        public string Nombre { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Correo del instructor")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La extension es requerida")]
        [Display(Name = "Extensión")]
        [RegularExpression(@"[0-9]{4,4}", ErrorMessage = "Ingrese solo numeros y 4 caractéres")]
        public string Extension { get; set; }

        [Required(ErrorMessage = "El horario es requerido")]
        [Display(Name = "Horario")]
        public string Horario { get; set; }

        public string Descanso { get; set; }

        public string Tel1 { get; set; }

        public string Tel2 { get; set; }

        public string CALIF { get; set; }

        public string Gerencia { get; set; }

        public string Materias { get; set; }

        public string Actualizacion { get; set; }

        public string Calif2 { get; set; }
    }
}
