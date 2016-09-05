using Kviz4Harun.Helpers;
using Kviz4Harun.ViewModels.Home;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Kviz4Harun.Controllers
{
    public class HomeController : Controller
    {
        public Context db = new Context();

        public ActionResult Index()
        {
            var vm = new HomeIndexViewModel();
            vm.AvailableQuizes = db.Quizes.Where(p => p.isPublished).Select(x => new HomeIndexViewModel.QuizInfo
            {
                vId = x.vId,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.Teacher.Name,
                Name = x.Name
            }).ToList();

            return View(vm);
        }

        public new ActionResult Profile()//instead of having roles, im using this method to determine where to send the logged in user(pupil or teacher)
        {
            int logiraniKorisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Id;
            if (db.Pupils.Where(x => x.UserId == logiraniKorisnikId).FirstOrDefault() != null)
                return RedirectToAction("Index", "Pupil");
            else
                return RedirectToAction("Index", "Teacher");
        }

        public ActionResult Login(string username, string password)
        {
            if (Autentifikacija.Login(username, password, HttpContext))
                return RedirectToAction("Index");
            else
                throw new NotImplementedException();
        }

        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext, false);
            return RedirectToAction("Index");
        }
    }
}