using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Document> Documents { get; } = new HashSet<Document>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
