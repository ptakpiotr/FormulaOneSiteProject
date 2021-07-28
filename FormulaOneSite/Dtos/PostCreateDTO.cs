using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Dtos
{
    public class PostCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        [Url]
        public string PhotoSrc { get; set; }
    }
}
