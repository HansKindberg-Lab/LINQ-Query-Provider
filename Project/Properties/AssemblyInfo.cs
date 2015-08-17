using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyDescription("Enter a description")]
[assembly: CLSCompliant(true)]
[assembly: Guid("3ed38658-e947-42a4-8a75-1f61229d572d")]
[assembly: InternalsVisibleTo("HansKindberg.IntegrationTests")]
[assembly: InternalsVisibleTo("HansKindberg.ShimTests")]
[assembly: InternalsVisibleTo("HansKindberg.UnitTests")]

// ReSharper disable CheckNamespace
internal static class AssemblyInfo // ReSharper restore CheckNamespace
{
	#region Fields

	internal const string AssemblyName = "HansKindberg";

	#endregion
}