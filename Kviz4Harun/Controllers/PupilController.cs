using Kviz4Harun.Helpers;
using Kviz4Harun.ViewModels;
using Kviz4Harun.ViewModels.Pupil;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Kviz4Harun.Controllers
{
    [Autorizacija]
    public class PupilController : Controller
    {
        Context db = new Context();
        // GET: Pupil
        public ActionResult Index()
        {
            PupilIndexViewModel vm = new PupilIndexViewModel();
            LogiraniKorisnikVM k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            vm.username = k.username;
            vm.Name = k.Ime + ' ' + k.Prezime;
            vm.QuizSessions = db.QuizSessions.Include(x => x.Quiz).Where(x => x.UserId == k.Id).Select(y => new PupilIndexViewModel.QuizSessionInfo
            {
                Id = y.Id,
                QuizName = y.Quiz.Name,
                QuizvId = y.Quiz.vId,
                StartDate = y.StartedAt.Value,
                ResultPercentage = ((int)(((float)y.CorrectAnswers) / (y.CorrectAnswers + y.WrongAnswers) * 100)).ToString() + "%",
                QuizDuration = DbFunctions.DiffMinutes(y.StartedAt, y.FinishedAt).ToString()
            }).OrderByDescending(x => x.StartDate).ToList();

            return View(vm);
        }
    }
}