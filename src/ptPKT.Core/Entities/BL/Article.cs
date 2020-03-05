using System.Collections.Generic;
using ptPKT.Core.Events;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities.BL
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsPosted { get; set; } 

        public IEnumerable<Tag> Tags { get; set; }

        public void MarkPosted()
        {
            IsPosted = true;
            Events.Add(new ArticlePostedEvent(this));
        }
    }
}
