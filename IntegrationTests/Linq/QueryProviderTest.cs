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

		[TestMethod]
		public void CreateQuery_Test()
		{
			var queryProvider = CreateQueryProvider<string>();

			var queryable = new Queryable<string>(queryProvider);

			var b = queryable.Where(value => value.Contains("A")).Expression;
			Assert.IsNotNull(b);

			foreach(var item in queryable.Where(value => value.Contains("A")))
			{
				var temp = item;
				Assert.IsNotNull(temp);
			}

			Assert.Inconclusive("Temporary");
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