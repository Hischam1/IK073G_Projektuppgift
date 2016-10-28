using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IK073G_Projektuppgift
{
    public class Prov
    {
        public int provId { get; set; }
        public int provDeltagare { get; set; }
        public string förnamn { get; set; }
        public string efternamn { get; set; }
        public string provTyp { get; set; }
        public DateTime datum { get; set; }
        public string provStatus { get; set; }
        public int antalRätt { get; set; }
        public int totalTid { get; set; }

        //public override string ToString()
        //{
        //    return provId + " " + provDeltagare + " " + provTyp;
        //}

    }
}