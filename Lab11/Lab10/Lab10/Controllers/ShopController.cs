using Lab10.Models;
using Lab10.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10.Controllers
{
    public class ShopController : Controller
    {

        private readonly MyDbContext _context;
        private Dictionary<int, CartItemViewModel> _cart;

        private const int EXPIRATION = 7;
        public ShopController(MyDbContext context)
        {
            _context = context;
        }


        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.Articles.Include(a => a.Category).ToListAsync();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Current = null;
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(appDbContext);
        }

        public async Task<IActionResult> IndexList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articles =  await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == id)
                .ToListAsync();
            ViewBag.Current = id;
            ViewBag.Categories = _context.Categories.ToList();

            return View("Index", articles);
        }

        public Dictionary<int, CartItemViewModel> GetCart()
        {
            string cartString;
            Request.Cookies.TryGetValue("cart", out cartString);
            if(cartString != null)
            {
                _cart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(Request.Cookies["cart"]);
            } 
            else
            {
                _cart = new Dictionary<int, CartItemViewModel>();
            }
            return _cart;
        }

        public void SaveCart(Dictionary<int, CartItemViewModel> cart)
        {
            string cartToString = JsonConvert.SerializeObject(cart);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(EXPIRATION);
            Response.Cookies.Append("cart", cartToString, options);
        }

        private bool CartItemExists(int id)
        {
            _cart = GetCart();
            if (_cart == null) return false;
            return _cart.ContainsKey(id);
        }

        public async Task<IActionResult> Cart()
        {
            _cart = GetCart();
            ViewBag.Total = GetTotal();
            List<CartItemViewModel> basketList = new List<CartItemViewModel>();

            var keys = _cart.Keys.ToList();
            var articles = await _context.Articles.Include(a => a.Category).Where(a => keys.Contains(a.Id)).ToListAsync();

            foreach (var article in articles)
            {
                basketList.Add(new CartItemViewModel
                {
                    ArticleId = article.Id,
                    Article = article,
                    Quantity = _cart[article.Id].Quantity
                });
            }

            return View(basketList);
        }


        public decimal GetTotal()
        {
            _cart = GetCart();
            decimal? total = decimal.Zero;
            if (_cart == null)
            {
                return decimal.Zero;
            }
            foreach (KeyValuePair<int, CartItemViewModel> item in _cart)
            {
                total += (decimal?)item.Value.Article.Price * item.Value.Quantity;
            }
            return total ?? decimal.Zero;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            _cart = GetCart();
            if (CartItemExists(id))
            {
                _cart[id].Quantity++;
            }
            else
            {
                CartItemViewModel cartItemVM = new CartItemViewModel
                {
                    ArticleId = id,
                    Article = _context.Articles.SingleOrDefault(
                        a => a.Id == id),
                    Quantity = 1
                };
                _cart.Add(id, cartItemVM);
            }
            SaveCart(_cart);
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReduceCartItem(int id)
        {
            _cart = GetCart();

            if (CartItemExists(id))
            {
                if (_cart[id].Quantity <= 1) _cart.Remove(id);
                else _cart[id].Quantity--;
            }

            SaveCart(_cart);
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCartItem(int id)
        {
            _cart = GetCart();

            if (CartItemExists(id))
            {
                _cart.Remove(id);
                SaveCart(_cart);
            }

            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

    }
}
