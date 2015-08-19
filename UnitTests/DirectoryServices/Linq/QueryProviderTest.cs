using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using HansKindberg.DirectoryServices;
using HansKindberg.DirectoryServices.Linq;
using HansKindberg.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.DirectoryServices.Linq
{
	[TestClass]
	public class QueryProviderTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CreateQuery_Generic_IfTheGenericElementParameterIsNotAssignableToTheEntryInterface_ShouldThrowAnArgumentException()
		{
			try
			{
				new QueryProvider(Mock.Of<IInstanceFactory>()).CreateQuery<object>(Mock.Of<Expression>());
			}
			catch(ArgumentException argumentException)
			{
				var expectedMessageStart = string.Format(CultureInfo.InvariantCulture, "The element-type, \"{0}\", must be assignable to \"{1}\".", typeof(object), typeof(IEntry));

				if(argumentException.Message.StartsWith(expectedMessageStart, StringComparison.OrdinalIgnoreCase) && argumentException.ParamName == "TElement")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CreateQuery_IfTheExpressionHasAGenericTypeThatHasAnElementTypeNotAssignableToTheEntryInterface_ShouldThrowAnArgumentException()
		{
			var expressionType = typeof(IQueryable<object>);

			var expressionMock = new Mock<Expression>();
			expressionMock.Setup(expression => expression.Type).Returns(expressionType);

			try
			{
				new QueryProvider(Mock.Of<IInstanceFactory>()).CreateQuery(expressionMock.Object);
			}
			catch(ArgumentException argumentException)
			{
				var expectedMessageStart = string.Format(CultureInfo.InvariantCulture, "The generic expression-type \"{0}\" for expression:{1}{1}\"{2}\"{1}{1} must have an element-type assignable to \"{3}\".", expressionType, Environment.NewLine, null, typeof(IEntry).FriendlyFullName());

				if(argumentException.Message.StartsWith(expectedMessageStart, StringComparison.OrdinalIgnoreCase) && argumentException.ParamName == "expression")
					throw;
			}
		}

		#endregion
	}
}