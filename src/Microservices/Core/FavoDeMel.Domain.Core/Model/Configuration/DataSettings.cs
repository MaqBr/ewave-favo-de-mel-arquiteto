using System;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class DataSettings : SettingsBase
    {
        public readonly string CatalogoConnection;
        public readonly string IdentityConnection;
        public readonly string VendaConnection;
        public readonly int MemoryLimit;

        public DataSettings(IConfiguration configuration)
        {
            CatalogoConnection = configuration[AppSettings.Keys.ConnectionStrings.Catalogo_CONNECTION_STRING];
            VendaConnection = configuration[AppSettings.Keys.ConnectionStrings.Venda_CONNECTION_STRING];
            IdentityConnection = configuration[AppSettings.Keys.ConnectionStrings.Identity_CONNECTION_STRING];
            MemoryLimit = Convert.ToInt32(configuration[AppSettings.Keys.ConnectionStrings.MEMORY_LIMIT]);
        }

        public override string ToString()
        {
            var strB = new StringBuilder();
            strB.AppendLine("___________________________________________");
            strB.AppendLine($"________{nameof(DataSettings)}__________");
            strB.AppendLine(MontarTextoChaveValor(nameof(CatalogoConnection), CatalogoConnection));
            strB.AppendLine(MontarTextoChaveValor(nameof(VendaConnection), VendaConnection));
            strB.AppendLine(MontarTextoChaveValor(nameof(IdentityConnection), IdentityConnection));
            strB.AppendLine("___________________________________________");
            return strB.ToString();
        }
    }
}