using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.Commands
{
	public abstract class Command
	{
		#region Fields

		private readonly ICollection<string> _attributes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		#endregion

		#region Properties

		public virtual ICollection<string> Attributes
		{
			get { return this._attributes; }
		}

		public virtual string Filter { get; set; }

		#endregion
	}

	public abstract class Command<T> : Command, ICommand<T>
	{
		#region Methods

		object ICommand.Execute()
		{
			return this.Execute();
		}

		public abstract T Execute();

		#endregion
	}
}