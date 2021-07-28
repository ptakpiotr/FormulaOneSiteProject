using FormulaOneSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UploadController : Controller
    {
        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(FileModel model)
        {
            if (ModelState.IsValid && Path.HasExtension(model.File.FileName))
            {
                var file = model.File;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","uploads",$"{model.Name}{Path.GetExtension(file.FileName)}");
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return RedirectToActionPermanent("Index","Home");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
