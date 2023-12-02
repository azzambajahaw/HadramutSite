using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HadramutSite.Data;
using HadramutSite.Models;
using HadramutSite.ModelFile;
using HadramutSite.Process;

namespace HadramutSite.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment webHost;
        private readonly FilesHelper filesHelper;

        public QuestionsController(MyDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            this.webHost = webHost;
            filesHelper = new FilesHelper(webHost);
        }

        // GET: Questions
        public async Task<IActionResult> Index(int id)
        {
            if (UserInfo.username != null)
            {
                //var myDbContext = _context.Questions.Include(q => q.Subject).Include(q => q.User);
                List<Question>? myDbContext = await _context.Questions.AsNoTracking().Where(x => x.SubjectID == id).ToListAsync();
                ViewBag.SubjectName = _context.Subjects.Find(id)!.Name;
                ViewBag.SubjectId = id;
                return View( myDbContext);
            }
            return Content("Error");

        }



        // GET: Questions/Create
        public IActionResult Create(int subjectId)
        {
            if (UserInfo.username != null)
            {
                //ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "Name");
                //ViewData["UserID"] = new SelectList(_context.Users, "ID", "Password");

                ViewBag.SubjectId = subjectId;

                return View();
            }
            return Content("Error");
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionFile questioFile)
        {
            if (UserInfo.username != null)
            {
                //return Content(questioFile.Content + "\n" + questioFile.Image + "\n" + questioFile.SubjectID + "\n" + questioFile.UserID);
                Question q = new Question();
                ModelState.Remove("Image");
                //return Content(ModelState.ToString());
                if (ModelState.IsValid)
                {
                    q = questioFile;

                    if (questioFile.Image != null)
                    {
                        q.Image = await filesHelper.UploadFile(questioFile.Image, Path.Combine("wwwroot", "images"));
                    }
                    else
                    {
                        q.Image = "default.png";
                    }

                    try
                    {
                        _context.Questions.Add(q);

                    }
                    catch (Exception ex)
                    {
                        return Content(ex.Message);
                    }
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                    return RedirectToAction("Index", new { id = q.SubjectID }); ;
                }
                  return View(questioFile); 
            }

            return Content("Error");
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (UserInfo.username != null)
            {
                if (id == null || _context.Questions == null)
                {
                    return NotFound();
                }

                var question = await _context.Questions.FindAsync(id);
                if (question == null)
                {
                    return NotFound();
                }
                ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "Name", question.SubjectID);
                ViewData["UserID"] = new SelectList(_context.Users, "ID", "Password", question.UserID);
                return View(question);
            }
            else
            {
                return Content("Error");
            }

        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Content,Image,SubjectID,UserID")] Question question)
        {
            if (UserInfo.username != null)
            {
                if (id != question.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(question);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!QuestionExists(question.ID))
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
                ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "Name", question.SubjectID);
                ViewData["UserID"] = new SelectList(_context.Users, "ID", "Password", question.UserID);
                return View(question);
            }
            return Content("Error");

        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (UserInfo.username != null)
            {
                if (id == null || _context.Questions == null)
                {
                    return NotFound();
                }

                var question = await _context.Questions
                    .Include(q => q.Subject)
                    .Include(q => q.User)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (question == null)
                {
                    return NotFound();
                }

                return View(question);
            }
            return Content("Error");

        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (UserInfo.username != null)
            {
                if (_context.Questions == null)
                {
                    return Problem("Entity set 'MyDbContext.Questions'  is null.");
                }
                var question = await _context.Questions.FindAsync(id);
                if (question != null)
                {
                    if (question.Image != null)
                    {
                        filesHelper.DeleteImage(question.Image);
                    }
                    _context.Questions.Remove(question);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Subjects"); ;
            }
            return Content("Error");

        }

        private bool QuestionExists(int id)
        {
            return (_context.Questions?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
