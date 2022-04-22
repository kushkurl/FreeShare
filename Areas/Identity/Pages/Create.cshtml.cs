using FreeShare.Data;
using FreeShare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeShare.Views
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        // Binding means, on post we get the property that is here
        [BindProperty]
        public Models.Data Data { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<CategoryType> CategoryType { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Category = _db.Category.ToList();

            CategoryType = _db.CategoryType.ToList();
            // We don't have to pass the empty Book object, it does that automatically
            // We will be able to access this Book inside the Create view
        }
        [HttpGet]
        public JsonResult GetCategoryListByCategoryId(string ctype)
        {
            var data = _db.Category.Where(x => x.NameToken == ctype).ToList();
            return new JsonResult(data);
        }

        // Task is IActionResult because we are redirecting to a new page
        public async Task<IActionResult> OnPost()
        {
            Data.Author = User.Identity.Name;
            // ModelState.IsValid checks required areas in the model etc.
            if (ModelState.IsValid)
            {
                await _db.Data.AddAsync(Data);
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
