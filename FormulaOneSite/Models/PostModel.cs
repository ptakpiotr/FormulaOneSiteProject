using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Models
{
    public class PostModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }
        
        [Url]
        public string PhotoSrc { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TimeOfCreation { get; set; } = DateTime.UtcNow;
    }
}
