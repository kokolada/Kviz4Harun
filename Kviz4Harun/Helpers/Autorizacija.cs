using Kviz4Harun.ViewModels;
using System.Web.Mvc;

namespace Kviz4Harun.Helpers
{
    public class Autorizacija : FilterAttribute, IAuthorizationFilter
    {
        string _rola;
        public Autorizacija(string rola = "")
        {
            _rola = rola;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            LogiraniKorisnikVM k = Autentifikacija.GetLogiraniKorisnik(filterContext.HttpContext);

            if (k == null)
            {
                filterContext.HttpContext.Response.Redirect("/");
            }
        }
    }
}