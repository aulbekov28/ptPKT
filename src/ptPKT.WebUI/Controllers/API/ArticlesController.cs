using Microsoft.AspNetCore.Mvc;
using ptPKT.SharedKernel.Interfaces;
using ptPKT.WebUI.Models;
using System.Linq;
using ptPKT.Core.Entities.BL;

namespace ptPKT.WebUI.Controllers
{
    public class ArticlesController : BaseApiController
    {
        private readonly IRepository _repository;

        public ArticlesController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Articles
        [HttpGet]
        public IActionResult List()
        {
            var items = _repository.List<Article>();
            return Ok(items);
        }

        // GET: api/Articles
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = ArticleDTO.FromArticle(_repository.GetById<Article>(id));
            return Ok(item);
        }

        // POST: api/Articles
        [HttpPost]
        public IActionResult Post([FromBody] ArticleDTO article)
        {
            var newArticle = new Article()
            {
                Title = article.Title,
            };
            _repository.Add(newArticle);
            return Ok(ArticleDTO.FromArticle(newArticle));
        }

        [HttpPut]
        public IActionResult Update([FromBody] ArticleDTO article)
        {
            var updatedArticle = new Article();
            _repository.Update(updatedArticle);
            return Ok(ArticleDTO.FromArticle(updatedArticle));
        }
    }
}