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
        public bool klaratProv { get; set; } // Blir true om man klarat prov och blir false efter 1 år.

        public override string ToString()
        {
            return förnamn + " " + efternamn;
        }
    }
}