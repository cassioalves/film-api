using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Entity
{
    public class Film : EntityBase
    {
        public int Year { get; set; }
        public string Title { get; set; }
        public string Studio { get; set; }
        public string Producer { get; set; }
        public bool Winner { get; set; }
    }
}