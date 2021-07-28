using FormulaOneSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Data.ApiAccess
{
    public interface IResultsApiAccess
    {
        Task<List<StandingsModel>> GetStandingsAsync(int year);
    }
}
