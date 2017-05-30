using MyFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core.DAL
{
    public class MyFinanceDataContext : DbContext
    {
        public MyFinanceDataContext()
            : base("mysavings")
        {
        }

        public DbSet<DistributionPercentage> DistributionPercentages { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<DistributionRule> DistributionRules { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Reserve> Reserves { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Debug.WriteLine("\n----- Validation Errors:");
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("- Class: {0} / Property: {1} / Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
                Debug.WriteLine("\n----- StackTrace: " + dbEx.StackTrace);
                Debug.WriteLine("\n----- End of Validation Errors.");
                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Value)
                .HasPrecision(20, 10);

            modelBuilder.Entity<DistributionPercentage>()
                .Property(t => t.Percentage)
                .HasPrecision(20, 10);
            
            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.Operation)
                .WithMany(t => t.Transactions)
                .WillCascadeOnDelete(true);
        }
    }
}
