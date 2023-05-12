namespace DMOrganizerDomainModel
{
    public class Category
    {
        public Category(int categoryId, string name, Category parent)
        {
            ID = categoryId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public Category Parent { get; set; }
        // optional one-to-many: parent of books, may have no books
        public List<Book> Books { get; } = new List<Book>(); // Collection navigation containing dependents

    }
}