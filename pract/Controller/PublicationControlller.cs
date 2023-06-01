using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using System.Threading.Tasks;
using DBTestOnAlacritas;

namespace Controller
{
    public class PublicationController
    {
        private List<Publication> publications;

        public PublicationController()
        {
            publications = LoadAll();
        }

        public List<Publication> LoadAll()
        {
            return LoadAllPublications(); // Call the updated method instead of recursive call
        }

        public static List<Publication> LoadAllPublications()
        {
            List<Publication> loadedPublications = DatabaseAdapter.LoadAll();
            if (loadedPublications != null)
            {
                return loadedPublications;
            }
            return null;
        }

       public List<Publication> SearchByResearcher(Researcher researcher)
{
    if (researcher == null)
    {
        return publications;
    }

    if (publications == null)
    {
        publications = LoadAllPublications();
    }

    var pubsByAuthor = from pub in publications
                       from author in pub.Authors
                       where author.Equals(researcher.GivenName)
                       select pub;

    return pubsByAuthor.ToList();
}


        public void Display()
        {
            foreach (Publication publication in publications)
            {
                Console.WriteLine(publication);
            }
        }

        public void LoadResearchers()
        {
            // Implementation to load researchers goes here
        }

        public Publication FilterbyName(string name)
        {
            return publications.FirstOrDefault(pub => pub.Authors.Contains(name));
        }

        public List<Publication> FilterbyLevel(string level)
        {
            return publications.Where(pub => pub.Type.Equals(level)).ToList();
        }
    }
}
