using System.Linq;
using HansKindberg.DirectoryServices;
using HansKindberg.DirectoryServices.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.IntegrationTests.DirectoryServices
{
	[TestClass]
	public class BasicDirectoryTest
	{
		#region Methods

		[TestMethod]
		public void Count_Test()
		{
			var queryProvider = new QueryProvider(new ActivatorWrapper());

			var directory = new Mock<BasicDirectory>(queryProvider) {CallBase = true}.Object;

			// ReSharper disable ReplaceWithSingleCallToCount
			var count = directory.Find<IEntry>().Where(entry => entry.DistinguishedName.Contains("a")).Count();
			// ReSharper restore ReplaceWithSingleCallToCount

			Assert.AreEqual(0, count);

			count = directory.Find<IEntry>().Count(entry => entry.DistinguishedName.Contains("a"));

			Assert.AreEqual(0, count);

			//Assert.Inconclusive("Temporary");
		}

		[TestMethod]
		public void Find_Test()
		{
			var queryProvider = new QueryProvider(new ActivatorWrapper());

			var directory = new Mock<BasicDirectory>(queryProvider) {CallBase = true}.Object;

			var result = directory.Find<IEntry>().Where(entry => entry.DistinguishedName.Contains("a"));

			//Assert.AreEqual(1, result.Count());

			foreach(var entry in result)
			{
				Assert.AreEqual("Test", entry.DistinguishedName);
			}

			Assert.AreEqual(0, result.Count());

			//Assert.Inconclusive("Temporary");
		}

		[TestMethod]
		public void Miscellaneous_Tests()
		{
			var queryProvider = new QueryProvider(new ActivatorWrapper());

			var directory = new Mock<BasicDirectory>(queryProvider) {CallBase = true}.Object;

			var result = directory.Find<IEntry>().Where(entry => entry.DistinguishedName.Contains("a")).Select(entry => entry.DistinguishedName);

			//Assert.AreEqual(1, result.Count());

			foreach(var entry in result)
			{
				Assert.AreEqual("Test", entry);
			}

			Assert.AreEqual(0, result.Count());

			//Assert.Inconclusive("Temporary");
		}

		#endregion
	}
}