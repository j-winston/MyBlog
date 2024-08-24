namespace MyBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string? Content { get; set; }
        public DateTime? DatePosted { get; set; }
        public User? Author { get; set; }




    }
}

