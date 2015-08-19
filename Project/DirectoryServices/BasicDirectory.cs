using System;
using System.Linq;
using HansKindberg.Linq;

namespace HansKindberg.DirectoryServices
{
	public abstract class BasicDirectory : IDirectory
	{
		#region Fields

		private readonly IQueryProvider _queryProvider;

		#endregion

		#region Constructors

		protected BasicDirectory(IQueryProvider queryProvider)
		{
			if(queryProvider == null)
				throw new ArgumentNullException("queryProvider");

			this._queryProvider = queryProvider;
		}

		#endregion

		#region Properties

		protected internal virtual IQueryProvider QueryProvider
		{
			get { return this._queryProvider; }
		}

		#endregion

		#region Methods

		public virtual IQueryable<T> Find<T>() where T : IEntry
		{
			//return this.Find<T>(this.DirectorySetting.Search.DistinguishedName);
			return this.Find<T>(string.Empty);
		}

		public virtual IQueryable<T> Find<T>(string rootDistinguishedName) where T : IEntry
		{
			return new Queryable<T>(this.QueryProvider);

			//throw new NotImplementedException();
			////	//var queryProvider = new QueryProvider<T>(null, new ExpressionTranslatorFactory(new AttributeMapping<T>(), new FilterConcatenator(), new ValueResolver()), this.FindInternal);

			////	//////			return new Queryable<T>(queryProvider);
		}

		#endregion
	}
}