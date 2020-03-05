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
    [Route("user")]
    public class UserController : Controller
    {
        private MyContext dbContext;

        public UserController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult DashboardUser()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }

            ViewBag.User = userInDb;            

            return View();
        }

        [HttpGet("edit")]
        public IActionResult EditUser()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            ViewBag.User = userInDb;            
            return View(userInDb);
        }


//////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("edit/update")]
        public IActionResult UpdateUser(User editUser)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }

            User update = dbContext.Users.FirstOrDefault( n => n.UserId == userInDb.UserId);

            if(ModelState.IsValid)
            {
                update.FirstName = editUser.FirstName;
                update.LastName = editUser.LastName;
                update.Email = editUser.Email;
                update.UpdatedAt = editUser.UpdatedAt;
                dbContext.SaveChanges();
                ViewBag.User = userInDb; 
                return Redirect("/user/dashboard");
            }

            ViewBag.User = userInDb;            
            return Redirect("/user/edit");
        }

////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("edit/password")]
        public IActionResult EditUserPassword()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            ViewBag.User = userInDb;            
            return View();
        }

        [HttpPost("edit/password/update")]
        public IActionResult UpdateUserPassword(User editUser)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }

            User toUpdate = dbContext.Users.FirstOrDefault( u => u.UserId == editUser.UserId);
            if(ModelState.IsValid)
            {
                toUpdate.Password = editUser.Password;
                toUpdate.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }

            ViewBag.User = userInDb;            
            return RedirectToAction("EditUser");
        }

////////////////////////////////////////////////////////////////////////

        private User LoggedIn()
        {
            User LogIn = dbContext.Users.FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return LogIn;
        }
    }
}