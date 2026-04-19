using AkinBook.Domain.Entities;
using AkinBook.Infrastructure.Auth;
using AkinBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Infrastructure.Seed
{
    public static class AppSeed
    {
        public static async Task SeedAsync(AppDbContext db, IConfiguration config, IPasswordHasher hasher)
        {
            var adminEmail = config["Seed:AdminEmail"];
            var adminPassword = config["Seed:AdminPassword"];
            var createDemoBooks = bool.TryParse(config["Seed:CreateDemoBooks"], out var v) && v;

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
                return;

            adminEmail = adminEmail.Trim().ToLowerInvariant();

            var admin = await db.Users.SingleOrDefaultAsync(x => x.Email == adminEmail);

            if (admin is null)
            {
                admin = new User
                {
                    Id = Guid.NewGuid(),
                    Email = adminEmail,
                    Role = "Admin"
                };

                admin.PasswordHash = hasher.Hash(adminPassword);

                db.Users.Add(admin);
                await db.SaveChangesAsync();
            }
            else
            {
                admin.Role = "Admin";
                admin.PasswordHash = hasher.Hash(adminPassword);
                await db.SaveChangesAsync();
            }

            if (createDemoBooks)
            {
                var anyBook = await db.Books.AnyAsync();
                if (!anyBook)
                {
                    db.Books.AddRange(new[]
                    {
                        new Book { 
                            Id = Guid.NewGuid(),
                            TitleTr = "Suç ve Ceza",
                            TitleEn = "Crime and Punishment",
                            Author = "Dostoyevski",
                            DescriptionTr = "Klasik roman",
                            DescriptionEn = "Classic novel",
                            PublishedYear = 1866,
                            UserId = admin.Id,
                            CreatedAt = DateTime.UtcNow 
                        },
                        new Book { 
                            Id = Guid.NewGuid(),
                            TitleTr = "Sefiller",
                            TitleEn = "Les Misérables",
                            Author = "Victor Hugo",
                            DescriptionTr = "Toplumsal adalet ve insanlık üzerine klasik roman.",
                            DescriptionEn = "A classic novel about justice and humanity.",
                            PublishedYear = 1862,
                            UserId = admin.Id,
                            CreatedAt = DateTime.UtcNow 
                        },
                        new Book {
                            Id = Guid.NewGuid(),
                            TitleTr = "1984",
                            TitleEn = "1984",
                            Author = "George Orwell",
                            DescriptionTr = "Distopik bir romandır.",
                            DescriptionEn = "A dystopian novel.",
                            PublishedYear = 1949,
                            UserId = admin.Id,
                            CreatedAt = DateTime.UtcNow 
                        }
                    });

                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
