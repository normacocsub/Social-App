namespace SocialAppApi.Models
{
    public class ReaccionInputModel
    {
        public string Codigo { get; set; }
        public bool Like { get; set; }
        public bool Love { get; set; }
        public string IdUsuario { get; set; }
        public string IdPublicacion { get; set; }
    }
    
    public class ReaccionViewModel : ReaccionInputModel
    {

    }
}