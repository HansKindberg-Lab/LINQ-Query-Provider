using System.Linq;

namespace HansKindberg.DirectoryServices
{
	public class AdsiDirectory : BasicDirectory
	{
		#region Constructors

		public AdsiDirectory(IQueryProvider queryProvider) : base(queryProvider) {}

		#endregion
	}
}