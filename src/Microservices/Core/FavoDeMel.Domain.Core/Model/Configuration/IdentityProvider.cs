using FavoDeMel.Domain.Core.Extensions;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class IdentityProvider : SettingsBase
    {
        public readonly string ScopesInline;

        public readonly string AuthorityUri;
        public readonly string[] Scopes;

        public IdentityProvider(IConfiguration configuration)
        {
            var authorityUri = configuration[AppSettings.Keys.SSO.AUTHORITY_URI];
            ScopesInline = configuration[AppSettings.Keys.SSO.SCOPES];

            AuthorityUri = authorityUri;
            Scopes = ScopesInline.IsNotEmpty<char>() ? ScopesInline.Split("|") : new string[] { };
        }

        public override string ToString()
        {
            var strB = new StringBuilder();
            strB.AppendLine("___________________________________________");
            strB.AppendLine($"________{nameof(IdentityProvider)}__________");
            strB.AppendLine(MontarTextoChaveValor(nameof(AuthorityUri), AuthorityUri));
            strB.AppendLine(MontarTextoChaveValor(nameof(Scopes), ScopesInline));
            strB.AppendLine("___________________________________________");
            return strB.ToString();
        }
    }
}