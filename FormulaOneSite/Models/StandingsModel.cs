using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Models
{
    public class StandingsModel
    {
        public int Position { get; set; }
        public double Points { get; set; }
        public int wins { get; set; }
        public DriverModel Driver { get; set; }
        public List<ConstructorModel> Constructors { get; set; }
    }
}
