using AngularBlogCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularBlogCore.API.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.ArticleId).HasColumnName("article_id");
            builder.Property(c => c.Name).HasColumnName("name");
            builder.Property(c => c.ContentMain).HasColumnName("content_main");
            builder.Property(c => c.PublishDate).HasColumnName("publish_date");
        }
    }
}