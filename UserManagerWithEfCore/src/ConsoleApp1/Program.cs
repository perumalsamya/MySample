using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NewMethod();
        }

        private static async Task NewMethod()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer("Data Source =HIBAML5067A\\SQLEXPRESS; Initial Catalog = test; Integrated Security = SSPI;");

            CustomHasher custom = new CustomHasher();

            using (var context = new DbContext(optionsBuilder.Options))
            {
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context, new IdentityErrorDescriber()),
                        new List<IRoleValidator<IdentityRole>>() {
                                    new RoleValidator<IdentityRole>(new IdentityErrorDescriber())
                            },
                        new UpperInvariantLookupNormalizer(),
                        new IdentityErrorDescriber(),
                        new LoggerFactory().CreateLogger<RoleManager<IdentityRole>>(),
                        null);

                IdentityRole role = new IdentityRole("admin");
                var status = roleManager.CreateAsync(role).Result;
                role = new IdentityRole("user-everyone");
                status = roleManager.CreateAsync(role).Result;
                var rols = roleManager.Roles;

                UserManager<User> _userManager = new UserManager<User>(
                    new UserStore<User>(context),
                    null,
                    new CustomHasher(),
                    new List<IUserValidator<User>>() { new UserValidator<User>() },
                    new List<PasswordValidator<User>>() { new PasswordValidator<User>() },
                    null,
                    new IdentityErrorDescriber(),
                    null,
                    new LoggerFactory().CreateLogger<UserManager<User>>()
                    );


                var dbUser = _userManager.FindByNameAsync("perumal21").Result;

                //dbUser.Roles.Add()

                bool isvalid = _userManager.CheckPasswordAsync(dbUser, "Ivendapi@123").Result;

                User user = new User();
                user.UserName = "posuser1";
                user.Email = "perumal62@gmail.com";
                user.Id = Guid.NewGuid().ToString();
                user.Key = 1001;

                var result = _userManager.CreateAsync(user, "Ivendapi@123").Result;

                Console.ReadLine();

            }
        }
    }


    public class CustomHasher : IPasswordHasher<User>
    {
        public string Hash(string input)
        {
            var sha1 = SHA256.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = sha1.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public string HashPassword(User user, string password)
        {
            return Hash(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            providedPassword = Hash(providedPassword);

            if (hashedPassword.Equals(providedPassword))
                return PasswordVerificationResult.Success;
            else return PasswordVerificationResult.Failed;
        }
    }
}
