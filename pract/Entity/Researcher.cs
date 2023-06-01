using System;
using System.Collections.Generic;

namespace Entity
{
    public class Researcher
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Title { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Level { get; set; }
        public string Utas_start { get; set; }
        public string Current_start { get; set; }
        public Employment_level LEVEL;
        public List<Publication> Publications { get; set; }

        public override string ToString()
{
    return $"{Id}, {GivenName}, {FamilyName}, {Title}, {Campus}, {Email}, {Photo}, {Level}, {Utas_start}, {Current_start}";
}

        public enum Position
        {
            Student,
            Staff,
            Associate
        }

        public enum Employment_level
        {
            Unknown,
            Student,
            A,
            B,
            C,
            D,
            E
        }
    }
}
