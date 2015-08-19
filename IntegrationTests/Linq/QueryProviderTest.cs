using System.Linq;
using System.Linq.Expressions;
using HansKindberg.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.IntegrationTests.Linq
{
	[TestClass]
	public class QueryProviderTest
	{
		#region Methods

		//private static Expression CreateExpression<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>> predicate)
		//{
		//	if (queryable == null)
		//		throw new ArgumentNullException("queryable");

		//	if (predicate == null)
		//		throw new ArgumentNullException("predicate");

		//	return Expression.Call(null, ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(typeof(TSource)), new[] { queryable.Expression, Expression.Quote(predicate) });
		//}
		[TestMethod]
		public void CreateQuery_Test()
		{
			var queryProvider = CreateQueryProvider<string>();

			var queryable = new Queryable<string>(queryProvider);

			var b = queryable.Where(value => value.Contains("A")).Expression;

			b = b;

			foreach(var item in queryable.Where(value => value.Contains("A")))
			{
				var temp = item;
				temp = temp;
			}

			//var expression = CreateExpression(queryable, value => value.Contains("A"));

			//expression = expression;

			Assert.Inconclusive("Temporary");

			////var expression = new[] {"A", "B", "C"}.Where(value => value != "C").AsQueryable().Expression;
			//var expression = new[] { "A", "B", "C" }.AsQueryable().Expression;

			//try
			//{
			//	CreateQueryProvider().CreateQuery<string>(expression);
			//}
			//catch(ArgumentNullException argumentNullException)
			//{
			//	if(argumentNullException.ParamName == "expression")
			//		throw;
			//}
		}

		private static QueryProvider CreateQueryProvider<TElement>()
		{
			var queryProviderMock = new Mock<QueryProvider>(new ActivatorWrapper()) {CallBase = true};

			queryProviderMock.Setup(queryProvider => queryProvider.Execute(It.IsAny<Expression>())).Returns(Enumerable.Empty<TElement>());

			return queryProviderMock.Object;
		}

		#endregion
	}
}