using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CNews.Data;
using CNews.Models;

namespace CNews.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly CNews.Data.ApplicationDbContext _context;

        public CreateModel(CNews.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AuthorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
        ViewData["DesignerId"] = new SelectList(_context.Set<User>(), "Id", "Id");
        ViewData["EditorId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}