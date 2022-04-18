using FreeShare.Data;
using FreeShare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FreeShare.Views
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Models.Data Data { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // We pass Id as a parameter from Index page view when user clicks Edit
        public async Task OnGet(int id)
        {
            Data = await _db.Data.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Models.Data bookFromDb = await _db.Data.FindAsync(Data.Id);
                bookFromDb.Name = Data.Name;
                bookFromDb.Author = Data.Author;
                bookFromDb.ISBN = Data.ISBN;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}
