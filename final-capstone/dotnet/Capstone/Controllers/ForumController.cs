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

        [HttpDelete("myposts/{postId}")]
        [Authorize]
        public bool DeleteLoggedInUserPost(int postId, int userId) //Delete their post
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return forumDao.DeleteLoggedInUserPost(postId, userId);
        }

        [HttpDelete("mycomments/{commentId}")]
        [Authorize]
        public bool DeleteLoggedInUserComment(int commentId, int userId) //Delete their comment
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return forumDao.DeleteLoggedInUserComment(commentId, userId);
        }

        [HttpPut("myposts/{postId}")]
        [Authorize]
        public bool UpdateLoggedInUserPost(Post updatedPost, int userId, int postId) //Update their post
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return forumDao.UpdateLoggedInUserPost(updatedPost, userId, postId);
        }

        [HttpPut("mycomments/{commentId}")]
        [Authorize]
        public bool UpdateLoggedInUserComment(Comment updatedComment, int userId, int commentId) //Update their comment
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return forumDao.UpdateLoggedInUserComment(updatedComment, userId, commentId);
        }



        //Admin & Mod methods

        [HttpDelete("admin/posts/{postid}")]
        [Authorize(Roles = "admin, mod")]
        public bool DeletePost(int postId) //Can delete any post
        {
            return forumDao.DeletePost(postId);
        }

        [HttpDelete("admin/comments/{commentId}")]
        [Authorize(Roles = "admin, mod")]
        public bool DeleteComment(int commentId) //Can delete any comment
        {
            return forumDao.DeleteComment(commentId);
        }

        [HttpPut("admin/posts/{postId}")]
        [Authorize(Roles = "admin, mod")]
        public bool UpdatePost(Post updatedPost, int postId) //Can update any post
        {
            return forumDao.UpdatePost(updatedPost, postId);
        }

        [HttpPut("admin/comments/{commentId}")]
        [Authorize(Roles = "admin, mod")]
        public bool UpdateComment(Comment updatedComment, int commentId) //Can update any post
        {
            return forumDao.UpdateComment(updatedComment, commentId);
        }
    }
}
