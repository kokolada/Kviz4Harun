using Kviz4Harun.Helpers;
using Kviz4Harun.Models;
using Kviz4Harun.ViewModels;
using Kviz4Harun.ViewModels.Teacher;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Kviz4Harun.Controllers
{
    [Autorizacija]
    public class TeacherController : Controller
    {
        Context db = new Context();
        // GET: Teacher
        public ActionResult Index()
        {
            TeacherIndexViewModel vm = new TeacherIndexViewModel();
            LogiraniKorisnikVM k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            vm.username = k.username;
            vm.Name = k.Ime + ' ' + k.Prezime;
            vm.QuizSessions = db.QuizSessions.Include(x => x.Quiz).Where(x => x.UserId == k.Id).Select(y => new TeacherIndexViewModel.QuizSessionInfo
            {
                Id = y.Id,
                QuizName = y.Quiz.Name,
                QuizvId = y.Quiz.vId,
                StartDate = y.StartedAt.Value,
                ResultPercentage = ((int)(((float)y.CorrectAnswers) / (y.CorrectAnswers + y.WrongAnswers) * 100)).ToString() + "%",
                QuizDuration = DbFunctions.DiffMinutes(y.StartedAt, y.FinishedAt).ToString()
            }).OrderByDescending(x => x.StartDate).ToList();
            vm.AvailableQuizes = db.Quizes.Select(x => new TeacherIndexViewModel.QuizInfo
            {
                vId = x.vId,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.Teacher.Name,
                Name = x.Name,
                QuestionCount = x.Questions.Count
            }).ToList();

            return View(vm);
        }

        public ActionResult NewQuiz()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewQuiz(string QuizName)
        {
            Quiz q = new Quiz();
            q.CreatedAt = DateTime.Now;
            q.isPublished = false;
            q.Name = QuizName;
            var userID = Autentifikacija.GetLogiraniKorisnik(HttpContext).Id;
            q.TeacherId = db.Teachers.Where(x => x.UserId == userID).First().Id;
            db.Quizes.Add(q);

            db.SaveChanges();

            Random rand = new Random();
            string randomNumber = rand.Next(10000, 99999).ToString();
            q.vId = Convert.ToInt32(q.Id.ToString() + randomNumber);

            db.SaveChanges();

            return RedirectToAction("EditQuiz", new { vId = q.vId });
        }

        public PartialViewResult AddQuestions(int vId)
        {
            Quiz q = db.Quizes.Include(x => x.Questions.Select(y => y.PossibleAnswers)).Where(x => x.vId == vId).First();
            EditQuizViewModel vm = new EditQuizViewModel
            {
                QuizvId = q.vId,
                QuizName = q.Name,
                NewQuestion = null,
                Questions = q.Questions.Select(x => new EditQuizViewModel.QuestionInfo
                {
                    Id = x.Id,
                    Text = x.Text,
                    Answers = x.PossibleAnswers.Select(z => new EditQuizViewModel.QuestionInfo.AnswerInfo
                    {
                        Id = z.Id,
                        IsCorrect = z.Id == x.CorrectAnswerId,
                        Text = z.Text
                    }).ToList()
                }).OrderByDescending(x => x.Id).ToList()
            };
            vm.NewQuestion.Text = "";

            return PartialView("_AddQuizQuestionPartial", vm);
        }

        [HttpPost]
        public ActionResult AddQuestions(EditQuizViewModel model)
        {
            Question question = new Question();
            question.Text = model.NewQuestion.Text;
            question.CorrectAnswerId = null;
            question.QuizId = db.Quizes.Where(x => x.vId == model.QuizvId).First().Id;
            db.Questions.Add(question);
            db.SaveChanges();

            foreach (var answer in model.NewQuestion.Answers)
            {
                var a = new QuestionOption
                {
                    Text = answer.Text,
                    QuestionId = question.Id
                };
                db.QuestionOptions.Add(a);
                db.SaveChanges();

                if (answer.IsCorrect)
                    question.CorrectAnswerId = a.Id;
                question.PossibleAnswers.Add(a);

                db.SaveChanges();
            }

            return AddQuestions(model.QuizvId);
        }

        public ActionResult EditQuiz(int vId)
        {
            Quiz q = db.Quizes.Include(x => x.Questions.Select(y => y.PossibleAnswers)).Where(x => x.vId == vId).First();
            EditQuizViewModel vm = new EditQuizViewModel
            {
                QuizvId = q.vId,
                QuizName = q.Name,
                NewQuestion = null,
                Questions = q.Questions.Select(x => new EditQuizViewModel.QuestionInfo
                {
                    Id = x.Id,
                    Text = x.Text,
                    Answers = x.PossibleAnswers.Select(z => new EditQuizViewModel.QuestionInfo.AnswerInfo
                    {
                        Id = z.Id,
                        IsCorrect = z.Id == x.CorrectAnswerId,
                        Text = z.Text
                    }).ToList()
                }).OrderByDescending(x => x.Id).ToList()
            };

            return View(vm);
        }
    }
}