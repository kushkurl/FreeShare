using FreeShare.Data;
using FreeShare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeShare.Areas.Identity.Pages
{
    public class DashBoardModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        // Binding means, on post we get the property that is here
        [BindProperty]
        public List<Models.Data> Data { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<CategoryType> CategoryType { get; set; }
        public DashBoardModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Category = _db.Category.ToList();
            Data = _db.Data.ToList();

            CategoryType = _db.CategoryType.ToList();
            // We don't have to pass the empty Book object, it does that automatically
            // We will be able to access this Book inside the Create view
        }
        [HttpGet]
        public async Task<IActionResult> OnPost()
        {
            
            // ModelState.IsValid checks required areas in the model etc.
            if (ModelState.IsValid)
            {
                Data = _db.Data.Where(c => c.Author == User.Identity.Name).ToList().ToList();

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

    }
}
