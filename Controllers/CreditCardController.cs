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
using System.ComponentModel.DataAnnotations;
using PasswordKeeper.Controllers;

namespace PasswordKeeper.Controllers
{
    [Route("creditcard")]
    public class CreditCardController : Controller
    {
        private MyContext dbContext;

        public CreditCardController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult DashboardCreditCard()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }

            ViewBag.AllCards = dbContext.CreditCards.Include( n => n.CardHolder ).Where( n => n.UserId == userInDb.UserId ).ToList();
            ViewBag.User = userInDb;            

            return View();
        }

        // [HttpGet("add")]
        // public IActionResult AddCreditCard()
        // {
        //     User userInDb = LoggedIn();
        //     if(userInDb == null)
        //     {
        //         return RedirectToAction("LogOut", "Home");
        //     }

        //     ViewBag.User = userInDb;            
        //     return View();
        // }

        [HttpPost("create")]
        public IActionResult CreateCreditCard(CreditCard newCreditCard)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            string Visa = "4";
            string MC = "5";
            string AE = "3";
            string Dis = "6";
            if(ModelState.IsValid)  
            {
                if(newCreditCard.CardNumber[0] == Visa[0])
                {
                    newCreditCard.CardType = "Visa";
                }
                else if(newCreditCard.CardNumber[0] == MC[0])
                {
                    newCreditCard.CardType = "Mastercard";
                }
                else if(newCreditCard.CardNumber[0] == AE[0] && newCreditCard.CardNumber.Length == 15)
                {
                    newCreditCard.CardType = "American Express";
                }
                else if(newCreditCard.CardNumber[0] == Dis[0])
                {
                    newCreditCard.CardType = "Discover";
                }
                else
                {
                    ModelState.AddModelError("CardNumber", "Please enter a valid Card Number");
                    return View("AddCreditCard");
                }

                if(newCreditCard.Expiration.Year < DateTime.Now.Year || newCreditCard.Expiration.Month < DateTime.Now.Month){
                    ModelState.AddModelError("Expiration", "Please enter a valid credit card");
                    return View("AddCreditCard");
                }
                

                newCreditCard.CardNumber = EncryptDecrypt.Encrypt(newCreditCard.CardNumber);
                newCreditCard.SecurityCode = EncryptDecrypt.Encrypt(newCreditCard.SecurityCode);
                newCreditCard.UserId = userInDb.UserId;
                dbContext.CreditCards.Add(newCreditCard);
                dbContext.SaveChanges();
                return Redirect($"/creditcard/dashboard");
            }   
            else
            {
                ViewBag.User = userInDb;
                return View("Dashboard");
            }   
        }

        [HttpGet("{creditcardId}")]
        public IActionResult ShowCreditCard(int creditcardId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            CreditCard show = dbContext.CreditCards.Include( n => n.CardHolder ).FirstOrDefault( n => n.CreditCardId == creditcardId );
            
            ViewBag.User = userInDb;
            return View(show);
        }

        [HttpGet("{creditcardId}/edit")]
        public IActionResult EditCreditCard(int creditcardId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }


            CreditCard ThisCreditCard = dbContext.CreditCards.FirstOrDefault( n => n.CreditCardId == creditcardId );

            ViewBag.Edit = dbContext.CreditCards.FirstOrDefault( n => n.CreditCardId == creditcardId);
            ViewBag.User = userInDb;
            return View(ThisCreditCard);
        }

        [HttpPost("{creditcardId}/update")]
        public IActionResult UpdateCreditCard(CreditCard updateCreditCard, int creditcardId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            CreditCard update = dbContext.CreditCards.FirstOrDefault( n => n.CreditCardId == creditcardId);
            string Visa = "4";
            string MC = "5";
            string AE = "6";
            string Dis = "7";
            if(ModelState.IsValid)
            {
                if(updateCreditCard.CardNumber[0] == Visa[0])
                {
                    updateCreditCard.CardType = "Visa";
                }
                else if(updateCreditCard.CardNumber[0] == MC[0])
                {
                    updateCreditCard.CardType = "Mastercard";
                }
                else if(updateCreditCard.CardNumber[0] == AE[0])
                {
                    updateCreditCard.CardType = "American Express";
                }
                else if(updateCreditCard.CardNumber[0] == Dis[0])
                {
                    updateCreditCard.CardType = "Discover";
                }
                else
                {
                    ModelState.AddModelError("CardNumber", "Please enter a valid Card Number");
                    return View("AddCreditCard");
                }
                if(updateCreditCard.Expiration.Year < DateTime.Now.Year || updateCreditCard.Expiration.Month < DateTime.Now.Month){
                    ModelState.AddModelError("Expiration", "Please enter a valid credit card");
                    return View("AddCreditCard");
                }




                update.CardName = updateCreditCard.CardName;
                update.CardNumber = EncryptDecrypt.Encrypt(updateCreditCard.CardNumber);
                update.CardType = updateCreditCard.CardType;
                update.Expiration = updateCreditCard.Expiration;
                update.SecurityCode = EncryptDecrypt.Encrypt(updateCreditCard.SecurityCode);
                update.UpdatedAt = updateCreditCard.UpdatedAt;
                dbContext.SaveChanges();
                ViewBag.User = userInDb;
                return Redirect("/creditcard/dashboard");
            }
            ViewBag.User = userInDb;
            return RedirectToAction("Dashboard");
        }


        [HttpGet("{creditcardId}/delete")]
        public IActionResult DeleteCreditCard(int creditcardId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("LogOut", "Home");
            }    

            CreditCard remove = dbContext.CreditCards.FirstOrDefault( w => w.CreditCardId == creditcardId);
            dbContext.CreditCards.Remove(remove);
            dbContext.SaveChanges();
            return RedirectToAction("DashboardCreditCard");
        }


        ////////////////////////////////////////////////////////////////

        private User LoggedIn()
        {
            User LogIn = dbContext.Users.Include(u => u.MyCreditCards).FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return LogIn;
        }
    }
}