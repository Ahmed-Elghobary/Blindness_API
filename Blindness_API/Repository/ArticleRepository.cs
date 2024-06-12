using Blindness_API.Data;
using Blindness_API.Models;
using Blindness_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Blindness_API.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _db.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _db.Articles.FindAsync(id);
        }
    }
}
