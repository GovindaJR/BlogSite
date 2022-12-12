using System.Data.SqlClient;
using BlogCity.Models;
using System.Drawing;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using BlogCity;

namespace BlogCity.Models
{
    public class Blog
    {
        List<Post> posts;
        List<Recipient> recipients;

        public Blog()
        {
            posts = new List<Post>();
            recipients = new List<Recipient>();
        }

        public List<Post> getPosts()
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Blog;Trusted_Connection=True;Encrypt=False;"))
            {
                sqlConnection.Open();
                List<Post> posts = new List<Post>();

                using (SqlCommand command = new SqlCommand("SELECT * FROM post ORDER BY ID DESC", sqlConnection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Post tempPost = new Post();
                        tempPost.ID = (int)reader.GetValue(0);
                        tempPost.Title = (string)reader.GetValue(1);
                        tempPost.Body = (string)reader.GetValue(2);
                        tempPost.Author = (string)reader.GetValue(3);
                        tempPost.Date = (DateTime)reader.GetValue(4);

                        posts.Add(tempPost);
                    }
                    Console.WriteLine("Returning");
                    return posts;
                }
            }
        }


        public Post getPostByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Blog;Trusted_Connection=True;Encrypt=False;"))
            {
                sqlConnection.Open();
                Post post = new Post();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM post WHERE ID={id} ", sqlConnection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        post.ID = (int)reader.GetValue(0);
                        post.Title = (string)reader.GetValue(1);
                        post.Body = (string)reader.GetValue(2);
                        post.Author = (string)reader.GetValue(3);
                        post.Date = (DateTime)reader.GetValue(4);
                    }
                    return post;
                }
            }
        }


        public void addPost(Post post)
        {
            Console.WriteLine("Attempted to Add Post");
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Blog;Trusted_Connection=True;Encrypt=False;"))
            {

                sqlConnection.Open();
                string query = "INSERT INTO Post (Title, Body, Author) VALUES(@Title, @Body, @Author)";
                SqlCommand command = new SqlCommand(query, sqlConnection);

                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Body", post.Body);
                command.Parameters.AddWithValue("@Author", post.Author);

                command.ExecuteNonQuery();

            }
        }

        public List<Recipient> getRecipients()
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Blog;Trusted_Connection=True;Encrypt=False;"))
            {
                sqlConnection.Open();
                List<Recipient> recipients = new List<Recipient>();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Recipient", sqlConnection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        RecipientFactory rf = new RecipientFactory();
                        recipients.Add(rf.factory((int)reader.GetValue(0), (string)reader.GetValue(1), (string)reader.GetValue(2)));
                    }
                    Console.WriteLine("Returning");
                    return recipients;
                }
            }
        }

        public void registerRecipient(Recipient recipient)
        {
            recipient.add();
        }

        public void removeRecipient(Recipient recipient)
        {
            recipient.remove();
        }

        public void notifyRecipients(Post post)
        {
            List<Recipient> allRecipients = getRecipients();
            allRecipients.ForEach(recipient =>
            {
                recipient.update(post);
            });
        }

    }
}


