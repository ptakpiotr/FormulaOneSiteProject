using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Models
{
    public class CommentModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PostId { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string Content { get; set; }

        public int Likes { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TimeOfCreation { get; set; } = DateTime.UtcNow;
    }
}
