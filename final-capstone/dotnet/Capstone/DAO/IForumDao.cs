using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IForumDao
    {
        //Misc methods
        public List<Comment> GetCommentsByPostId(int postId); //need this to display comments on respective post



        //Anonymous user methods
        public Post GetPost(int postId); //Anonymous users can view any post
        public Comment GetComment(int commentId); //Anonymous users can view any comment
        public List<Post> GetPostsByUserId(int userId); //Anonymous users can view all posts by a specified user
        public List<Comment> GetCommentsByUserId(int userId); //Anonymous users can view all comments by a specified user



        //Registered user methods
        public Post AddPost(Post newPost); //Registered users can add a post on forum
        public Comment AddComment(Comment newComment); //Registered users can add a comment on a post on forum
        public List<Post> GetLoggedInUserPosts(int userId); //Registered users can view all their posts
        public List<Comment> GetLoggedInUserComments(int userId); //Registered users can view all their posts
    }
}
