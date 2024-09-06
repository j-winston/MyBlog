namespace MyBlog.Models
{
    public class CreatePostModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? CoverImage { get; set; }
        public string? CoverImagePath { get; set; }

    }

}
