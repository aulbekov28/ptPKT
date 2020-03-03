using ptPKT.SharedKernel;
using System.Collections.Generic;

namespace ptPKT.Core.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsDraft { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
