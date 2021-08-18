using Microsoft.Extensions.Configuration;
using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class MicroServiceSettings : SettingsBase
    {
        public readonly string CatalogoBaseUrl;
        public readonly string VendaBaseUrl;
        public readonly string IdentityBaseUrl;

        public MicroServiceSettings(IConfiguration configuration)
        {
            CatalogoBaseUrl = configuration[AppSettings.Keys.MicroServices.CATALOGO_BASE_URI];
            VendaBaseUrl = configuration[AppSettings.Keys.MicroServices.VENDA_BASE_URI];
            IdentityBaseUrl = configuration[AppSettings.Keys.MicroServices.IDENTITY_BASE_URI];
        }

        public override string ToString()
        {
            var strB = new StringBuilder();
            strB.AppendLine("___________________________________________");
            strB.AppendLine($"________{nameof(DataSettings)}__________");
            strB.AppendLine(MontarTextoChaveValor(nameof(CatalogoBaseUrl), CatalogoBaseUrl));
            strB.AppendLine(MontarTextoChaveValor(nameof(VendaBaseUrl), VendaBaseUrl));
            strB.AppendLine(MontarTextoChaveValor(nameof(IdentityBaseUrl), IdentityBaseUrl));
            strB.AppendLine("___________________________________________");
            return strB.ToString();
        }
    }
}