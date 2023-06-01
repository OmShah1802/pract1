using DBTestOnAlacritas;
using Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entity.Researcher;

namespace Controller
{
    public class ResearcherController
    {
        private const string connectionString = "server=alacritas.cis.utas.edu.au;database=kit206;uid=kit206;pwd=kit206;";
        private static List<Researcher> tempResearcherList;

        private List<Researcher> researcherList;
        DatabaseAdapter yes = new DatabaseAdapter();
        public ResearcherController()
        {
            researcherList = yes.LoadAllResearchers();
        }

        public List<Researcher> LoadAllResearchers()
        {
            return yes.LoadAllResearchers();
        }

        public void Display()
        {
            researcherList.ForEach(Console.WriteLine);
        }

        public Researcher FilterByName(string name)
        {
            foreach (Researcher researcher in researcherList)
            {
                if (researcher.GivenName.Equals(name))
                {
                    return researcher;
                }
            }
            return null;
        }

        public static List<Researcher> FilterByType(bool isStudent, List<Researcher> researchers)
{
    if (isStudent)
    {
        var filtered = from Researcher researcher in researchers
                       where researcher.LEVEL == Employment_level.Student
                       select researcher;
        tempResearcherList = new List<Researcher>(filtered);
    }
    else
    {
        var filtered = from Researcher researcher in researchers
                       where researcher.LEVEL != Employment_level.Student
                       select researcher;
        tempResearcherList = new List<Researcher>(filtered);
    }
    return tempResearcherList;
}






        public List<Researcher> FilterByLevel(Researcher.Employment_level level)
        {
            List<Researcher> filter = new List<Researcher>();
            foreach (Researcher researcher in researcherList)
            {
                if (researcher.LEVEL == level)
                {
                    filter.Add(researcher);
                }
            }
            return filter;
        }
    }
}
