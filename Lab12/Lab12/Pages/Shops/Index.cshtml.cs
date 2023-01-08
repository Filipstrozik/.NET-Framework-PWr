using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab12.Context;
using Lab12.Models;

namespace Lab12.Pages.Shops
{
    public class IndexModel : PageModel
    {
        private readonly Lab12.Context.MyDbContext _context;

        public IndexModel(Lab12.Context.MyDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; }


        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public Category Category { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {

            if (Id == null)
            {
                Article = await _context.Articles.Include(a => a.Category).ToListAsync();
                ViewData["Categories"] = await _context.Categories.ToListAsync();
                ViewData["Current"] = null;
                return Page();
            } else
            {
                Category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == Id);

                Article = await _context.Articles
                    .Include(a => a.Category).Where(a => a.CategoryId == Category.Id).ToListAsync();


                ViewData["Categories"] = _context.Categories.ToList();
                ViewData["Current"] = Id;

                return Page();
            }
        }




    }
}
