using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class PigResume
    {
        public string ID { get; set; }

        public string PigFarmID { get; set; }

        public DateTime BirthDate { get; set; }

        public string Type { get; set; }

        public string Slaughterhouse { get; set; }

        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime ModDate { get; set; }

        public string ModUser { get; set; }

    }
}