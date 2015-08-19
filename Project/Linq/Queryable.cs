using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace HansKindberg.Linq
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class Queryable<T> : IOrderedQueryable<T>
	{
		#region Fields

		private readonly Expression _expression;
		private readonly IQueryProvider _provider;

		#endregion

		#region Constructors

		public Queryable(IQueryProvider provider)
		{
			if(provider == null)
				throw new ArgumentNullException("provider");

			this._expression = Expression.Constant(this);
			this._provider = provider;
		}

		public Queryable(IQueryProvider provider, Expression expression) : this(provider, expression, typeof(IQueryable<T>)) {}

		protected internal Queryable(IQueryProvider provider, Expression expression, Type requiredAssignableQueryableType)
		{
			if(provider == null)
				throw new ArgumentNullException("provider");

			if(expression == null)
				throw new ArgumentNullException("expression");

			if(requiredAssignableQueryableType == null)
				throw new ArgumentNullException("requiredAssignableQueryableType");

			var queryableType = typeof(IQueryable);

			if(!queryableType.IsAssignableFrom(requiredAssignableQueryableType))
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The required assignable queryable-type must be assignable to \"{0}\".", queryableType), "requiredAssignableQueryableType");

			if(!requiredAssignableQueryableType.IsGenericType)
				throw new ArgumentException("The required assignable queryable-type must be a generic type.", "requiredAssignableQueryableType");

			if(!requiredAssignableQueryableType.IsAssignableFrom(expression.Type))
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The expression-type \"{0}\" is not assignable to \"{1}\".", expression.Type, requiredAssignableQueryableType), "expression");

			this._expression = expression;
			this._provider = provider;
		}

		#endregion

		#region Properties

		public virtual Type ElementType
		{
			get { return typeof(T); }
		}

		public virtual Expression Expression
		{
			get { return this._expression; }
		}

		public virtual IQueryProvider Provider
		{
			get { return this._provider; }
		}

		#endregion

		#region Methods

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Provider.Execute<IEnumerable>(this.Expression).GetEnumerator();
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			return this.Provider.Execute<IEnumerable<T>>(this.Expression).GetEnumerator();
		}

		public override string ToString()
		{
			var expression = this.Expression.NodeType == ExpressionType.Constant && ((ConstantExpression) this.Expression).Value == this ? string.Empty : this.Expression.ToString();

			return string.Format(CultureInfo.InvariantCulture, "{0}{1} - Expression: {2}{1} - Provider: {3}", base.ToString(), Environment.NewLine, expression, this.Provider);
		}

		#endregion
	}
}