namespace SIGCIv2.Models
{
    public class Certificacion
    {
        public int IdCertificacion { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public DateOnly Fecha { get; set; }
    }
}
