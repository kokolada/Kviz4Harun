using System;
using System.Collections.Generic;

namespace Kviz4Harun.ViewModels.Teacher
{
    public class TeacherIndexViewModel
    {
        public TeacherIndexViewModel()
        {
            QuizSessions = new List<QuizSessionInfo>();
        }

        public string username { get; set; }
        public string Name { get; set; }

        public List<QuizInfo> AvailableQuizes { get; set; }
        public List<QuizSessionInfo> QuizSessions { get; set; }

        public class QuizInfo
        {
            public int vId { get; set; }
            public string Name { get; set; }
            public int QuestionCount { get; set; }
            public DateTime CreatedAt { get; set; }
            public string CreatedBy { get; set; }
        }

        public class QuizSessionInfo
        {
            public int Id { get; set; }
            public string ResultPercentage { get; set; }
            public DateTime StartDate { get; set; }
            public string QuizDuration { get; set; }
            public string QuizName { get; set; }
            public int QuizvId { get; set; }//for starting a new session of the same quiz
        }
    }
}