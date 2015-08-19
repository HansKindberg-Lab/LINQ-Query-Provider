using System;

namespace HansKindberg.Extensions
{
	public interface ITypeExtension
	{
		#region Methods

		Type AsGenericType(Type type, Type genericTypeDefinition);
		string GetFriendlyFullName(Type type);
		string GetFriendlyName(Type type);

		#endregion
	}
}