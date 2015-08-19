using System.Linq;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectory
	{
		#region Methods

		IQueryable<T> Find<T>() where T : IEntry;
		IQueryable<T> Find<T>(string rootDistinguishedName) where T : IEntry;

		#endregion
	}
}