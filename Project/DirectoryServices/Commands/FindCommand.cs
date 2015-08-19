using System.Collections.Generic;
using System.Linq;

namespace HansKindberg.DirectoryServices.Commands
{
	public class FindCommand : Command<IEnumerable<IEntry>>
	{
		#region Methods

		public override IEnumerable<IEntry> Execute()
		{
			return Enumerable.Empty<IEntry>();
		}

		#endregion
	}
}