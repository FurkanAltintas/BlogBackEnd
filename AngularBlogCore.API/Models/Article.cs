namespace AngularBlogCore.API.Models
{
    public class Article
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string ContentSummary { get; set; }
        public string ContentMain { get; set; }
        public string Picture { get; set; }
        public int ViewCount { get; set; }
        public DateTime PublishDate { get; set; }

        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }
    }
}