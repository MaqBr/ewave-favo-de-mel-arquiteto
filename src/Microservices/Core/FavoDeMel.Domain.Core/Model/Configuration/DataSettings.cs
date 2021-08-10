using System;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class DataSettings : SettingsBase
    {
        public readonly string BankingConnection;
        public readonly string TransferConnection;
        public readonly int MemoryLimit;

        public DataSettings(IConfiguration configuration)
        {
            BankingConnection = configuration[AppSettings.Keys.ConnectionStrings.BANKING_CONNECTION_STRING];
            TransferConnection = configuration[AppSettings.Keys.ConnectionStrings.TRANSFER_CONNECTION_STRING];
            MemoryLimit = Convert.ToInt32(configuration[AppSettings.Keys.ConnectionStrings.MEMORY_LIMIT]);
        }

        public override string ToString()
        {
            var strB = new StringBuilder();
            strB.AppendLine("___________________________________________");
            strB.AppendLine($"________{nameof(DataSettings)}__________");
            strB.AppendLine(MontarTextoChaveValor(nameof(BankingConnection), BankingConnection));
            strB.AppendLine(MontarTextoChaveValor(nameof(TransferConnection), TransferConnection));
            strB.AppendLine("___________________________________________");
            return strB.ToString();
        }
    }
}