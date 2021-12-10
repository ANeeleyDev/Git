using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Post
    {
        public int postId { get; set; }
        public int userId { get; set; }
        public string postTitle { get; set; }
        public string postContent { get; set; }
    }

    public class Comment
    {
        public int commentId { get; set; }
        public int commentPostId { get; set; }
        public int commentUserId { get; set; }
        public string commentContent { get; set; }
    }
}
