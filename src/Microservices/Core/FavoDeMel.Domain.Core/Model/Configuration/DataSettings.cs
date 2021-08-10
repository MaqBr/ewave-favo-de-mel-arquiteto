using System;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class DataSettings : SettingsBase
    {
        public readonly string CatalogoConnection;
        public readonly string TransferConnection;
        public readonly int MemoryLimit;

        public DataSettings(IConfiguration configuration)
        {
            CatalogoConnection = configuration[AppSettings.Keys.ConnectionStrings.Catalogo_CONNECTION_STRING];
            TransferConnection = configuration[AppSettings.Keys.ConnectionStrings.TRANSFER_CONNECTION_STRING];
            MemoryLimit = Convert.ToInt32(configuration[AppSettings.Keys.ConnectionStrings.MEMORY_LIMIT]);
        }

        public override string ToString()
        {
            var strB = new StringBuilder();
            strB.AppendLine("___________________________________________");
            strB.AppendLine($"________{nameof(DataSettings)}__________");
            strB.AppendLine(MontarTextoChaveValor(nameof(CatalogoConnection), CatalogoConnection));
            strB.AppendLine(MontarTextoChaveValor(nameof(TransferConnection), TransferConnection));
            strB.AppendLine("___________________________________________");
            return strB.ToString();
        }
    }
}