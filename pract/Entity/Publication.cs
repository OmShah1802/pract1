using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Publication
    {
        
        public string Title { get; set; }
        public int YearOfPublication { get; set; }
        public DateTime AvailableFrom { get; set; }
        public string Type { get; set; }
        public int Ranking { get; set; }
        public List<string> Authors { get; set; }

        public override string ToString()
        {
            return Title + ' ' + YearOfPublication + ' ' + AvailableFrom + ' ' + Type + ' ' + Ranking + ' ' + Authors;
        }
    }
}