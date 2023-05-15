using WebAPP.Areas.Organizers.Data;
using Microsoft.Extensions.DependencyModel;

namespace WebAPP.Data
{
    public static class DBInitializer
    {
        public static void Initialize(WebAPPContext context)
        {
            if (context.Organizers.Any()
                && context.Books.Any()
                && context.Pages.Any())
            { return; }            

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
                    }
            };
            var testBook2 = new Book
            {
                Name = "TestBook2",
                PageDMOs = new List<PageDMO>
                {
                    testPage2
                }
            };
            var testCategory = new Category { Name = "TestCat"};
            testCategory.Books.Add(testBook2);

            var testOrganizer = new Organizer { Name = "TestOrganizer"};
            testOrganizer.Books.Add(testBook1);
            testOrganizer.Subcategories.Add(testCategory);


            context.Organizers.Add(testOrganizer);

            context.SaveChanges();
        }
    }
}
