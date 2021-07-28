using FormulaOneSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Data
{
    public interface ICommentRepo
    {
        List<CommentModel> GetCommentsForPost(Guid postId);
        List<CommentModel> GetUserComments(string username);
        void AddComment(CommentModel comment);
        void SaveChanges();
    }
}
