using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CNews.Data;
using CNews.Models;

namespace CNews.Pages.Articles
{
    public class EditModel : PageModel
    {
        private readonly CNews.Data.ApplicationDbContext _context;

        public EditModel(CNews.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
           ViewData["DesignerId"] = new SelectList(_context.Set<User>(), "Id", "Id");
           ViewData["EditorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
