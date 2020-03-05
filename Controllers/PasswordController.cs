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
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace PasswordKeeper.Controllers
{
    [Route("password")]
    public class PasswordController : Controller
    {
        private MyContext dbContext;

        public PasswordController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult DashboardPassword()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            string[] allLogos = new string[]
            {
                "instagram",
                "netflix",
                "espn",
                "snapchat",
                "dropbox",
                "reddit",
                "whatsapp",
                "linkedin",
                "google",
                "yahoo",
                "apple",
                "hbo",
                "lyft",
                "bankofamerica",
                "chase",
                "tiktok",
                "hulu",
                "paypal",
                "spotify",
                "twitter",
                "amazon",
                "grubhub"
            };

            ViewBag.Logos = allLogos;
            ViewBag.AllPasswords = dbContext.Passwords.Include( n => n.PasswordMaker ).Where( n => n.UserId == userInDb.UserId ).OrderBy(n=>n.Website).ToList();

            ViewBag.User = userInDb;            
            // List<Password> AllPasswords = dbContext.Passwords.Include( n => n.PasswordMaker ).Where( n => n.UserId == userInDb.UserId ).ToList();
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreatePassword(Password newPassword)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    
            if(ModelState.IsValid)  
            {

                newPassword.Website = newPassword.Website.Trim();
                newPassword.ThePassword = EncryptDecrypt.Encrypt(newPassword.ThePassword);
                dbContext.Passwords.Add(newPassword);
                dbContext.SaveChanges();
                return Redirect($"/password/dashboard");
            }   
            else
            {
                ViewBag.User = userInDb;
                return View("Dashboard");
            }   
        }

        [HttpPost("{passwordId}/update")]
        public IActionResult UpdatePassword(Password updatePassword, int passwordId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }

            Password update = dbContext.Passwords.FirstOrDefault( n => n.PasswordId == passwordId);

            if(ModelState.IsValid)
            {
                update.Website = updatePassword.Website.Trim();
                update.Email = updatePassword.Email;
                update.UserName = updatePassword.UserName;
                update.ThePassword = EncryptDecrypt.Encrypt(updatePassword.ThePassword);
                update.PasswordNotes = updatePassword.PasswordNotes;
                update.UpdatedAt = updatePassword.UpdatedAt;
                dbContext.SaveChanges();
                ViewBag.User = userInDb;
                return Redirect($"/password/dashboard");
            }

            ViewBag.User = userInDb;
            return Redirect($"/password/dashboard");
        }


        [HttpGet("{passwordId}/delete")]
        public IActionResult DeletePassword(int passwordId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    

            Password remove = dbContext.Passwords.FirstOrDefault( w => w.PasswordId == passwordId);
            dbContext.Passwords.Remove(remove);
            dbContext.SaveChanges();
            return RedirectToAction("DashboardPassword");
        }







////////////////////////////////////////////////////////////////////

        private User LoggedIn()
        {
            User LogIn = dbContext.Users.Include(u => u.MyPasswords).FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return LogIn;
        }

        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////

        
    }
}