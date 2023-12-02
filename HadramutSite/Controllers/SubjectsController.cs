using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HadramutSite.Data;
using HadramutSite.Models;

namespace HadramutSite.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly MyDbContext _context;

        public SubjectsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {

            if (UserInfo.username != null)
            {
                return _context.Subjects != null ?
                                        View(await _context.Subjects.ToListAsync()) :
                                        Problem("Entity set 'MyDbContext.Subjects'  is null.");
            }
            return Content("Error");

        }

    
        // GET: Subjects/Create
        public IActionResult Create()
        {
            if (UserInfo.isAdmin == true)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Subject subject)
        {
            if (UserInfo.isAdmin == true)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(subject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(subject);
            }
            return RedirectToAction("Index");

        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (UserInfo.isAdmin == true)
            {
                if (id == null || _context.Subjects == null)
                {
                    return NotFound();
                }

                var subject = await _context.Subjects.FindAsync(id);
                if (subject == null)
                {
                    return NotFound();
                }
                return View(subject);
            }
            return RedirectToAction("Index");

        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Subject subject)
        {
            if (UserInfo.isAdmin == true)
            {
                if (id != subject.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(subject);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SubjectExists(subject.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(subject);
            }
            return RedirectToAction("Index");


        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subjects == null)
            {
                return Problem("Entity set 'MyDbContext.Subjects'  is null.");
            }
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return (_context.Subjects?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
