using Microsoft.Extensions.Hosting;
using BlogCity.Models;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace BlogCity.Models
{
    public class EmailRecipient : Recipient
    {

        public int ID;
        public string Type = "Email";
        public string Contact;


        public EmailRecipient(int ID, string Type, string Contact)
        {
            this.ID = ID;
            this.Type = Type;
            this.Contact = Contact;
        }

        public EmailRecipient(string Contact)
        {
            this.Contact = Contact;
        }


        public void update(Post post)
        {
            sendEmail(post);
        }

        public void add()
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Blog;Trusted_Connection=True;Encrypt=False;"))
            {
                sqlConnection.Open();
                string query = "INSERT INTO Recipient (Type, Contact) VALUES(@Type, @Contact)";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@Type", Type);
                command.Parameters.AddWithValue("@Contact", Contact);
                command.ExecuteNonQuery();
            }
        }

        public void remove()
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Blog;Trusted_Connection=True;Encrypt=False;"))
            {
                sqlConnection.Open();
                string query = $"DELETE FROM Recipient WHERE Recipient.ID = {ID}";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();
            }
        }

        public void sendEmail(Post post)
        {
            try
            {
                var fromAddress = new MailAddress("BlogCityAlert@gmail.com", "Blog City");
                var toAddress = new MailAddress("govindaram123@gmail.com");
                const string fromPassword = "whjudqnpqnzhvlgd";
                const string subject = "New Post Added!";
                string body = $"<h1>New BlogCity Post Added</h1><p>Post \"{post.Title}\" by {post.Author}</p><p>Come check it on our blog list!</p>";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })

                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                    Console.WriteLine("Sent Email!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -" + ex);
            }
        }

    }
}
