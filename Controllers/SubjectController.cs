using MVCPROJ;
using MVCPROJ.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Assignment_MVC.Controllers
{
     public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubjectController(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<IActionResult> Index(string SearchString) 
        {
            var teachers = from t in _db.Subject select t;
            if(!string.IsNullOrEmpty(SearchString)){
                teachers = teachers.Where(s => s.Subject_Name!.Contains(SearchString));
            }
            return View(await teachers.ToListAsync());
        }

         public IActionResult Create()
        {
            
            return View();
        }


        [HttpPost]
        public IActionResult Create([Bind(" Subject_Name,credits,SubjectID,syllabus,StudentId")] Subject subobj)
        {
            if (ModelState.IsValid)
            {
                _db.Subject.Add(subobj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subobj);
        }



        [HttpGet]
        public IActionResult Edit(int subjectid)
        {
            var subobj = _db.Subject.Find(subjectid);
            return View(subobj);

        }

        [HttpPost]
        public IActionResult Edit(Subject updatedvaluesobj)
        {
            _db.Subject.Update(updatedvaluesobj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int subjectid)
        {
            var subobj = _db.Subject.Find(subjectid);
             if (ModelState.IsValid)
            {
                _db.Subject.Remove(subobj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subobj);
        }

    }
}