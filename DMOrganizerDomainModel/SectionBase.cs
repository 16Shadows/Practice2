using System;
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    public class SectionBase
    {
        public SectionBase(string title, string text)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public virtual string Text { get; set; }
        public virtual List<Section> Sections { get; } = new List<Section>();
    }
}
