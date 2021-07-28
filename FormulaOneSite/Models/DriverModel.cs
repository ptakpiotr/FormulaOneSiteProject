using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Models
{
    public class DriverModel
    {
        public string DriverId { get; set; }
        [Url]
        public string Url { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Nationality { get; set; }
    }
}
