using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using HansKindberg.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.Linq
{
	[TestClass]
	public class QueryableTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_WithOneParameter_IfTheProviderParameterIsNotNull_ShouldSetTheExpression()
		{
			var queryable = new Queryable<string>(Mock.Of<IQueryProvider>());

			Assert.AreEqual(queryable, ((ConstantExpression) queryable.Expression).Value);
			Assert.IsTrue(typeof(IQueryable<string>).IsAssignableFrom(queryable.Expression.Type));
		}

		[TestMethod]
		public void Constructor_WithOneParameter_IfTheProviderParameterIsNotNull_ShouldSetTheProvider()
		{
			var provider = Mock.Of<IQueryProvider>();

			var queryable = new Queryable<object>(provider);

			Assert.AreEqual(provider, queryable.Provider);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithOneParameter_IfTheProviderParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "provider")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithThreeParameters_IfTheExpressionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(Mock.Of<IQueryProvider>(), null, typeof(object));
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "expression")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithThreeParameters_IfTheExpressionTypeIsNotAssignableToTheRequiredAssignableQueryableType_ShouldThrowAnArgumentException()
		{
			var expressionType = typeof(IQueryable<object>);
			var requiredType = typeof(IQueryable<string>);

			var expressionMock = new Mock<Expression>();
			expressionMock.Setup(expression => expression.Type).Returns(expressionType);

			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(Mock.Of<IQueryProvider>(), expressionMock.Object, requiredType);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.Message.StartsWith(string.Format(CultureInfo.InvariantCulture, "The expression-type \"{0}\" is not assignable to \"{1}\".", expressionType, requiredType), StringComparison.OrdinalIgnoreCase) && argumentException.ParamName == "expression")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithThreeParameters_IfTheProviderParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(null, Mock.Of<Expression>(), typeof(object));
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "provider")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithThreeParameters_IfTheRequiredAssignableQueryableTypeParameterIsNotAGenericType_ShouldThrowAnArgumentException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(Mock.Of<IQueryProvider>(), Mock.Of<Expression>(), typeof(IQueryable));
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.Message.StartsWith("The required assignable queryable-type must be a generic type.", StringComparison.OrdinalIgnoreCase) && argumentException.ParamName == "requiredAssignableQueryableType")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithThreeParameters_IfTheRequiredAssignableQueryableTypeParameterIsNotAssignableToIQueryable_ShouldThrowAnArgumentException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(Mock.Of<IQueryProvider>(), Mock.Of<Expression>(), typeof(object));
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.Message.StartsWith(string.Format(CultureInfo.InvariantCulture, "The required assignable queryable-type must be assignable to \"{0}\".", typeof(IQueryable)), StringComparison.OrdinalIgnoreCase) && argumentException.ParamName == "requiredAssignableQueryableType")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithThreeParameters_IfTheRequiredAssignableQueryableTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(Mock.Of<IQueryProvider>(), Mock.Of<Expression>(), null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "requiredAssignableQueryableType")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithTwoParameters_IfTheExpressionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(Mock.Of<IQueryProvider>(), null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "expression")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.String>")]
		public void Constructor_WithTwoParameters_IfTheExpressionTypeIsNotAssignableToAQueryableOfTheGenericTypeParameter_ShouldThrowAnArgumentException()
		{
			var expressionType = typeof(IQueryable<object>);

			var expressionMock = new Mock<Expression>();
			expressionMock.Setup(expression => expression.Type).Returns(expressionType);

			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<string>(Mock.Of<IQueryProvider>(), expressionMock.Object);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.Message.StartsWith(string.Format(CultureInfo.InvariantCulture, "The expression-type \"{0}\" is not assignable to \"{1}\".", expressionType, typeof(IQueryable<string>)), StringComparison.OrdinalIgnoreCase) && argumentException.ParamName == "expression")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Linq.Queryable`1<System.Object>")]
		public void Constructor_WithTwoParameters_IfTheProviderParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Queryable<object>(null, Mock.Of<Expression>());
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "provider")
					throw;
			}
		}

		[TestMethod]
		public void ToString_ShouldReturnInformationAboutTheTypeAndTheExpressionAndTheProvider()
		{
			const string expressionValue = "Expression-value";
			const string providerValue = "Provider-value";

			var expressionMock = new Mock<Expression>();
			expressionMock.Setup(expression => expression.Type).Returns(typeof(IQueryable<object>));
			expressionMock.Setup(expression => expression.ToString()).Returns(expressionValue);

			var providerMock = new Mock<Mock<IQueryProvider>>(); // We need to mock a type that overrides the "ToString" method. Using Mock<IQueryProvider> here does not work.
			providerMock.Setup(provider => provider.ToString()).Returns(providerValue);

			var expected = string.Format(CultureInfo.InvariantCulture, "{0}{1} - Expression: {2}{1} - Provider: {3}", "HansKindberg.Linq.Queryable`1[System.Object]", Environment.NewLine, expressionValue, providerValue);

			Assert.AreEqual(expected, new Queryable<object>(providerMock.As<IQueryProvider>().Object, expressionMock.Object).ToString());
		}

		#endregion
	}
}