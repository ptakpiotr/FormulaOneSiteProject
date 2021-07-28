using FormulaOneSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Data
{
    public class CommentRepo : ICommentRepo
    {
        private readonly PostsDbContext _context;

        public CommentRepo(PostsDbContext context)
        {
            _context = context;
        }
        public void AddComment(CommentModel comment)
        {
            _context.Comments.Add(comment);
        }

        public List<CommentModel> GetCommentsForPost(Guid postId)
        {
            var res = _context.Comments.Where(c=>c.PostId==postId).ToList();

            return res;
        }

        public List<CommentModel> GetUserComments(string username)
        {
            var res = _context.Comments.Where(c => c.From == username).ToList();

            return res;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
