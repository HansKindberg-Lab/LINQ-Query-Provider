using System;

namespace HansKindberg.DirectoryServices
{
	public class Entry : IEntry
	{
		#region Fields

		private string _distinguishedName;

		#endregion

		#region Constructors

		public Entry(string distinguishedName)
		{
			if(distinguishedName == null)
				throw new ArgumentNullException("distinguishedName");

			if(string.IsNullOrWhiteSpace(distinguishedName))
				throw new ArgumentException("The distinguished-name can not be empty.", "distinguishedName");

			this._distinguishedName = distinguishedName;
		}

		#endregion

		#region Properties

		public virtual string DistinguishedName
		{
			get { return this._distinguishedName; }
			protected set { this._distinguishedName = value; }
		}

		public virtual bool IsReadOnly { get; protected internal set; }

		#endregion
	}
}