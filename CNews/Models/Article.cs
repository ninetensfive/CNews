using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CNews.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }

        public string EditorId { get; set; }
        public User Editor { get; set; }

        public string DesignerId { get; set; }
        public User Designer { get; set; }
    }
}
