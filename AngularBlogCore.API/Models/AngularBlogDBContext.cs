using AngularBlogCore.API.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AngularBlogCore.API.Models
{
    public class AngularBlogDBContext : DbContext
    {
        public AngularBlogDBContext()
        {

        }

        public AngularBlogDBContext(DbContextOptions<AngularBlogDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Id).HasColumnName("id");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("name");
            modelBuilder.Entity<Category>().Property(c => c.PublishDate).HasColumnName("publish_date");

            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
    }
}
