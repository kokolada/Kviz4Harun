namespace Kviz4Harun.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context context)
        {

            Context db = new Context();

            if (db.Quizes.Count() > 0) return;

            User user = new User();
            user.CreatedAt = DateTime.Now;
            user.username = "pupil";
            user.passwordSalt = Crypto.GenerateSalt();
            user.passwordHash = Crypto.HashPassword("test" + user.passwordSalt);
            db.Users.AddOrUpdate(user);

            db.SaveChanges();

            User user2 = new User();
            user2.CreatedAt = DateTime.Now;
            user2.username = "teacher";
            user2.passwordSalt = Crypto.GenerateSalt();
            user2.passwordHash = Crypto.HashPassword("test" + user2.passwordSalt);
            db.Users.AddOrUpdate(user2);

            db.SaveChanges();

            Pupil pupil = new Pupil();
            pupil.DateOfBirth = DateTime.Parse("1.1.2011");
            pupil.FirstName = "TestName";
            pupil.LastName = "TestLastName";
            pupil.UserId = user.Id;
            db.Pupils.AddOrUpdate(pupil);

            db.SaveChanges();

            Teacher teacher = new Teacher();
            teacher.Name = "TestTeacherName";
            teacher.UserId = user2.Id;
            db.Teachers.AddOrUpdate(teacher);

            db.SaveChanges();

            Question question = new Question();
            question.Text = "Is this data dummy?";
            question.CorrectAnswerId = null;
            db.Questions.AddOrUpdate(question);

            db.SaveChanges();

            QuestionOption answer = new QuestionOption();
            answer.Text = "Yes";
            answer.QuestionId = question.Id;
            db.QuestionOptions.AddOrUpdate(answer);

            db.SaveChanges();

            QuestionOption answer2 = new QuestionOption();
            answer2.Text = "No";
            answer2.QuestionId = question.Id;
            db.QuestionOptions.AddOrUpdate(answer2);

            db.SaveChanges();

            question.CorrectAnswerId = answer.Id;
            question.PossibleAnswers.Add(answer);
            question.PossibleAnswers.Add(answer2);

            db.SaveChanges();

            Question question2 = new Question();
            question2.Text = "Is this app dummy?";
            question2.CorrectAnswerId = null;
            db.Questions.AddOrUpdate(question2);

            db.SaveChanges();

            QuestionOption answer3 = new QuestionOption();
            answer3.Text = "Yes";
            answer3.QuestionId = question2.Id;
            db.QuestionOptions.AddOrUpdate(answer3);

            db.SaveChanges();

            QuestionOption answer4 = new QuestionOption();
            answer4.Text = "No";
            answer4.QuestionId = question2.Id;
            db.QuestionOptions.AddOrUpdate(answer4);

            db.SaveChanges();

            QuestionOption answer5 = new QuestionOption();
            answer5.Text = "Maybe";
            answer5.QuestionId = question2.Id;
            db.QuestionOptions.AddOrUpdate(answer5);

            db.SaveChanges();

            question2.CorrectAnswerId = answer5.Id;
            question2.PossibleAnswers.Add(answer3);
            question2.PossibleAnswers.Add(answer4);
            question2.PossibleAnswers.Add(answer5);

            db.SaveChanges();

            Question question3 = new Question();
            question3.Text = "Kako se voda prenosi?";
            question3.CorrectAnswerId = null;
            db.Questions.AddOrUpdate(question3);

            db.SaveChanges();

            QuestionOption answer6 = new QuestionOption();
            answer6.Text = "U kanti";
            answer6.QuestionId = question3.Id;
            db.QuestionOptions.AddOrUpdate(answer6);

            db.SaveChanges();

            QuestionOption answer7 = new QuestionOption();
            answer7.Text = "Preko ramena";
            answer7.QuestionId = question3.Id;
            db.QuestionOptions.AddOrUpdate(answer7);

            db.SaveChanges();

            QuestionOption answer8 = new QuestionOption();
            answer8.Text = "Sa koljena na koljeno";
            answer8.QuestionId = question3.Id;
            db.QuestionOptions.AddOrUpdate(answer8);

            db.SaveChanges();

            question3.CorrectAnswerId = answer8.Id;
            question3.PossibleAnswers.Add(answer6);
            question3.PossibleAnswers.Add(answer7);
            question3.PossibleAnswers.Add(answer8);

            db.SaveChanges();

            Quiz quiz = new Quiz();
            quiz.CreatedAt = DateTime.Now;
            quiz.Name = "The Dummy Quiz";
            quiz.isPublished = true;
            quiz.Questions.Add(question);
            quiz.Questions.Add(question2);
            quiz.Questions.Add(question3);
            quiz.TeacherId = teacher.Id;
            db.Quizes.AddOrUpdate(quiz);

            db.SaveChanges();

            question.QuizId = quiz.Id;
            question2.QuizId = quiz.Id;
            question3.QuizId = quiz.Id;

            Random rand = new Random();
            string randomNumber = rand.Next(10000, 99999).ToString();
            quiz.vId = Convert.ToInt32(quiz.Id.ToString() + randomNumber);

            db.SaveChanges();
        }
    }
}
