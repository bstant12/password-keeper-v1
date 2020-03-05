using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PasswordKeeper.Models;
using PasswordKeeper.Contexts;
using Microsoft.EntityFrameworkCore;


namespace PasswordKeeper.Controllers
{
    [Route("note")]
    public class NoteController : Controller
    {
        private MyContext dbContext;

        public NoteController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult DashboardNote()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            ViewBag.User = userInDb;            
            ViewBag.AllNotes = dbContext.Notes.Include( n => n.NoteTaker ).Where( n => n.UserId == userInDb.UserId ).OrderBy(n=>n.Name).ToList();
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateNote(Note newNote)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    
            if(ModelState.IsValid)  
            {
                newNote.NoteContent = EncryptDecrypt.Encrypt(newNote.NoteContent);
                dbContext.Notes.Add(newNote);
                dbContext.SaveChanges();
                return Redirect($"/note/dashboard");
            }   
            else
            {
                ViewBag.User = userInDb;
                return View("Dashboard");
            }   
        }

        [HttpGet("{noteId}/delete")]
        public IActionResult DeleteNote(int noteId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    
            Note remove = dbContext.Notes.FirstOrDefault( w => w.NoteId == noteId);
            dbContext.Notes.Remove(remove);
            dbContext.SaveChanges();
            return RedirectToAction("DashboardNote");
        }


        [HttpGet("{noteId}/edit")]
        public IActionResult EditNote(int noteId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            Note ThisNote = dbContext.Notes.FirstOrDefault( n => n.NoteId == noteId );
            ThisNote.NoteContent = EncryptDecrypt.Decrypt(ThisNote.NoteContent);
            ViewBag.Edit = dbContext.Notes.FirstOrDefault( n => n.NoteId == noteId);
            ViewBag.User = userInDb;
            return View(ThisNote);
        }

        [HttpPost("{noteId}/update")]
        public IActionResult UpdateNote(Note updateNote, int noteId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            Note update = dbContext.Notes.FirstOrDefault( n => n.NoteId == noteId);
            if(ModelState.IsValid)
            {
                update.Name = updateNote.Name;
                update.NoteContent = EncryptDecrypt.Encrypt(updateNote.NoteContent);
                update.UpdatedAt = updateNote.UpdatedAt;
                if(updateNote.NoteContent == "")
                {
                    ModelState.AddModelError("NoteContent", "You didn't enter anything.");
                    ViewBag.User = userInDb;
                    return Redirect("/note/dashboard");
                }
                dbContext.SaveChanges();
                ViewBag.User = userInDb;
                return Redirect("/note/dashboard");
            }
            ViewBag.User = userInDb;
            return Redirect("/note/dashboard");
        }

///////////////////////////////////////////////////////////////////////////////////////////////////////////

        private User LoggedIn()
        {
            User LogIn = dbContext.Users.Include(u => u.MyNotes).FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return LogIn;
        }
    }
}