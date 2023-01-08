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
    public class IndexModel : PageModel
    {
        private readonly Lab12.Context.MyDbContext _context;

        public IndexModel(Lab12.Context.MyDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
