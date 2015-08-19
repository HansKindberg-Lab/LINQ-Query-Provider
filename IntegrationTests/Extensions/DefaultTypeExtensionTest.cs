using System.Collections.Generic;
using HansKindberg.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.IntegrationTests.Extensions
{
	[TestClass]
	public class DefaultTypeExtensionTest
	{
		#region Methods

		[TestMethod]
		public void AsGenericType_IfTheTypeIsAnArrayAndTheGenericTypeDefinitionIsTheTypeDefinitionForTheEnumerableInterface_ShouldNotReturnNull()
		{
			var typeExtension = new DefaultTypeExtension();

			Assert.IsNotNull(typeExtension.AsGenericType(typeof(object[]), typeof(IEnumerable<>)));
			Assert.IsNotNull(typeExtension.AsGenericType(typeof(int[]), typeof(IEnumerable<>)));
			Assert.IsNotNull(typeExtension.AsGenericType(typeof(string[]), typeof(IEnumerable<>)));
		}

		[TestMethod]
		public void GetFriendlyFullName_IfTheTypeIsAGenericType_ShouldWorkCorrectly()
		{
			var typeExtension = new DefaultTypeExtension();

			Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.Int32>", typeExtension.GetFriendlyFullName(typeof(Dictionary<string, int>)));
			Assert.AreEqual("System.Collections.Generic.Dictionary<System.Object, System.Boolean>", typeExtension.GetFriendlyFullName(typeof(Dictionary<object, bool>)));

			Assert.AreEqual("System.Collections.Generic.IDictionary<System.String, System.Int32>", typeExtension.GetFriendlyFullName(typeof(IDictionary<string, int>)));
			Assert.AreEqual("System.Collections.Generic.IDictionary<System.Object, System.Boolean>", typeExtension.GetFriendlyFullName(typeof(IDictionary<object, bool>)));

			Assert.AreEqual("System.Collections.Generic.IEnumerable<System.Int32>", typeExtension.GetFriendlyFullName(typeof(IEnumerable<int>)));
			Assert.AreEqual("System.Collections.Generic.IEnumerable<System.Object>", typeExtension.GetFriendlyFullName(typeof(IEnumerable<object>)));
			Assert.AreEqual("System.Collections.Generic.IEnumerable<System.String>", typeExtension.GetFriendlyFullName(typeof(IEnumerable<string>)));

			Assert.AreEqual("System.Collections.Generic.List<System.Int32>", typeExtension.GetFriendlyFullName(typeof(List<int>)));
			Assert.AreEqual("System.Collections.Generic.List<System.Object>", typeExtension.GetFriendlyFullName(typeof(List<object>)));
			Assert.AreEqual("System.Collections.Generic.List<System.String>", typeExtension.GetFriendlyFullName(typeof(List<string>)));
		}

		[TestMethod]
		public void GetFriendlyFullName_IfTheTypeIsAGenericTypeDefinition_ShouldWorkCorrectly()
		{
			var typeExtension = new DefaultTypeExtension();

			Assert.AreEqual("System.Collections.Generic.Dictionary<TKey, TValue>", typeExtension.GetFriendlyFullName(typeof(Dictionary<,>)));

			Assert.AreEqual("System.Collections.Generic.IDictionary<TKey, TValue>", typeExtension.GetFriendlyFullName(typeof(IDictionary<,>)));

			Assert.AreEqual("System.Collections.Generic.IEnumerable<T>", typeExtension.GetFriendlyFullName(typeof(IEnumerable<>)));

			Assert.AreEqual("System.Collections.Generic.List<T>", typeExtension.GetFriendlyFullName(typeof(List<>)));
		}

		[TestMethod]
		public void GetFriendlyName_IfTheTypeIsAGenericType_ShouldWorkCorrectly()
		{
			var typeExtension = new DefaultTypeExtension();

			Assert.AreEqual("Dictionary<String, Int32>", typeExtension.GetFriendlyName(typeof(Dictionary<string, int>)));
			Assert.AreEqual("Dictionary<Object, Boolean>", typeExtension.GetFriendlyName(typeof(Dictionary<object, bool>)));

			Assert.AreEqual("IDictionary<String, Int32>", typeExtension.GetFriendlyName(typeof(IDictionary<string, int>)));
			Assert.AreEqual("IDictionary<Object, Boolean>", typeExtension.GetFriendlyName(typeof(IDictionary<object, bool>)));

			Assert.AreEqual("IEnumerable<Int32>", typeExtension.GetFriendlyName(typeof(IEnumerable<int>)));
			Assert.AreEqual("IEnumerable<Object>", typeExtension.GetFriendlyName(typeof(IEnumerable<object>)));
			Assert.AreEqual("IEnumerable<String>", typeExtension.GetFriendlyName(typeof(IEnumerable<string>)));

			Assert.AreEqual("List<Int32>", typeExtension.GetFriendlyName(typeof(List<int>)));
			Assert.AreEqual("List<Object>", typeExtension.GetFriendlyName(typeof(List<object>)));
			Assert.AreEqual("List<String>", typeExtension.GetFriendlyName(typeof(List<string>)));
		}

		[TestMethod]
		public void GetFriendlyName_IfTheTypeIsAGenericTypeDefinition_ShouldWorkCorrectly()
		{
			var typeExtension = new DefaultTypeExtension();

			Assert.AreEqual("Dictionary<TKey, TValue>", typeExtension.GetFriendlyName(typeof(Dictionary<,>)));

			Assert.AreEqual("IDictionary<TKey, TValue>", typeExtension.GetFriendlyName(typeof(IDictionary<,>)));

			Assert.AreEqual("IEnumerable<T>", typeExtension.GetFriendlyName(typeof(IEnumerable<>)));

			Assert.AreEqual("List<T>", typeExtension.GetFriendlyName(typeof(List<>)));
		}

		#endregion
	}
}