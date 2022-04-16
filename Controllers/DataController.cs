using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeShare.Data;
using FreeShare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreeShare.Controllers
{
    public class DataController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DataController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(Json(new { data =  _db.Book.ToListAsync() }));
        }

        [HttpDelete]
        public async Task<IActionResult> Index(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);

            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Delete successful" });
        }
    }
}