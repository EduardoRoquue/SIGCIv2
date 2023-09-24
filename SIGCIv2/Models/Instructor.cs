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
        public int Extension { get; set; }

        [Required(ErrorMessage = "El horario es requerido")]
        [Display(Name = "Horario")]
        public string Horario { get; set; }

        [Display(Name = "Descanso")]
        public string Descanso { get; set; }

        [Display(Name = "Telefono Celular")]
        public string Tel1 { get; set; }

        [Display(Name = "Telefono fijo")]
        public string Tel2 { get; set; }

        [Display(Name = "Calificación")]
        public string CALIF { get; set; }

        [Display(Name = "Gerencia/Unidad/Taller/Línea")]
        public string Gerencia { get; set; }

        [Display(Name = "Materias a impartir")]
        public string Materias { get; set; }

        [Display(Name = "Actualización")]
        public string Actualizacion { get; set; }

        [Display(Name = "Calificación (ACT)")]
        public string Calif2 { get; set; }
    }
}
