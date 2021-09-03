using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDWithIssuesCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWithIssuesCore.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Student> students = await StudentDb.GetStudents(context);
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student p)
        {
            if (ModelState.IsValid)
            {
                p = await StudentDb.Add(p, context);

                TempData["Message"] = $"{p.Name} was added successfully";

                return RedirectToAction("Index");
            }

            //Show web page with errors
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //get the product by id
            Student p = await StudentDb.GetStudent(context, id);

            //show it on web page
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                context.Entry(p).State = EntityState.Modified;
                await context.SaveChangesAsync();

                ViewData["Message"] = "Product updated successfully";
            }
            //return view with errors
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Student p = await StudentDb.GetStudent(context, id);
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            //Get Product from database
            Student p = await StudentDb.GetStudent(context, id);

            context.Entry(p).State = EntityState.Deleted;

            await context.SaveChangesAsync();

            TempData["Message"] = $"{p.Name} was deleted";

            return RedirectToAction("Index");
        }
    }
}