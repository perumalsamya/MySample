using System;
using Gtk;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrySQLite
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();

            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            //optionsBuilder.UseSqlServer("Data Source =HIBAML5067A\\SQLEXPRESS; Initial Catalog = test; Integrated Security = SSPI;");
            optionsBuilder.UseSqlite(@"Data Source=D:\DB\CXS.DB");

            DbContext context = new DbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

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

            User user = new User();
            user.UserName = "posuser1";
            user.Email = "posuser1@gmail.com";
            user.Id = Guid.NewGuid().ToString();
            user.Key = 1001;

            var result = _userManager.CreateAsync(user, "Ivendapi@123").Result;

            bool isvalid = _userManager.CheckPasswordAsync(user, "Ivendapi@123").Result;

            MainWindow win = new MainWindow();
            win.Show();

            if (isvalid)
            {
                MessageDialog dialog = new MessageDialog(win, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, true, "Is Authentication Proper = {0}", isvalid);
                dialog.Show();
            }
            Application.Run();
        }
    }

    public class DbContext : IdentityDbContext<User>
    {
        public DbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class User : IdentityUser
    {
        public int? Key { get; set; }
    }

    public class CustomHasher : IPasswordHasher<User>
    {
        public string Hash(string input)
        {
            return input.GetHashCode().ToString();
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
