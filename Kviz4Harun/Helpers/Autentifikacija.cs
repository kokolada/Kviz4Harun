using Kviz4Harun.Models;
using Kviz4Harun.ViewModels;
using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Kviz4Harun.Helpers
{
    public class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void PokreniNovuSesiju(LogiraniKorisnikVM korisnik, HttpContextBase context, bool zapamtiPassword)
        {
            context.Session.Add(LogiraniKorisnik, korisnik);

            if (zapamtiPassword)
            {
                HttpCookie cookie = new HttpCookie("_mvc_session", korisnik != null ? korisnik.Id.ToString() : "");
                cookie.Expires = DateTime.Now.AddDays(30);
                context.Response.Cookies.Add(cookie);
            }
        }

        public static LogiraniKorisnikVM GetLogiraniKorisnik(HttpContextBase context)
        {
            LogiraniKorisnikVM korisnik = (LogiraniKorisnikVM)context.Session[LogiraniKorisnik];

            return korisnik;
        }

        public static bool Login(string username, string password, HttpContextBase httpContext)
        {
            using (Context ctx = new Context())
            {
                var user = ctx.Users.FirstOrDefault(x => x.username == username);
                if (user != null)
                {
                    if (Crypto.VerifyHashedPassword(user.passwordHash, password + user.passwordSalt))
                    {
                        LogiraniKorisnikVM k = new LogiraniKorisnikVM();
                        k.Id = user.Id;
                        k.username = user.username;

                        PokreniNovuSesiju(k, httpContext, true);

                        return true;
                    }
                }
            }

            return false;

        }

        public static void CreateUser(string username, string password)
        {
            using (Context ctx = new Context())
            {
                var salt = Crypto.GenerateSalt();
                User user = new User
                {
                    username = username,
                    passwordHash = Crypto.HashPassword(password + salt),
                    passwordSalt = salt,
                    CreatedAt = DateTime.Now
                };
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }
    }
}