using Kviz4Harun.Helpers;
using Kviz4Harun.Models;
using Kviz4Harun.ViewModels.Quiz;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Kviz4Harun.Controllers
{
    [Autorizacija]
    public class QuizController : Controller
    {
        Context db = new Context();

        public ActionResult NewSession(int Id) //Id === vId
        {
            NewSessionViewModel vm = new NewSessionViewModel();
            Quiz q = db.Quizes.Include(x => x.Teacher).Where(x => x.vId == Id).FirstOrDefault();
            vm.vId = q.vId;
            vm.Teacher = q.Teacher.Name;
            vm.Name = q.Name;
            vm.CreatedAt = q.CreatedAt;

            return View(vm);
        }

        [HttpPost]
        public PartialViewResult StartQuiz(int QuizvId)
        {
            NewSessionViewModel vm = new NewSessionViewModel();
            Quiz q = db.Quizes.Include(x => x.Teacher).Include(x => x.Questions).Where(x => x.vId == QuizvId).FirstOrDefault();
            vm.vId = q.vId;
            vm.Teacher = q.Teacher.Name;
            vm.Name = q.Name;
            vm.CreatedAt = q.CreatedAt;

            QuizSession session = new QuizSession();
            session.QuizId = q.Id;
            session.StartedAt = DateTime.Now;
            session.UserId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Id;
            session.WrongAnswers = 0;
            session.CorrectAnswers = 0;

            db.QuizSessions.Add(session);
            db.SaveChanges();

            vm.SessionId = session.Id;

            int i = 1;
            foreach (var question in q.Questions)
            {
                session.PupilAnswers.Add(new Answer
                {
                    AndsweredCorrectly = null,
                    QuestionId = question.Id,
                    QuestionOptionId = null,
                    QuizSessionId = session.Id,
                    SeqNumber = i
                });
                i++;
            }

            db.SaveChanges();

            vm.CurrentQuestion = q.Questions.Where(qu => qu.Id == session.PupilAnswers.Where(pa => pa.SeqNumber == 1).First().QuestionId)
                .Select(x => new NewSessionViewModel.QuestionInfo
                {
                    Id = x.Id,
                    PossibleAnswers = db.QuestionOptions.Where(qo => qo.QuestionId == x.Id).Select(y => new NewSessionViewModel.QuestionInfo.OptionInfo
                    {
                        Id = y.Id,
                        Text = y.Text
                    }).ToList(),
                    Text = x.Text,
                    SeqNumber = 1,
                    IsLastQuestion = false
                }).FirstOrDefault();

            return PartialView("_QuizQuestionPartial", vm);
        }

        [HttpPost]
        public PartialViewResult PreviousQuestion(int SessionId, int SeqNumber)
        {
            NewSessionViewModel vm = new NewSessionViewModel();
            Answer PreviousQuestion = db.Answer.Include(x => x.Question).Where(x => x.QuizSessionId == SessionId && x.SeqNumber == (SeqNumber - 1)).FirstOrDefault();
            if (PreviousQuestion == null)
                return null;

            vm.SessionId = SessionId;
            vm.CurrentQuestion.Id = PreviousQuestion.QuestionId;
            vm.CurrentQuestion.IsLastQuestion = false;
            vm.CurrentQuestion.SeqNumber = PreviousQuestion.SeqNumber;
            vm.CurrentQuestion.Text = PreviousQuestion.Question.Text;
            vm.CurrentQuestion.PossibleAnswers = db.QuestionOptions.Where(qo => qo.QuestionId == PreviousQuestion.QuestionId).Select(x => new NewSessionViewModel.QuestionInfo.OptionInfo
            {
                Id = x.Id,
                Text = x.Text
            }).ToList();
            vm.CurrentQuestion.ChosenAnswerId = PreviousQuestion.QuestionOptionId;

            return PartialView("_QuizQuestionPrevNextPartial", vm);
        }

        [HttpPost]
        public PartialViewResult NextQuestion(int SessionId, int SeqNumber)
        {
            NewSessionViewModel vm = new NewSessionViewModel();
            Answer nextQuestion = db.Answer.Include(x => x.Question).Where(x => x.QuizSessionId == SessionId && x.SeqNumber == (SeqNumber + 1)).FirstOrDefault();
            if (nextQuestion == null)
                return null;

            vm.SessionId = SessionId;
            vm.CurrentQuestion.Id = nextQuestion.QuestionId;
            var lastAnswer = db.Answer.Where(x => x.QuizSessionId == SessionId).ToList().Last();
            vm.CurrentQuestion.IsLastQuestion = lastAnswer.QuestionId == nextQuestion.QuestionId ? true : false;
            vm.CurrentQuestion.SeqNumber = nextQuestion.SeqNumber;
            vm.CurrentQuestion.Text = nextQuestion.Question.Text;
            vm.CurrentQuestion.PossibleAnswers = db.QuestionOptions.Where(qo => qo.QuestionId == nextQuestion.QuestionId).Select(x => new NewSessionViewModel.QuestionInfo.OptionInfo
            {
                Id = x.Id,
                Text = x.Text
            }).ToList();
            vm.CurrentQuestion.ChosenAnswerId = nextQuestion.QuestionOptionId;

            return PartialView("_QuizQuestionPrevNextPartial", vm);
        }

        [HttpPost]
        public void TryAnswer(int SessionId, int QuestionId, int Answer) //Answer === QuestionOptionId
        {
            Answer answer = db.Answer.Where(x => x.QuizSessionId == SessionId && x.QuestionId == QuestionId).First();
            Question question = db.Questions.Include(x => x.PossibleAnswers).Where(x => x.Id == QuestionId).First();

            answer.AnsweredAt = DateTime.Now;
            answer.QuestionOptionId = Answer;
            if (question.CorrectAnswerId == answer.QuestionOptionId) answer.AndsweredCorrectly = true;
            else answer.AndsweredCorrectly = false;

            db.SaveChanges();
        }

        [HttpPost]
        public ActionResult EndQuiz(int SessionId)
        {
            //ToDo: kad user klikne na zavrsi button ovde ode request, popune se svi podaci na sesiji i pokaze se useru result screen
            QuizSession session = db.QuizSessions.Include(x => x.PupilAnswers).Where(x => x.Id == SessionId).FirstOrDefault();

            session.FinishedAt = DateTime.Now;
            foreach (var answer in session.PupilAnswers)
            {
                if (answer.AndsweredCorrectly.HasValue && answer.AndsweredCorrectly.Value)
                    session.CorrectAnswers++;
                else
                    session.WrongAnswers++;
            }

            db.SaveChanges();

            QuizSession LastSession = db.QuizSessions.Include(x => x.Quiz).Include(x => x.User).ToList().Last();
            if (!(Autentifikacija.GetLogiraniKorisnik(HttpContext).Id == LastSession.UserId && LastSession.Id == SessionId))
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Results", LastSession);
        }
    }
}