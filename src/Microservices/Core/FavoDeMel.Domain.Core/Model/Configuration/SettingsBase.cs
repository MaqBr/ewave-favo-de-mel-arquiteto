using FavoDeMel.Domain.Core.Extensions;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public abstract class SettingsBase
    {
        protected string MontarTextoChaveValor(string chave, string valor)
        {
            var str = valor.IsNotEmpty() ? $"{chave}: {valor}" : $"{chave} não encontrado.";

            return str;

        }
    }
}
