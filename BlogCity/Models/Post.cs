namespace BlogCity.Models
{
    public class Post
    {
        public int ID { get; set; }

        public string Title { get; set; }    

        public string Body { get; set; } 

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public Post() { }

        public Post(string title, string body, string author)
        {
            this.Title = title;
            this.Body = body;
            this.Author = author;
        }


        public Post(string title, string body, string author, DateTime date)
        {
            this.Title= title;
            this.Body= body;
            this.Author= author;
            this.Date= date;
        }
    }
}
