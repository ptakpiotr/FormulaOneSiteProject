using AutoMapper;
using FormulaOneSite.Data;
using FormulaOneSite.Dtos;
using FormulaOneSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FormulaOneSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommentRepo _comments;
        private readonly IPostsRepo _posts;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ICommentRepo comments, IPostsRepo posts, IMapper mapper)
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

        public IActionResult ViewPost(string id)
        {
            Guid guid;
            Guid.TryParse(id, out guid);
            var res = _posts.GetOnePost(guid);
            if (res == null)
            {
                return View("Error");
            }
            var output = _mapper.Map<PostReadDTO>(res);
            var comments = _comments.GetCommentsForPost(guid);
            if (comments != null)
            {
                output.Comments.AddRange(comments);
            }

            return View(output);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
