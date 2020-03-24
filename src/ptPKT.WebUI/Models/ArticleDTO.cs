using System.ComponentModel.DataAnnotations;
using ptPKT.Core.Entities;
using ptPKT.Core.Entities.BL;

namespace ptPKT.WebUI.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public static ArticleDTO FromArticle(Article item)
        {
            return new ArticleDTO()
            {
                Id = item.Id,
                Title = item.Title,
            };
        }
    }
}