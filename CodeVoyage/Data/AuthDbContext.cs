using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeVoyage.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //creation and seeding of roles
            var readerRoleId = "92fc6ba9-f92f-47bf-a184-cd191e876e6b";
            var writerRoleId = "767f9104-1bc7-44ee-96a0-a0d43c97d320";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                    ConcurrencyStamp = writerRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //creation and seeding of user
            var adminUserId = "bfd25642-8d71-429c-a1c1-a1d1af1b85f0";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codevoyage.com",
                Email = "admin@codevoyage.com",
                NormalizedUserName = "ADMIN@CODEVOYAGE.COM",
                NormalizedEmail = "ADMIN@CODEVOYAGE.COM"
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "P@$$w0rd");
            builder.Entity<IdentityUser>().HasData(admin);

            //assign user roles
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
