namespace MyBlog.Models
{
    public class User
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<Post>? BlogPosts { get; set; }

    }
}
