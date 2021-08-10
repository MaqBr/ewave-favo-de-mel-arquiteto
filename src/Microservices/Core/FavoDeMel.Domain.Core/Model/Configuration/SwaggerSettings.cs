namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class SwaggerSettings
    {
        public SwaggerAutenticacaoSettings Autenticacao { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }
}