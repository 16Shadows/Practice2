using DMOrganizerDomainModel;
using Microsoft.Extensions.DependencyModel;
using WebAPP.Models;

namespace WebAPP.Data
{
    public static class DBInitializer
    {
        public static void Initialize(DMOrganizerDBContext context)
        {
            if (context.Accounts.Any()
                && context.Organizers.Any()
                && context.Books.Any()
                && context.Pages.Any())
            { return; }

            var testAccount = new Account { Name = "Gnome", HashedPassword = "Smol" };
            context.Accounts.Add(testAccount);
            var testOrganizer = new Organizer { Name = "TestOrganizer", Owner = testAccount };
            context.Organizers.Add(testOrganizer);
            var testCategory = new Category { Name = "TestCat", Parent = testOrganizer };
            context.Categories.Add(testCategory);

            var testPage1 = new PageDMO { Position = 1 };
            var testPage2 = new PageDMO { Position = 1 };
            var testPage3 = new PageDMO { Position = 2 };

            var testBook1 = new Book
            {
                Name = "TestBook1",
                PageDMOs = new List<PageDMO>
                    {
                        testPage1,
                        testPage3
                    },
                ParentCategory = testCategory
            };
            var testBook2 = new Book
            {
                Name = "TestBook2",
                PageDMOs = new List<PageDMO>
                {
                    testPage2
                },
                ParentCategory = testOrganizer
            };
            context.Books.Add(testBook1);
            context.Books.Add(testBook2);

            context.SaveChanges();
        }
    }
}
