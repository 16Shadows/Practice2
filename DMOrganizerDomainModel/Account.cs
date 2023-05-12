using System;
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    public class Account
    {
        public Account(string name, string hashedPassword)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            HashedPassword = hashedPassword ?? throw new ArgumentNullException(nameof(hashedPassword));
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public virtual List<Organizer> Organizers { get; set; } = new List<Organizer>();
    }
}
