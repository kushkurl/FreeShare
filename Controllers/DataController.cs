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
            /*var stData =  new List<Models.Data>();
           var data = new Models.Data();
            data.Name = "test";
            stData = _db.Data.ToList();*/
            return View(_db.Data.ToList());
        }

        [HttpGet]
        public IActionResult MyDashBoard()
        {
            
            return View(Json(new { data = _db.Data.ToListAsync() }));
        }

        [HttpGet]
        public IActionResult Create(FreeShare.Models.Data data)
        {
            if (data == null)
            {
                return View(Json(new { success = false, message = "Error while Creating" }));
            }

            _db.Data.Add(data);
            _db.SaveChangesAsync();

            return View(Json(new { success = true, message = "Added successful" }));
        }

        [HttpGet]
        public IActionResult Edit(FreeShare.Models.Data data)
        {
            var FromDb = _db.Data.FirstOrDefault(b => b.Id == data.Id);

            if (FromDb == null)
            {
                return View(Json(new { success = false, message = "Error while Editing" }));
            }
            /*FromDb.Name = data.Name;
            FromDb.Author = data.Author;
            FromDb.ISBN = data.ISBN;
            FromDb.CId = data.CId;
            _db.Data.Update(data);
            //_db.Data.Remove(FromDb);
            _db.SaveChangesAsync();*/

            return View(FromDb);
        }
    }
}