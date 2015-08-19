using System;
using System.Linq;
using System.Linq.Expressions;
using HansKindberg.DirectoryServices.Commands;

namespace HansKindberg.DirectoryServices.Linq
{
	public class QueryProvider : HansKindberg.Linq.QueryProvider<IEntry>
	{
		#region Constructors

		public QueryProvider(IInstanceFactory instanceFactory) : base(instanceFactory) {}

		#endregion

		#region Methods

		public override object Execute(Expression expression)
		{
			return this.GetCommand(expression).Execute();
		}

		[Obsolete("DUMMY implementation")]
		protected internal virtual ICommand GetCommand(Expression expression)
		{
			if(expression == null)
				throw new ArgumentNullException("expression");

			if(typeof(IQueryable<IEntry>).IsAssignableFrom(expression.Type))
				return new FindCommand();

			if(typeof(int).IsAssignableFrom(expression.Type))
				return new CountCommand();

			throw new NotImplementedException();
		}

		#endregion
	}
}