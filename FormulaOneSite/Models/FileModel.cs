﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Models
{
    public class FileModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
