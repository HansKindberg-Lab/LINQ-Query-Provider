using System;
using HansKindberg.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.Linq
{
	[TestClass]
	public class QueryProviderTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CreateQuery_Generic_IfTheExpressionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				CreateQueryProvider().CreateQuery<object>(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "expression")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CreateQuery_IfTheExpressionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				CreateQueryProvider().CreateQuery(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "expression")
					throw;
			}
		}

		private static QueryProvider CreateQueryProvider()
		{
			return CreateQueryProvider(Mock.Of<IInstanceFactory>());
		}

		private static QueryProvider CreateQueryProvider(IInstanceFactory instanceFactory)
		{
			return new Mock<QueryProvider>(instanceFactory) {CallBase = true}.Object;
		}

		#endregion
	}
}