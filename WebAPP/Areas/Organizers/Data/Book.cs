namespace WebAPP.Areas.Organizers.Data;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    // required one-to-many: child of category/organizer, parent is required
    public int ParentCategoryId { get; set; } 
    public CategoryBase ParentCategory { get; set; } 

    // required one-to-many: parent of pages
    public List<PageDMO> PageDMOs { get; set; } = new List<PageDMO>(); 
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

