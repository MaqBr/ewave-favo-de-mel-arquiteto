using System.Security.Principal;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
