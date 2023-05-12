using Microsoft.EntityFrameworkCore;

namespace DMOrganizerWebModel
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


    }
}