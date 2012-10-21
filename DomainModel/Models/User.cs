using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
