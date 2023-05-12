using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    public class Section
    {
        public Section(string title, string text)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Title { get; set; }
        public string Text { get; set; }
        public List<Section> Sections { get; } = new List<Section>();
    }
}
