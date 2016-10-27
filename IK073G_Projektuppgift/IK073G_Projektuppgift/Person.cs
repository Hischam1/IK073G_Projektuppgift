using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IK073G_Projektuppgift
{
    public class Person
    {
        public int anställningsID { get; set; }
        public string förnamn { get; set; }
        public string efternamn { get; set; }
        public string telefonnummer { get; set; }
        public bool nyanställd { get; set; }
        public bool anställd { get; set; }
        public bool admin { get; set; }

        public override string ToString()
        {
            return förnamn + " " + efternamn;
        }
    }
}