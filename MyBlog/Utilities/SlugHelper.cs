namespace MyBlog.Utilities
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string title)
        {
            // Replace whitespace with hyphen
            string slug = title.Trim().ToLower().Replace(" ", "-");

            return slug;

        }

    }

}


