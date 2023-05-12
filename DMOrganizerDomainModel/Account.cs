using System;
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string HashedPassword { get; set; }
        public virtual List<Organizer> Organizers { get; set; }
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
