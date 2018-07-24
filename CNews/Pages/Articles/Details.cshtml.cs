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
    public class DetailsModel : PageModel
    {
        private readonly CNews.Data.ApplicationDbContext _context;

        public DetailsModel(CNews.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Designer)
                .Include(a => a.Editor).FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
