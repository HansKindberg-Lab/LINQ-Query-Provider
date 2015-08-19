using System;

namespace HansKindberg.Extensions
{
	public interface ITypeExtension
	{
		#region Methods

		[Obsolete("Maybe not the correct name for the method.")]
		Type AsGenericType(Type type, Type genericTypeDefinition);

		string GetFriendlyFullName(Type type);
		string GetFriendlyName(Type type);

		#endregion
	}
}