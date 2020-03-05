using ptPKT.Core.Entities.BL;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Events
{
    public class ArticlePostedEvent : BaseDomainEvent
    {
        public Article PostedArticle { get; set; }

        public ArticlePostedEvent(Article article)
        {
            PostedArticle = article;
        }
    }
}
