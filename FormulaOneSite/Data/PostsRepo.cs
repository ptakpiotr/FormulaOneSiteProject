using FormulaOneSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Data
{
    public class PostsRepo : IPostsRepo
    {
        private readonly PostsDbContext _context;

        public PostsRepo(PostsDbContext context)
        {
            _context = context;
        }

        public void AddOnePost(PostModel post)
        {
            _context.Posts.Add(post);
        }

        public List<PostModel> GetAllPosts()
        {
            var res = _context.Posts.ToList();

            return res;
        }

        public PostModel GetOnePost(Guid id)
        {
            var res = _context.Posts.FirstOrDefault(p => p.Id == id);

            return res;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
