using FormulaOneSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Data
{
    public interface IPostsRepo
    {
        List<PostModel> GetAllPosts();
        PostModel GetOnePost(Guid id);
        void AddOnePost(PostModel post);
        void SaveChanges();
    }
}
