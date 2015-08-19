using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using HansKindberg.Extensions;

namespace HansKindberg.Linq
{
	public abstract class QueryProvider : IQueryProvider
	{
		#region Fields

		private readonly IInstanceFactory _instanceFactory;

		#endregion

		#region Constructors

		protected QueryProvider(IInstanceFactory instanceFactory)
		{
			if(instanceFactory == null)
				throw new ArgumentNullException("instanceFactory");

			this._instanceFactory = instanceFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IInstanceFactory InstanceFactory
		{
			get { return this._instanceFactory; }
		}

		#endregion

		#region Methods

		public virtual IQueryable CreateQuery(Expression expression)
		{
			return (IQueryable) this.InstanceFactory.Create(typeof(Queryable<>).MakeGenericType(this.GetElementType(expression)), new object[] {this, expression});
		}

		public virtual IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			this.ValidateElementType(expression, typeof(TElement));

			return new Queryable<TElement>(this, expression);
		}

		public abstract object Execute(Expression expression);

		public virtual TResult Execute<TResult>(Expression expression)
		{
			return (TResult) this.Execute(expression);
		}

		protected internal virtual Type GetElementType(Expression expression)
		{
			if(expression == null)
				throw new ArgumentNullException("expression");

			if(expression.Type == null)
				throw new ArgumentException("The expression-type can not be null.", "expression");

			var genericTypeDefinition = typeof(IQueryable<>);

			var genericType = expression.Type.AsGenericType(genericTypeDefinition);

			if(genericType == null)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The expression-type \"{0}\" for expression \"{1}\" must be a generic type of the generic type definition \"{2}\".", expression.Type, expression, genericTypeDefinition.FriendlyFullName()), "expression");

			var elementType = genericType.GetGenericArguments()[0];

			this.ValidateElementType(expression, elementType);

			return elementType;
		}

		protected internal virtual void ValidateElementType(Expression expression, Type elementType) {}

		#endregion
	}

	[Obsolete("Not sure if we need this generic query-provider.")]
	public abstract class QueryProvider<T> : QueryProvider
	{
		#region Constructors

		protected QueryProvider(IInstanceFactory instanceFactory) : base(instanceFactory) {}

		#endregion

		#region Methods

		protected internal override void ValidateElementType(Expression expression, Type elementType)
		{
			var validElementType = typeof(T);

			if(!validElementType.IsAssignableFrom(elementType))
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The generic expression-type \"{0}\" for expression \"{1}\" must have an element-type assignable to \"{2}\".", expression != null ? expression.Type : null, expression, validElementType.FriendlyFullName()), "expression");
		}

		#endregion
	}
}