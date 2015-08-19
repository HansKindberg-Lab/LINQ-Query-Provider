using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using HansKindberg.Extensions;
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

	[TestClass]
	public class GenericQueryProviderTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CreateQuery_Generic_IfTheExpressionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				CreateQueryProvider<object>().CreateQuery<object>(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "expression")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CreateQuery_Generic_IfTheGenericElementParameterIsNotAssignableToTheGenericParameterType_ShouldThrowAnArgumentException()
		{
			try
			{
				CreateQueryProvider<string>().CreateQuery<object>(Mock.Of<Expression>());
			}
			catch(ArgumentException argumentException)
			{
				var expectedMessageStart = string.Format(CultureInfo.InvariantCulture, "The element-type, \"{0}\", must be assignable to \"{1}\".", typeof(object), typeof(string));

				if(argumentException.Message.StartsWith(expectedMessageStart, StringComparison.OrdinalIgnoreCase) && argumentException.ParamName == "TElement")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CreateQuery_IfTheExpressionHasAGenericQueryableTypeThatHasAnElementTypeNotAssignableToTheGenericParameterType_ShouldThrowAnArgumentException()
		{
			var expressionType = typeof(IQueryable<object>);

			var expressionMock = new Mock<Expression>();
			expressionMock.Setup(expression => expression.Type).Returns(expressionType);

			try
			{
				CreateQueryProvider<string>().CreateQuery(expressionMock.Object);
			}
			catch(ArgumentException argumentException)
			{
				var expectedMessageStart = string.Format(CultureInfo.InvariantCulture, "The generic expression-type \"{0}\" for expression:{1}{1}\"{2}\"{1}{1} must have an element-type assignable to \"{3}\".", expressionType, Environment.NewLine, null, typeof(string).FriendlyFullName());

				if(argumentException.Message.StartsWith(expectedMessageStart, StringComparison.OrdinalIgnoreCase) && argumentException.ParamName == "expression")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CreateQuery_IfTheExpressionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				CreateQueryProvider<object>().CreateQuery(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "expression")
					throw;
			}
		}

		private static QueryProvider CreateQueryProvider<T>()
		{
			return CreateQueryProvider<T>(Mock.Of<IInstanceFactory>());
		}

		private static QueryProvider CreateQueryProvider<T>(IInstanceFactory instanceFactory)
		{
			return new Mock<QueryProvider<T>>(instanceFactory) {CallBase = true}.Object;
		}

		#endregion
	}
}