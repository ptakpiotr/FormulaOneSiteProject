using AutoMapper;
using FormulaOneSite.Data;
using FormulaOneSite.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Hubs
{
    public class CommentsHub : Hub
    {
        private readonly ICommentRepo _comments;
        private readonly IMapper _mapper;

        public CommentsHub(ICommentRepo comments, IMapper mapper)
        {
            _comments = comments;
            _mapper = mapper;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task UpdateLikes(string message)
        {
            var msg = JsonConvert.DeserializeObject<dynamic>(message);
            Guid id;
            var bul = Guid.TryParse(msg.id.ToString(), out id);
            if (bul)
            {
                var commentToUpdate = _comments.GetCommentsForPost(id).Where(c => c.Id.ToString() == msg.commId.ToString()).First();
                var updated = commentToUpdate;
                updated.Likes++;
                _mapper.Map(updated, commentToUpdate);
                await Task.Run(() => _comments.SaveChanges());
            }
        }

        public async Task AddComment(string comment)
        {
            Console.WriteLine("HITIT!");
            var message = JsonConvert.DeserializeObject<CommentModel>(comment);
            var postId = message.PostId.ToString();
            Guid id;
            var bul = Guid.TryParse(postId, out id);
            message.PostId = id;
            message.From = Context.User.Identity.Name;
            message.Likes = 0;

            if (bul)
            {
                Console.WriteLine("HITIT22!");
                _comments.AddComment(message);
                _comments.SaveChanges();

                await Clients.All.SendAsync($"NewComment{postId}", JsonConvert.SerializeObject(message));

            }
        }
    }
}
