using System;

namespace Kviz4Harun.Models
{
    public class Pupil
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}