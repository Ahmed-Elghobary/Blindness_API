using Blindness_API.Models;

namespace Blindness_API.Repository.IRepository
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<Article> GetArticleByIdAsync(int id);
    }
}
