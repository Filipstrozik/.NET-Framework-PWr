using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab12.Context;
using Lab12.Models;

namespace Lab12.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly Lab12.Context.MyDbContext _context;

        public DeleteModel(Lab12.Context.MyDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FindAsync(id);

            if (Category != null)
            {
                int articlesConnected = await _context.Articles
                    .Where(a => a.CategoryId == id)
                    .CountAsync();
                if(articlesConnected > 0)
                {
                    ModelState.AddModelError(
                         string.Empty,
                         $"Cannot delete category that has {articlesConnected} articles related. You can only delete article with no articles."
                         );
                    return Page();
                }

                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
