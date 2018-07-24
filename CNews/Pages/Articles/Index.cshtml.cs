using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CNews.Data;
using CNews.Models;

namespace CNews.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly CNews.Data.ApplicationDbContext _context;

        public IndexModel(CNews.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Designer)
                .Include(a => a.Editor).ToListAsync();
        }
    }
}
