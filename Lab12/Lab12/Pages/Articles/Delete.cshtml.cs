using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab12.Context;
using Lab12.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Lab12.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly Lab12.Context.MyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DeleteModel(Lab12.Context.MyDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
                .Include(a => a.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
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

            Article = await _context.Articles.FindAsync(id);


            if (Article != null)
            {
                if (Article.filePath != null)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                    string filePath = Path.Combine(uploadFolder, Article.filePath);
                    Article.filePath = null;
                    if (System.IO.File.Exists(filePath))
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi != null)
                        {
                            System.IO.File.Delete(filePath);
                            fi.Delete();
                        }
                    }
                }
                _context.Articles.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
