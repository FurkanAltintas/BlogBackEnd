using AngularBlogCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularBlogCore.API.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Id).HasColumnName("id");
            builder.Property(a => a.CategoryId).HasColumnName("category_id");
            builder.Property(a => a.Title).HasColumnName("title");
            builder.Property(a => a.ContentSummary).HasColumnName("content_summary");
            builder.Property(a => a.ContentMain).HasColumnName("content_main");
            builder.Property(a => a.Picture).HasColumnName("picture");
            builder.Property(a => a.ViewCount).HasColumnName("viewCount");
            builder.Property(a => a.PublishDate).HasColumnName("publish_date");
        }
    }
}