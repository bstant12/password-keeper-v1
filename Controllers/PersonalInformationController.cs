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
    [Route("info")]
    public class PersonalInformationController : Controller
    {
        private MyContext dbContext;

        public PersonalInformationController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult DashboardPersonalInformation()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }

            ViewBag.User = userInDb;            
            ViewBag.AllInfo = dbContext.PersonalInformations.Include( n => n.InfoOwner ).Where( n => n.UserId == userInDb.UserId ).OrderBy(p =>p.InfoName).ToList();
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreatePersonalInformation(PersonalInformation newPersonalInformation)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    
            if(ModelState.IsValid)  
            {
                newPersonalInformation.InfoContent = EncryptDecrypt.Encrypt(newPersonalInformation.InfoContent);
                dbContext.PersonalInformations.Add(newPersonalInformation);
                dbContext.SaveChanges();
                return Redirect("/info/dashboard");
            }   
            else
            {
                ViewBag.User = userInDb;
                return Redirect("/info/dashboard");
            }   
        }

        [HttpGet("{personalinformationId}/delete")]
        public IActionResult DeletePersonalInformation(int personalinformationId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    

            PersonalInformation remove = dbContext.PersonalInformations.FirstOrDefault( w => w.PersonalInformationId == personalinformationId);
            dbContext.PersonalInformations.Remove(remove);
            dbContext.SaveChanges();
            return RedirectToAction("DashboardPersonalInformation");
        }

        [HttpPost("{personalinformationId}/update")]
        public IActionResult UpdatePersonalInformation(PersonalInformation updatePersonalInformation, int personalinformationId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }

            PersonalInformation update = dbContext.PersonalInformations.FirstOrDefault( n => n.PersonalInformationId == personalinformationId);

            if(ModelState.IsValid)
            {
                update.InfoName = updatePersonalInformation.InfoName;
                update.InfoContent = EncryptDecrypt.Encrypt(updatePersonalInformation.InfoContent);
                update.InfoNote = updatePersonalInformation.InfoNote;

                update.UpdatedAt = updatePersonalInformation.UpdatedAt;
                dbContext.SaveChanges();
                ViewBag.User = userInDb;
                return Redirect("/info/dashboard");
            }

            ViewBag.User = userInDb;
            return Redirect("/info/dashboard");
        }

/////////////////////////////////////////////////////////////////////////////////////
        private User LoggedIn()
        {
            User LogIn = dbContext.Users.Include(u => u.MyPersonalInformations).FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return LogIn;
        }
    }
}