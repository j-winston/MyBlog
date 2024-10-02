using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class CreatePostModel
    {

        [Required(ErrorMessage = "Post body is required.")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Cover image is required.")]
        public IFormFile? CoverImage { get; set; }

        public string? CoverImagePath { get; set; }

    }

}
