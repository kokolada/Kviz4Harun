﻿namespace Kviz4Harun.Models
{
    public class QuestionOption
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}