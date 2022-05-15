using AngularBlogCore.API.Models;
using AngularBlogCore.API.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularBlogCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly AngularBlogDBContext _context;

        public ArticlesController(AngularBlogDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticle()
        {
            return await _context.Article.ToListAsync();
        }


        // Metodun ismine göre bir çağırma yapmıyoruz. Metodun tipine göre bir çağırma yapıyoruz
        // Ben eğer buraya bir değer girmezsem default olarak vermiş olduğum değerleri girecek.
        [HttpGet("{page}/{pageSize}")] // Metot tipine göre bir eşleştirme yapıyorsanız süslü parantezler içerisinde belirtmeniz gerekiyor.
        public IActionResult GetArticle(int page = 1, int pageSize = 5)
        {
            try
            {
                IQueryable<Article> query;

                query = _context.Article.Include(a => a.Category).Include(a => a.Comments).OrderByDescending(a => a.PublishDate);

                int totalCount = query.Count(); // Sayfalama yapacağımız için toplam kaç tane makale olduğunu bilmemiz gerekiyor.

                // 5 * (1 - 1) = 0
                // 5 * (2 - 1) = 5 => 5 tane makaleyi atlayıp sonraki 5 makaleyi getirecek.
                var articleResponse = query.Skip((pageSize * (page - 1))).Take(pageSize).ToList().Select(a => new ArticleResponse
                {
                    Id = a.Id,
                    Title = a.Title,
                    ContentMain = a.ContentMain,
                    ContentSummary = a.ContentSummary,
                    Picture = a.Picture,
                    ViewCount = a.ViewCount,
                    CommentCount = a.Comments.Count(),
                    PublishDate = a.PublishDate,
                    Category = new CategoryResponse() { Id = a.Category.Id, Name = a.Category.Name }
                });

                var result = new // İsimsiz bir method oluşturduk
                {
                    TotalCount = totalCount,
                    Articles = articleResponse
                };

                return Ok(result); // 200 Durum Kodu
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // 400 Durum Kodu
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null) return BadRequest();
            return article;
        }

        [HttpPost]
        public async Task<IActionResult> PostArticle(Article article)
        {
            await _context.Article.AddAsync(article);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        [HttpPut]
        public async Task<IActionResult> PutArticle(Article article)
        {
            var data = await _context.Article.FindAsync(article.Id);
            if (data == null) return NotFound();
            await Task.Run(() => { _context.Article.Update(article); });
            return Ok(article.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            var data = await _context.Article.FindAsync(articleId);
            if (data == null) return NotFound();
            await Task.Run(() => { _context.Article.Remove(data); });
            return NoContent();
        }
    }
}