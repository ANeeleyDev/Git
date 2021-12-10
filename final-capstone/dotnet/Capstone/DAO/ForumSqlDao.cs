using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class ForumSqlDao : IForumDao
    {
        private readonly string connectionString;

        public ForumSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<Comment> GetCommentsByPostId(int postId)
        {
            List<Comment> allCommentsByPostId = new List<Comment>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT comment_id, comment_user_id, comment_post_id, comment_content " +
                                                    "FROM comments " +
                                                    "WHERE comment_post_id = @comment_post_id", conn);
                    cmd.Parameters.AddWithValue("@comment_post_id", postId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Comment comment = CreateCommentFromReader(reader);
                        allCommentsByPostId.Add(comment);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allCommentsByPostId;
        }

        public Post GetPost(int postId) //returning a single post based on the unique postId
        {
            Post post = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT post_id, user_id, post_title, post_content " +
                             "FROM posts " +
                             "WHERE post_id = @post_id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@post_id", postId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    post = CreatePostFromReader(reader);
                }

                return post;
            }
        }

        public Comment GetComment(int commentId) //returning a single comment based on the unique commentId
        {
            Comment comment = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT comment_id, comment_user_id, comment_post_id, comment_content " +
                             "FROM comments " +
                             "WHERE comment_id = @comment_id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@comment_id", commentId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    comment = CreateCommentFromReader(reader);
                }

                return comment;
            }
        }

        public List<Post> GetPostsByUserId(int userId)
        {
            List<Post> allPostsByUserId = new List<Post>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT post_id, user_id, post_title, post_content " +
                                                    "FROM posts " +
                                                    "WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Post post = CreatePostFromReader(reader);
                        allPostsByUserId.Add(post);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allPostsByUserId;
        }

        public List<Comment> GetCommentsByUserId(int userId)
        {
            List<Comment> allCommentsByUserId = new List<Comment>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT comment_id, comment_user_id, comment_post_id, comment_content " +
                                                    "FROM comments " +
                                                    "WHERE comment_user_id = @comment_user_id", conn);
                    cmd.Parameters.AddWithValue("@comment_user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Comment comment = CreateCommentFromReader(reader);
                        allCommentsByUserId.Add(comment);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allCommentsByUserId;
        }

        public Post AddPost(Post newPost)
        {
            int newPostId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO posts (user_id, post_title, post_content) " +
                "OUTPUT INSERTED.post_id " +
                "VALUES (@user_id, @post_title, @post_content)", conn);

                cmd.Parameters.AddWithValue("@user_id", newPost.userId);
                cmd.Parameters.AddWithValue("@post_title", newPost.postTitle);
                cmd.Parameters.AddWithValue("@post_content", newPost.postContent);

                newPostId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetPost(newPostId);
        }

        public Comment AddComment(Comment newComment)
        {
            int newCommentId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO comments (comment_user_id, comment_post_id, comment_content) " +
                "OUTPUT INSERTED.comment_id " +
                "VALUES (@comment_user_id, @comment_post_id, @comment_content)", conn);

                cmd.Parameters.AddWithValue("@comment_user_id", newComment.commentUserId);
                cmd.Parameters.AddWithValue("@comment_post_id", newComment.commentPostId);
                cmd.Parameters.AddWithValue("@comment_content", newComment.commentContent);

                newCommentId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetComment(newCommentId);
        }

        public List<Post> GetLoggedInUserPosts(int userId)
        {
            List<Post> allPostsByUserId = new List<Post>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT post_id, user_id, post_title, post_content " +
                        "FROM posts " +
                        "WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Post post = CreatePostFromReader(reader);
                        allPostsByUserId.Add(post);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allPostsByUserId;
        }

        public List<Comment> GetLoggedInUserComments(int userId)
        {
            List<Comment> allCommentsByUserId = new List<Comment>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT comment_id, comment_user_id, comment_post_id, comment_content " +
                        "FROM comments " +
                        "WHERE comment_user_id = @comment_user_id", conn);
                    cmd.Parameters.AddWithValue("@comment_user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Comment comment = CreateCommentFromReader(reader);
                        allCommentsByUserId.Add(comment);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allCommentsByUserId;
        }

        public bool DeleteLoggedInUserPost(int postId, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM posts " +
                        "WHERE post_id = @post_id " +
                        "AND user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool DeleteLoggedInUserComment(int commentId, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM comments " +
                        "WHERE comment_id = @comment_id " +
                        "AND comment_user_id = @comment_user_id", conn);
                    cmd.Parameters.AddWithValue("@comment_id", commentId);
                    cmd.Parameters.AddWithValue("@comment_user_id", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool UpdateLoggedInUserPost(Post updatedPost, int userId, int postId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE posts " +
                        "SET post_title = @post_title, post_content = @post_content " +
                        "WHERE user_id = @user_id AND post_id = @post_id", conn);
                    cmd.Parameters.AddWithValue("@post_title", updatedPost.postTitle);
                    cmd.Parameters.AddWithValue("@post_content", updatedPost.postContent);
                    cmd.Parameters.AddWithValue("@user_id", updatedPost.userId);
                    cmd.Parameters.AddWithValue("@post_id", updatedPost.postId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool UpdateLoggedInUserComment(Comment updatedComment, int userId, int commentId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE comments " +
                        "SET comment_content = @comment_content " +
                        "WHERE comment_user_id = @comment_user_id AND comment_id = @comment_id", conn);
                    cmd.Parameters.AddWithValue("@comment_content", updatedComment.commentContent);
                    cmd.Parameters.AddWithValue("@comment_user_id", updatedComment.commentUserId);
                    cmd.Parameters.AddWithValue("@comment_id", updatedComment.commentId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool DeletePost(int postId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM posts " +
                        "WHERE post_id = @post_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool DeleteComment(int commentId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM comments " +
                        "WHERE comment_id = @comment_id", conn);
                    cmd.Parameters.AddWithValue("@comment_id", commentId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool UpdatePost(Post updatedPost, int postId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE posts " +
                        "SET post_title = @post_title, post_content = @post_content " +
                        "WHERE post_id = @post_id", conn);
                    cmd.Parameters.AddWithValue("@post_title", updatedPost.postTitle);
                    cmd.Parameters.AddWithValue("@post_content", updatedPost.postContent);
                    cmd.Parameters.AddWithValue("@post_id", updatedPost.postId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool UpdateComment(Comment updatedComment, int commentId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE comments " +
                        "SET comment_content = @comment_content " +
                        "WHERE comment_id = @comment_id", conn);
                    cmd.Parameters.AddWithValue("@comment_content", updatedComment.commentContent);
                    cmd.Parameters.AddWithValue("@comment_id", updatedComment.commentId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        private Post CreatePostFromReader(SqlDataReader reader)
        {
            Post post = new Post();
            post.postId = Convert.ToInt32(reader["post_id"]);
            post.userId = Convert.ToInt32(reader["user_id"]);
            post.postTitle = Convert.ToString(reader["post_title"]);
            post.postContent = Convert.ToString(reader["post_content"]);

            return post;
        }

        private Comment CreateCommentFromReader(SqlDataReader reader)
        {
            Comment comment = new Comment();
            comment.commentId = Convert.ToInt32(reader["comment_id"]);
            comment.commentUserId = Convert.ToInt32(reader["comment_user_id"]);
            comment.commentPostId = Convert.ToInt32(reader["comment_post_id"]);
            comment.commentContent = Convert.ToString(reader["comment_content"]);

            return comment;
        }
    }
}
