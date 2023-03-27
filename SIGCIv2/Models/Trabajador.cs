using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SIGCIv2.Models
{
    public class Trabajador
    {
        public int IdTrabajador { get; set; }

        [Required(ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Expediente del trabajador")]
        [RegularExpression(@"[0-9]+",ErrorMessage ="Ingrese solo numeros")]
        [Remote(action:"VerificarExpediente", controller:"Trabajadores")]
        public int Expediente { get; set; }

        [Required(ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Nombre del trabajador")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage ="Solo letras")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="La {0} es requerida")]
        [Display(Name ="Categoria STC")]
        //[RegularExpression(@"")]
        public string IdCategoria { get; set; }

        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Gerencia/Subgerencia")]
        //[RegularExpression(@"")]
        public string Gerencia { get; set; }

        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Coordinación")]
        //[RegularExpression(@"")]
        public string Coordinacion { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Genero")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Solo letras")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Calidad laboral")]
        //[RegularExpression(@"^+[a-zA-Z\s]*$", ErrorMessage = "Solo letras")]
        public string CalidadLaboral { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Trabajador/Aspirante")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Seccional")]
        //[RegularExpression(@"")]
        public string Seccional { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Presenta alguna discapacidad")]
        //[RegularExpression(@"")]
        public string Discapacidad { get; set; }
    }
}
