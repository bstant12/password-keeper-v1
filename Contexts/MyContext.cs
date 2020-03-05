using Microsoft.EntityFrameworkCore;
using PasswordKeeper.Models;

namespace PasswordKeeper.Contexts
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users {get;set;}

        public DbSet<Note> Notes {get;set;}

        public DbSet<Contact> Contacts {get;set;}

        public DbSet<Password> Passwords {get;set;}

        public DbSet<CreditCard> CreditCards {get;set;}

        public DbSet<PersonalInformation> PersonalInformations {get;set;}
    }
}