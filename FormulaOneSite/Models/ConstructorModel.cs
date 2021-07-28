using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Models
{
    public class ConstructorModel
    {
        public string constructorId { get; set; }
        [Url]
        public string url { get; set; }
        public string name { get; set; }
        public string nationality { get; set; }
    }
}
