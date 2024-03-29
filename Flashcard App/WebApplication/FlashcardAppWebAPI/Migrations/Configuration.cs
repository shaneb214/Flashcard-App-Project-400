﻿namespace FlashcardAppWebAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FlashcardAppWebAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FlashcardAppWebAPI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //var user = new ApplicationUser() { UserName = "shaneb", Email = "shaneb214@gmail.com",Id = "e0197dde - 6f60 - 4e2f - 90a5 - fd0bb2644d34" };

            //IdentityResult result = await UserManager.

            //LANGUAGES

            //context.Languages.AddOrUpdate
            //(
            //    new Language() { ISO = "en", Name = "English" },
            //    new Language() { ISO = "ru", Name = "Russian" },
            //    new Language() { ISO = "it", Name = "Italian" },
            //    new Language() { ISO = "ja", Name = "Japanese" }
            //);
            //context.SaveChanges();

            //LANG PROFILE

            //context.LanguageProfiles.AddOrUpdate(new LanguageProfile()
            //{
            //    ID = Guid.NewGuid().ToString(),
            //    userID = "0ae94ef8-ecff-4d6a-a030-f2b573a797fa",
            //    nativeLanguageISO = "en",
            //    learningLanguageISO = "ru",
            //    IsCurrentProfile = true
            //});

            //context.SaveChanges();

            //SETS.

            //context.Sets.AddOrUpdate(new Set()
            //{
            //    ID = Guid.NewGuid().ToString(),
            //    Name = "Animals",
            //    LanguageProfileID = "cc3b0a6b-b418-4c1a-92cd-7b9fec687d51",
            //    IsDefaultSet = true
            //});
            //context.SaveChanges();


            //context.Flashcards.AddOrUpdate(new Flashcard()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    setID = "9187b6b3-2602-4267-b7c4-1532a934aa93",
            //    nativeSide = "Bear",
            //    learningSide = "медведь",
            //    notes = string.Empty
            //});
            //context.SaveChanges();


            //context.Languages.AddOrUpdate(
            //    new Language() { ISO = "cs", Name = "Czech" },
            //    new Language() { ISO = "de", Name = "German" },
            //    new Language() { ISO = "es", Name = "Spanish" },
            //    new Language() { ISO = "fi", Name = "Finnish" },
            //    new Language() { ISO = "da", Name = "Danish" },
            //    new Language() { ISO = "sv", Name = "Swedish" },
            //    new Language() { ISO = "fr", Name = "French" },
            //    new Language() { ISO = "cy", Name = "Welsh" },
            //    new Language() { ISO = "ga", Name = "Irish" },
            //    new Language() { ISO = "ko", Name = "Korean" },
            //    new Language() { ISO = "nl", Name = "Dutch" },
            //    new Language() { ISO = "pl", Name = "Polish" },
            //    new Language() { ISO = "pt", Name = "Portuguese" },
            //    new Language() { ISO = "ro", Name = "Romanian" },
            //    new Language() { ISO = "uk", Name = "Ukrainian" },
            //    new Language() { ISO = "af", Name = "Afrikaans" },
            //    new Language() { ISO = "sr", Name = "Serbian" }
            //    );
            //context.SaveChanges();
        }
    }
}
