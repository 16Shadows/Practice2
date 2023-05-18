using Microsoft.AspNetCore.Identity;
using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Identity.Data;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

// Add profile data for application users by adding properties to the Account class
public class UserAccount : IdentityUser
{
    public List<Organizer> Organizers { get; set; }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.