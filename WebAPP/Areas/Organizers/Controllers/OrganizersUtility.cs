using System.ComponentModel;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Controllers
{
	public static class OrganizersUtility
	{
		public static Organizer? GetOrganizer(this WebAPPContext context, UserAccount user, int organizerId)
		{
			return context.Organizers.Where(x => x.Id == organizerId && x.OwnerId == user.Id).ToArray().FirstOrDefault();
		}

		public static Organizer? GetOrganizer(this WebAPPContext context, UserAccount user, string name)
		{
			return context.Organizers.Where(x => x.OwnerId == user.Id && x.Name == name).ToArray().FirstOrDefault();
		}

		public static bool HasOrganizer(this WebAPPContext context, UserAccount user, int organizerId)
		{
			return context.GetOrganizer(user, organizerId) != null;
		}

		public static bool HasOrganizer(this WebAPPContext context, UserAccount user, string name)
		{
			return context.GetOrganizer(user, name) != null;
		}

		public static Category? GetCategory(this WebAPPContext context, int organizerId, int categoryId)
		{
			return context.Categories.Where(x => x.OrganizerId == organizerId && x.Id == categoryId).FirstOrDefault();
		}

		public static Document? GetDocument(this WebAPPContext context, int organizerId, int documentId)
		{
			return context.Documents.Where(x => x.OrganizerId == organizerId && x.Id == documentId).FirstOrDefault();
		}
	}
}
