using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("/forum/")]
    [ApiController]
    [Authorize]

    public class ForumController : ControllerBase
    {
        //Properties:
        private readonly IForumDao forumDao;



        //Constructor:
        public ForumController(IForumDao _forumDao)
        {
            forumDao = _forumDao;
        }



        //Misc methods:

        [HttpGet("post/{postId}/comments")]
        [AllowAnonymous]
        public List<Comment> GetCommentsByPostId(int postId)
        {
            return forumDao.GetCommentsByPostId(postId);
        }



        //Anonymous user methods

        [HttpGet("posts/{postId}")]
        [AllowAnonymous]
        public Post GetPost(int postId) //View any post
        {
            return forumDao.GetPost(postId);
        }

        [HttpGet("comments/{commentId}")]
        [AllowAnonymous]
        public Comment GetComment(int commentId) //View any comment
        {
            return forumDao.GetComment(commentId);
        }

        [HttpGet("user/{userId}/posts")]
        [AllowAnonymous]
        public List<Post> GetPostsByUserId(int userId) //View all posts by a specified user
        {
            return forumDao.GetPostsByUserId(userId);
        }

        [HttpGet("user/{userId}/comments")]
        [AllowAnonymous]
        public List<Comment> GetCommentsByUserId(int userId) //View all comments by a specified user
        {
            return forumDao.GetCommentsByUserId(userId);
        }



        //Registered user methods

        [HttpPost("myposts/add")]
        [Authorize]
        public Post AddPost(Post newPost) //Add post in forum
        {
            string userIdString = User.FindFirst("sub")?.Value;
            newPost.userId = Convert.ToInt32(userIdString);

            return forumDao.AddPost(newPost);
        }

        [HttpPost("mycomments/add")]
        [Authorize]
        public Comment AddComment(Comment newComment) //Add comment in forum
        {
            string userIdString = User.FindFirst("sub")?.Value;
            newComment.commentUserId = Convert.ToInt32(userIdString);

            return forumDao.AddComment(newComment);
        }

        [HttpGet("myposts")]
        [Authorize]
        public List<Post> GetLoggedInUserPosts(int userId) //View their posts
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return forumDao.GetLoggedInUserPosts(userId);
        }

        [HttpGet("mycomments")]
        [Authorize]
        public List<Comment> GetLoggedInUserComments(int userId) //View their comments
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return forumDao.GetLoggedInUserComments(userId);
        }
    }
}
