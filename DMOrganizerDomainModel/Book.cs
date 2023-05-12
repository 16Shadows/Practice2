using System.Collections.Generic;
namespace DMOrganizerDomainModel
{
    public class Book
    {
        public Book(string name, CategoryBase parentCategory)
        {
            Name = name;
            ParentCategory = parentCategory;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        // required one-to-many: child of category/organizer, parent is required
        public int ParentCategoryID { get; set; } 
        public CategoryBase ParentCategory { get; set; } 

        // required one-to-many: parent of pages
        public List<PageDMO> PageDMOs { get; set; } = new List<PageDMO>(); 
    }
}
