using Lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10.Controllers
{
    public class ShopController : Controller
    {

        private readonly MyDbContext _context;

        public ShopController(MyDbContext context)
        {
            _context = context;
        }


        // GET: Articles
        public IActionResult Index()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult IndexList([Bind("Id")] Category myCategory)
        {
            
            var myDbContext = _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == myCategory.Id);
            ViewData["myCategory"] = myCategory.Id;
            return View(myDbContext);
        }
    }
}
