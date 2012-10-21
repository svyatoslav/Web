using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models
{
    public class Paging
    {
        public string BaseUrl { get; set; }
        public int Current { get; set; }
        public int Total { get; set; }
        public bool ShowNext { get; set; }
        public int NextUrl { get; set; }
        public bool ShowPrev { get; set; }
        public int PrevUrl { get; set; }
        public bool ShowFirst { get; set; }
        public int FirstUrl { get; set; }
        public bool ShowLast { get; set; }
        public int LastUrl { get; set; }
    }
}

