using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Identity.Data;

namespace WebAPP.Areas.Organizers.Data
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Index(nameof(OwnerId), nameof(Name), IsUnique = true)]
    public class Organizer : CategoryBase
    {
        public string OwnerId { get; set; }
        public virtual UserAccount Owner { get; set; }
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
