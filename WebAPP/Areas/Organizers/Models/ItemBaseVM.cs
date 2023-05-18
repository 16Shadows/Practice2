namespace WebAPP.Areas.Organizers.Models
{
	public class ItemBaseVM
	{
		protected ItemBaseVM(string type, string name, int id)
		{
			Type = type ?? throw new ArgumentNullException(nameof(type));
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Id = id;
		}

		public string Type { get; }
		public string Name { get; }
		public int Id { get; }
	}
}
