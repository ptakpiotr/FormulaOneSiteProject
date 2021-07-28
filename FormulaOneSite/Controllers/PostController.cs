using AutoMapper;
using FormulaOneSite.Data;
using FormulaOneSite.Dtos;
using FormulaOneSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ICommentRepo _comments;
        private readonly IPostsRepo _posts;
        private readonly IMapper _mapper;

        public PostController(ILogger<PostController> logger,ICommentRepo comments,IPostsRepo posts,IMapper mapper)
        {
            _logger = logger;
            _comments = comments;
            _posts = posts;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var res = _posts.GetAllPosts().OrderByDescending(p => p.TimeOfCreation);

            var output = _mapper.Map<List<PostReadDTO>>(res);

            foreach (var p in output)
            {
                var comms = _comments.GetCommentsForPost(p.Id);
                if (comms != null)
                {
                    p.Comments.AddRange(comms);
                }

            }
            return View(output);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PostCreateDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                var post = _mapper.Map<PostModel>(createDTO);
                _posts.AddOnePost(post);
                _posts.SaveChanges();

                return View();
            }
            else
            {
                return View("Error");
            }
        }
    }
}
