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
    [Route("contact")]
    public class ContactController : Controller
    {
        private MyContext dbContext;

        public ContactController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult DashboardContact()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }

            ViewBag.User = userInDb;            
            ViewBag.AllContacts = dbContext.Contacts.Include( n => n.ContactMaker ).Where( n => n.UserId == userInDb.UserId ).OrderBy( n=>n.CFirstName).ToList();
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateContact(Contact newContact)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    
            if(ModelState.IsValid)  
            {
                dbContext.Contacts.Add(newContact);
                dbContext.SaveChanges();
                return Redirect("/contact/dashboard");
            }   
            else
            {
                ViewBag.User = userInDb;
                return View("Dashboard");
            }   
        }

        [HttpPost("{contactId}/update")]
        public IActionResult UpdateContact(Contact updateContact, int contactId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            Contact update = dbContext.Contacts.FirstOrDefault( n => n.ContactId == contactId);
            if(ModelState.IsValid)
            {
                update.CFirstName = updateContact.CFirstName;
                update.CLastName = updateContact.CLastName;
                update.CEmail = updateContact.CEmail;
                update.CPhoneNumber = updateContact.CPhoneNumber;
                update.CRelationship = updateContact.CRelationship;
                update.UpdatedAt = updateContact.UpdatedAt;
                dbContext.SaveChanges();
                ViewBag.User = userInDb;
                return Redirect($"/contact/dashboard");
            }
            ViewBag.User = userInDb;
            return Redirect($"/contact/dashboard");
        }

        [HttpGet("{contactId}/delete")]
        public IActionResult DeleteContact(int contactId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    

            Contact remove = dbContext.Contacts.FirstOrDefault( w => w.ContactId == contactId);
            dbContext.Contacts.Remove(remove);
            dbContext.SaveChanges();
            return RedirectToAction("DashboardContact");
        }

//////////////////////////////////////////////////////////////////////////////////////////////////////

        private User LoggedIn()
        {
            User LogIn = dbContext.Users.Include(u => u.MyContacts).FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return LogIn;
        }
    }
}