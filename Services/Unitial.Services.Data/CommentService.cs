using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels;

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

        public ICollection<CommetViewModel> GetComments(string postId)
        {
            var comments = commentRepository
                .All()
                .Where(x => x.PostId == postId)
                .Select(x => new CommetViewModel()
                {
                    Id = x.Id,
                    AuthorId = x.AuthorId,
                    CreatorProfilePic = userRepository.All().Where(y => x.AuthorId == y.Id).Select(x => x.ImageUrl).FirstOrDefault(),
                    CommentOn = x.CommentOn,
                    CommentText = x.CommentText,
                    PostId = x.PostId,
                })
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
                CommentText = text,
            };
            commentRepository.AddAsync(comment);
            commentRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
