﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace MyFinance.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("mysavings")
        {
        }
    }
}