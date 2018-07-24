using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CNews.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public int ReporterPoints { get; set; }

        public int EditorPoints { get; set; }

        public int DesignerPoints { get; set; }

        public List<Article> AuthorArticles { get; set; } = new List<Article>();
        public List<Article> EditorArticles { get; set; } = new List<Article>();
        public List<Article> DesignerArticles { get; set; } = new List<Article>();
    }
}
