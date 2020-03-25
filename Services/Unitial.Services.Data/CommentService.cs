using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;

namespace Unitial.Services.Data
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> commentRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public CommentService(IRepository<Comment> commentRepository, IRepository<ApplicationUser> userRepository)
        {
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;
        }

        public ICollection<Comment> GetComments(string postId)
        {
            var comments = commentRepository
                .All()
                .Where(x => x.PostId == postId)
                .OrderBy(x => x.CommentOn)
                .ToList();

            return comments;
        }

        public void CreateComment(string postId, string authorId, string text)
        {

            var comment = new Comment()
            {
                PostId = postId,
                AuthorId = authorId,
                //ImageUrl = userRepository.All().Where(x=>x.Id == authorId).Select(x=>x.ImageUrl).FirstOrDefault(),
                CommentText = text,
            };
            commentRepository.AddAsync(comment);
            commentRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
