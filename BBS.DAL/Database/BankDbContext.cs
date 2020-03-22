using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BBS.DAL.Database
{
    public class BankDbContext : IdentityDbContext<User, IdentityRole<int>,  int>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DepositType> DepositTypes { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<InterestPaymentType> InterestPaymentTypes { get; set; }
        public DbSet<DepositTerm> DepositTerms { get; set; }
        public DbSet<AccountProperty> AccountProperties { get; set; }
        public DbSet<CardRequest> CardRequests { get; set; }
        public DbSet<CardRequestHistory> CardRequestHistory { get; set; }

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(BankDbContext).Assembly);
        }
    }
}