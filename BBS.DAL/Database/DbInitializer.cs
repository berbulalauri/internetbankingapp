using BBS.Models.Constants;
using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.DAL.Database
{
    public static class DbInitializer
    {
        public static void SeedData(this BankDbContext context)
        {
            if (!context.Questions.Any())
            {
                context.Questions.AddRange(
                    new Question() { Content = "What was the house number and street name you lived in as a child?" },
                    new Question() { Content = "What are the last five digits of your driver's licence number?" },
                    new Question() { Content = "What is your spouse or partner's mother's maiden name?" });
                
                context.SaveChanges();
            }

            if (!context.Cities.Any())
            {
                context.Cities.AddRange(
                    new City() { Name = "Tbilisi" },
                    new City() { Name = "Gori" },
                    new City() { Name = "Rustavi" },
                    new City() { Name = "Zugdidi" },
                    new City() { Name = "Kutaisi" },
                    new City() { Name = "Batumi" },
                    new City() { Name = "Poti" },
                    new City() { Name = "Telavi" },
                    new City() { Name = "Oni" },
                    new City() { Name = "Khashuri" });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(GenerateUsers());
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole<int>() { Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole<int>() { Name = "User", NormalizedName = "USER" });
                context.SaveChanges();
            }

            if (!context.UserRoles.Any())
            {
                context.UserRoles.Add(new IdentityUserRole<int> {
                    UserId = context.Users.FirstOrDefault(x => x.UserName == "Admin").Id,
                    RoleId = 1 });
                context.SaveChanges();
            }

            if (!context.Currencies.Any())
            {
                context.Currencies.AddRange(
                    new Currency() { Name = "GEL", Rate = 1 },
                    new Currency() { Name = "USD", Rate = 2.55M },
                    new Currency() { Name = "EUR", Rate = 3.01M }
                );
                context.SaveChanges();
            }

            if (!context.AccountProperties.Any())
            {
                context.AccountProperties.Add(new AccountProperty() { MajorIndustryIdentifier = "5", BankIdentificationNumber = "77777" });
                context.SaveChanges();
            }

            if (!context.Accounts.Any())
            {
                context.Accounts.AddRange(
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = new DateTime(2020, 1, 20),
                        Name = "Bank Account",
                        Status = "Active",
                        Number = "GEO_AB78623457824678563487",
                        Balance = 1000000,
                    }, new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = new DateTime(2020, 1, 20),
                        Name = "Silk TV",
                        Status = "Active",
                        Number = "GEO_40702810710040100674",
                        Balance = 1000000,
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = new DateTime(2020, 1, 20),
                        Name = "Magti TV ",
                        Status = "Active",
                        Number = "GEO_40702810623040100682",
                        Balance = 1000000,
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = new DateTime(2020, 1, 20),
                        Name = "Global TV ",
                        Status = "Active",
                        Number = "GEO_40702998620970121618",
                        Balance = 1000000,
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = new DateTime(2020, 1, 20),
                        Name = "Marietta ticket",
                        Status = "Active",
                        Number = "GEO_13332810710040100333",
                        Balance = 1000000,
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = new DateTime(2020, 1, 20),
                        Name = "Georgia Online ticket",
                        Status = "Active",
                        Number = "GEO_13332810923040100233",
                        Balance = 1000000,
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = DateTime.Now,
                        Name = "Magticom internet",
                        Status = "Active",
                        Number = "12402810710140100876",
                        Balance = 0,
                        IsDeleted = false
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = DateTime.Now,
                        Name = "Maxnet internet",
                        Status = "Active",
                        Number = "12402810613040100444",
                        Balance = 0,
                        IsDeleted = false
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 1,
                        RegDate = DateTime.Now,
                        Name = "Silknet internet",
                        Status = "Active",
                        Number = "12402810613040107777",
                        Balance = 0,
                        IsDeleted = false
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 2,
                        RegDate = DateTime.Now,
                        Name = "elie saab",
                        Status = "Active",
                        Number = "12402810613040101111",
                        Balance = 50000,
                        IsDeleted = false
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 3,
                        RegDate = DateTime.Now,
                        Name = "dolce gabana",
                        Status = "Active",
                        Number = "12402810613040102222",
                        Balance = 50000,
                        IsDeleted = false
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 4,
                        RegDate = DateTime.Now,
                        Name = "yurgen gagarin",
                        Status = "Active",
                        Number = "12402810613040103333",
                        Balance = 50000,
                        IsDeleted = false
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 5,
                        RegDate = DateTime.Now,
                        Name = "Romeo Montague",
                        Status = "Active",
                        Number = "12402810613040104444",
                        Balance = 50000,
                        IsDeleted = false
                    },
                    new Account()
                    {
                        CurrencyId = 1,
                        UserId = 6,
                        RegDate = DateTime.Now,
                        Name = "Juliet Capulet",
                        Status = "Active",
                        Number = "12402810613040105555",
                        Balance = 50000,
                        IsDeleted = false
                    },
                    new Account
                    {
                        Balance = 200,
                        CurrencyId = 1,
                        Name = BankStringConstants.PhoneRefillAcc,
                        Number = "GE001TBCGTI00005342675",
                        RegDate = new DateTime(2009, 1, 9),
                        Status = "Active",
                        UserId = 1
                    });
                context.SaveChanges();
            }

            if (!context.Cards.Any())
            {
                context.Cards.AddRange(
                    new Card
                    {
                        AccountId = 1,
                        Cvc2 = 123,
                        UserId = 1,
                        ExpDate = new DateTime(2022, 2, 22),
                        IsDefault = true,
                        Name = "Test Card",
                        Number = "1234567891234567",
                        RegDate = new DateTime(2020, 1, 26),
                        Satus = true,
                        Status = "Active"
                    },
                    new Card
                    {
                        AccountId = 10,
                        Cvc2 = 123,
                        UserId = 2,
                        ExpDate = new DateTime(2022, 2, 22),
                        IsDefault = true,
                        Name = "elie saab",
                        Number = "1234567891231111",
                        RegDate = new DateTime(2020, 1, 26),
                        Satus = true,
                        Status = "Active",
                    },
                    new Card
                    {
                        AccountId = 11,
                        Cvc2 = 123,
                        UserId = 3,
                        ExpDate = new DateTime(2022, 2, 22),
                        IsDefault = true,
                        Name = "dolce gabana",
                        Number = "1234567891232222",
                        RegDate = new DateTime(2020, 1, 26),
                        Satus = true,
                        Status = "Active"
                    },
                    new Card
                    {
                        AccountId = 12,
                        Cvc2 = 123,
                        UserId = 4,
                        ExpDate = new DateTime(2022, 2, 22),
                        IsDefault = true,
                        Name = "yuri gagarin",
                        Number = "1234567891233333",
                        RegDate = new DateTime(2020, 1, 26),
                        Satus = true,
                        Status = "Active"
                    },
                    new Card
                    {
                        AccountId = 13,
                        Cvc2 = 123,
                        UserId = 5,
                        ExpDate = new DateTime(2022, 2, 22),
                        IsDefault = true,
                        Name = "Romeo Montague",
                        Number = "1234567891234444",
                        RegDate = new DateTime(2020, 1, 26),
                        Satus = true,
                        Status = "Active"
                    },
                    new Card
                    {
                        AccountId = 14,
                        Cvc2 = 123,
                        UserId = 6,
                        ExpDate = new DateTime(2022, 2, 22),
                        IsDefault = true,
                        Name = "Juliet Capadela",
                        Number = "1234567891235555",
                        RegDate = new DateTime(2020, 1, 26),
                        Satus = true,
                        Status = "Active"
                    });
                context.SaveChanges();
            }
            
            if (!context.EmploymentTypes.Any())
            {
                context.EmploymentTypes.AddRange(
                    new EmploymentType() { Name = "Officially" },
                    new EmploymentType() { Name = "Not Officially" },
                    new EmploymentType() { Name = "Maternity leave" },
                    new EmploymentType() { Name = "Pensioner" },
                    new EmploymentType() { Name = "Individual entrepreneur" });
                context.SaveChanges();
            }

            if (!context.ServiceCategories.Any())
            {
                context.ServiceCategories.AddRange(
                    new ServiceCategory() { Name = "Internet" },
                    new ServiceCategory() { Name = "Television" },
                    new ServiceCategory() { Name = "ElectronicTicket" },
                    new ServiceCategory() { Name = "Transfer" },
                    new ServiceCategory() { Name = "Deposit" },
                    new ServiceCategory() { Name = "Mobile and Internet" },
                    new ServiceCategory() { Name = "General Banking" });
                context.SaveChanges();
            }

            if (!context.Services.Any())
            {
                context.Services.AddRange(
                    new Service() { Name = "Magti TV", Description = "MagtiCom tv payment", FixedFee = 1, CategoryId = 2, AccountId = 3 },
                    new Service() { Name = "Silk TV", Description = "Silk TV  payment", FixedFee = 1, CategoryId = 2, AccountId = 2 },
                    new Service() { Name = "Global TV", Description = "Global TV payment", FixedFee = 1, CategoryId = 2, AccountId = 4 },
                    new Service() { Name = "Marietta ticket", Description = "Marietta ticket payment", FixedFee = 0, CategoryId = 3, AccountId = 5 },
                    new Service() { Name = "Georgia Online ticket", Description = "Georgia Online ticket payment", FixedFee = 0, CategoryId = 3, AccountId = 6 },

                    new Service()
                    {
                        Name = "Magticom internet",
                        Description = "Magticom internet payment",
                        FixedFee = 1,
                        CategoryId = 1,
                        AccountId = 7,
                        PercentFee = 1
                    },
                    new Service()
                    {
                        Name = "Maxnet internet",
                        Description = "Maxnet internet payment",
                        FixedFee = 1,
                        CategoryId = 1,
                        AccountId = 8,
                        PercentFee = 1
                    },
                    new Service()
                    {
                        Name = "Silknet internet",
                        Description = "Silknet internet payment",
                        FixedFee = 1,
                        CategoryId = 1,
                        AccountId = 9,
                        PercentFee = 1
                    },
                    new Service() { Name = "General transfer", Description = "General transfer service", FixedFee = 0, PercentFee = 5, CategoryId = 4, AccountId = 1 },
                    new Service() { Name = "Deposit", Description = "Add Deposit", FixedFee = 0, CategoryId = 5, AccountId = 1 },
                    new Service() { Name = BankStringConstants.PhoneRefilServiceName, CategoryId = 5, FixedFee = 0.50M, AccountId = 15, PercentFee = 0M },
                    new Service() { Name = "Bank Loan", Description = "Pay Bank Loan", FixedFee = 0m, AccountId = 10, CategoryId = 1, PercentFee = 0M });
                context.SaveChanges();
            }

            if (!context.InterestPaymentTypes.Any())
            {
                context.InterestPaymentTypes.AddRange(
                    new InterestPaymentType() { Name = "Monthly" },
                    new InterestPaymentType() { Name = "At the end of deposit" });
                context.SaveChanges();
            }

            if (!context.DepositTerms.Any())
            {
                context.DepositTerms.AddRange(
                    new DepositTerm()
                    {
                        Name = "Termless"
                    },
                    new DepositTerm()
                    {
                        Name = "9 month"
                    },
                    new DepositTerm()
                    {
                        Name = "12 month"
                    },
                    new DepositTerm()
                    {
                        Name = "18 month"
                    });
                context.SaveChanges();
            }

            if (!context.DepositTypes.Any())
            {
                context.DepositTypes.Add(
                    new DepositType()
                    {
                        Name = "GuaranteedCapital",
                        Description = "your capital'll be guaranteed",
                        BenefitsDescription = "will increase",
                        AnnualRate = 2,
                        BonusRate = 2,
                        InterestPaymentId = 1,
                        MinimumDepositAmount = 1,
                        MinimumReplenishmentAmount = 1,
                        MaximumDepositAmount = 200000,
                        DepositTermId = 1,
                        CurrencyId = 1,
                        IsDeleted = false
                    });
                context.SaveChanges();
            }

            if (!context.Deposits.Any())
            {
                context.Deposits.AddRange(
                    new Deposit
                    {
                        DepositTypeId = 1,
                        DepositAmount = 25355,
                        TermOfDeposit = DateTime.Now.AddYears(6),
                        InterestPayment = DateTime.Now.AddMonths(4),
                        CurrencyId = 1,
                        UserId = 2,
                        AccountToTransferId = 12,
                        AccountForInterestId = 1,
                        Status = "active",
                        DepositTermId = 4
                    },
                    new Deposit
                    {
                        DepositTypeId = 1,
                        DepositAmount = 8000,
                        TermOfDeposit = DateTime.Now.AddYears(6),
                        InterestPayment = DateTime.Now.AddYears(3),
                        CurrencyId = 1,
                        UserId = 4,
                        AccountToTransferId = 10,
                        AccountForInterestId = 1,
                        Status = "active",
                        DepositTermId = 3
                    },
                    new Deposit
                    {
                        DepositTypeId = 1,
                        DepositAmount = 6000,
                        TermOfDeposit = DateTime.Now.AddYears(6),
                        InterestPayment = DateTime.Now.AddYears(3),
                        CurrencyId = 1,
                        UserId = 3,
                        AccountToTransferId = 11,
                        AccountForInterestId = 1,
                        Status = "active",
                        DepositTermId = 1
                    },
                    new Deposit
                    {
                        DepositTypeId = 1,
                        DepositAmount = 7000,
                        TermOfDeposit = DateTime.Now.AddYears(6),
                        InterestPayment = DateTime.Now.AddYears(3),
                        CurrencyId = 1,
                        UserId = 2,
                        AccountToTransferId = 12,
                        AccountForInterestId = 1,
                        Status = "active",
                        DepositTermId = 3
                    },
                    new Deposit
                    {
                        DepositTypeId = 1,
                        DepositAmount = 3000,
                        TermOfDeposit = DateTime.Now.AddYears(6),
                        InterestPayment = DateTime.Now.AddYears(3),
                        CurrencyId = 1,
                        UserId = 6,
                        AccountToTransferId = 13,
                        AccountForInterestId = 1,
                        Status = "active",
                        DepositTermId = 2
                    },
                    new Deposit
                    {
                        DepositTypeId = 1,
                        DepositAmount = 4000,
                        TermOfDeposit = DateTime.Now.AddYears(6),
                        InterestPayment = DateTime.Now.AddYears(4),
                        CurrencyId = 1,
                        UserId = 5,
                        AccountToTransferId = 14,
                        AccountForInterestId = 1,
                        Status = "active",
                        DepositTermId = 4
                    },
                    new Deposit
                    {
                        DepositTypeId = 1,
                        DepositAmount = 12000,
                        TermOfDeposit = DateTime.Now.AddYears(6),
                        InterestPayment = DateTime.Now.AddYears(4),
                        CurrencyId = 1,
                        UserId = 4,
                        AccountToTransferId = 10,
                        AccountForInterestId = 1,
                        Status = "active",
                        DepositTermId = 4
                    },
                    new Deposit
                    {
                        DepositTypeId = 1,
                        DepositAmount = 14500,
                        TermOfDeposit = DateTime.Now.AddYears(6),
                        InterestPayment = DateTime.Now.AddYears(4),
                        CurrencyId = 1,
                        UserId = 3,
                        AccountToTransferId = 11,
                        AccountForInterestId = 1,
                        Status = "active",
                        DepositTermId = 4
                    });
                context.SaveChanges();
            }

            if (!context.LoanTypes.Any())
            {
                context.LoanTypes.Add(
                    new LoanType
                    {
                        Name = "Loan For Real estate",
                        Description = "taken by ppl who buys house",
                        GeneralInformation = "buy house",
                        MinLoanSum = 20000,
                        MaxLoanSum = 500000,
                        InterestRate = 17,
                        LoanSummary = "buy house",
                        MonthlyFee = 3,
                        MinTerm = 1,
                        MaxTerm = 36,
                        PaymentForAccidentInsurance = 1,
                        FeeForInsuranceLoan = 1,
                        FreeForServicesUponReciptCash = 1,
                        CurrencyId = 1,
                        IsDeleted = false
                    });
                context.SaveChanges();
            }

            if (!context.Employments.Any())
            {
                context.Employments.Add(
                    new Employment
                    {
                        EmploymentTypeId = 1,
                        DateOfEmployment = DateTime.Now,
                        OfficePhoneNumber = "555515151"
                    });
                context.SaveChanges();
            }

            if (!context.LoanRequests.Any())
            {
                context.LoanRequests.Add(
                    new LoanRequest()
                    {
                        EmployementId = 1,
                        LoanSum = 1,
                        LoanTypeId = 1,
                        Term = 1,
                        AccrueAccountId = 1,
                        TransferAccountId = 1,
                        UserId = 1,
                        Comments = "gonna buy house",
                        DateOfRequest = DateTime.Now.AddDays(4),
                        IsAccepted = true,
                        IsDenied = false,
                        IsDeleted = false
                    });
                context.SaveChanges();
            }

            context.SaveChanges();

        }

        static List<User> GenerateUsers()
        {
            var result = new List<User>();

            User admin = new User()
            {
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@tbcgti.ge",
                NormalizedEmail = "ADMIN@TBCGTI.GE",
                FirstName = "Admin",
                LatsName = "",
                PhoneNumber = "",
                SecurityStamp = "NTG7UTRWAUZFYBJJHQNAUSSMYSQXJ42W",
                Address = "Politkovskaia street", // Add all data to avoid NullReferenceExceptions
                Status = "Active",
                QuestionId = 1,
                Answer = "13",
                CityId = 1,
                RegDate = new DateTime(2020, 1, 20),
                PasswordHash = "admin"
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser<int>>().HashPassword(admin, admin.PasswordHash);

            User newUser1 = new User()
            {
                UserName = "eliesaab",
                NormalizedUserName = "eliesaab",
                Email = "eliesaab@gmail.com",
                NormalizedEmail = "eliesaab@GMAIL.COM",
                FirstName = "elie",
                LatsName = "saab",
                PhoneNumber = "591 97 12 02",
                PassportId = "01020325658",
                Address = "Politkovskaia street", // Add all data to avoid NullReferenceExceptions
                Status = "Active",
                QuestionId = 1,
                Answer = "13",
                CityId = 1,
                RegDate = new DateTime(2020, 1, 27),
                PasswordHash = "1t1sMyUs&r"
            };

            newUser1.PasswordHash = new PasswordHasher<IdentityUser<int>>().HashPassword(newUser1, newUser1.PasswordHash);

            User newUser2 = new User()
            {
                UserName = "dolcegabana",
                NormalizedUserName = "dolcegabana",
                Email = "dolcegabana@gmail.com",
                NormalizedEmail = "dolcegabana@GMAIL.COM",
                FirstName = "dolce",
                LatsName = "gabana",
                PhoneNumber = "591 97 12 02",
                PassportId = "01020325658",
                Address = "Politkovskaia street", // Add all data to avoid NullReferenceExceptions
                Status = "Active",
                QuestionId = 1,
                Answer = "13",
                CityId = 1,
                RegDate = new DateTime(2020, 1, 27),
                PasswordHash = "1t1sMyUs&r"
            };

            newUser2.PasswordHash = new PasswordHasher<IdentityUser<int>>().HashPassword(newUser2, newUser2.PasswordHash);

            User newUser3 = new User()
            {
                UserName = "yurigagarin",
                NormalizedUserName = "yurigagarin",
                Email = "yurigagarin@gmail.com",
                NormalizedEmail = "yurigagarin@GMAIL.COM",
                FirstName = "yuri",
                LatsName = "gagarin",
                PhoneNumber = "591 97 12 02",
                PassportId = "01020325658",
                Address = "Politkovskaia street", // Add all data to avoid NullReferenceExceptions
                Status = "Active",
                QuestionId = 1,
                Answer = "13",
                CityId = 1,
                RegDate = new DateTime(2020, 1, 27),
                PasswordHash = "1t1sMyUs&r"
            };

            newUser3.PasswordHash = new PasswordHasher<IdentityUser<int>>().HashPassword(newUser3, newUser3.PasswordHash);

            User newUser4 = new User()
            {
                UserName = "RomeoMontague",
                NormalizedUserName = "RomeoMontague",
                Email = "RomeoMontague@gmail.com",
                NormalizedEmail = "RomeoMontague@GMAIL.COM",
                FirstName = "Romeo",
                LatsName = "Montague",
                PhoneNumber = "591 97 12 02",
                PassportId = "01020325658",
                Address = "Politkovskaia street", // Add all data to avoid NullReferenceExceptions
                Status = "Active",
                QuestionId = 1,
                Answer = "13",
                CityId = 1,
                RegDate = new DateTime(2020, 1, 27),
                PasswordHash = "1t1sMyUs&r"
            };

            newUser4.PasswordHash = new PasswordHasher<IdentityUser<int>>().HashPassword(newUser4, newUser4.PasswordHash);

            User newUser5 = new User()
            {
                UserName = "JulietCapulet",
                NormalizedUserName = "JulietCapulet",
                Email = "JulietCapulet@gmail.com",
                NormalizedEmail = "JulietCapulet@GMAIL.COM",
                FirstName = "Juliet",
                LatsName = "Capulet",
                PhoneNumber = "591 97 12 02",
                PassportId = "01020325658",
                Address = "Politkovskaia street", // Add all data to avoid NullReferenceExceptions
                Status = "Active",
                QuestionId = 1,
                Answer = "13",
                CityId = 1,
                RegDate = new DateTime(2020, 1, 27),
                PasswordHash = "1t1sMyUs&r"
            };

            newUser5.PasswordHash = new PasswordHasher<IdentityUser<int>>().HashPassword(newUser5, newUser5.PasswordHash);
            result.AddRange(new List<User> {newUser1, newUser2, newUser3, newUser4, newUser5, admin});

            return result;
        }

    }
}
