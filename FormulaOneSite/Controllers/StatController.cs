using FormulaOneSite.Data.ApiAccess;
using FormulaOneSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Controllers
{
    public class StatController : Controller
    {
        private readonly ILogger<StatController> _logger;
        private readonly IResultsApiAccess _access;

        public StatController(ILogger<StatController> logger,IResultsApiAccess access)
        {
            _logger = logger;
            _access = access;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Season(int year)
        {
            if (year >= 1950 && year <= DateTime.Now.Year)
            {
                List<StandingsModel> results = await _access.GetStandingsAsync(year);
                return View(results);
            }
            else
            {
                return View();
            }

        }
    }
}
