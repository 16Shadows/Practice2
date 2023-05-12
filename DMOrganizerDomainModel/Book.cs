namespace DMOrganizerDomainModel
{
    public class Book
    {
        public Book(int bookID, string name, Category parentCategory)
        {
            ID = bookID;
            Name = name;
            ParentCategory = parentCategory; //nullable? can lay in root

        }
        public int ID { get; set; } //required
        public string Name { get; set; } //required
        public Category? ParentCategory { get; set; }// optional one-to-many: child of category, may have no parent category
        public List<PageDMO> PageDMOs { get; set; } = new List<PageDMO>(); // required one-to-many: parent of pages


        // need join table of pages, 1 to many, book ID will be in PageDMO
    }
}
