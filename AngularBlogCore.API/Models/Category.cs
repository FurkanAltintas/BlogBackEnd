namespace AngularBlogCore.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }

        public List<Article> Articles { get; set; }
    }
}