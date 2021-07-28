using FormulaOneSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Dtos
{
    public class PostReadDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PhotoSrc { get; set; }

        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}
