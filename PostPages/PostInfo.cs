using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTests
{
    public class PostInfo
    {
        public string Id { get; set; }
        public double Rate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {           
            return Id + Rate + Title + Text;
        }
    }
}
