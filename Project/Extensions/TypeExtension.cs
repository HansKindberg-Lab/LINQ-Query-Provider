using System;

namespace HansKindberg.Extensions
{
	public static class TypeExtension
	{
		#region Fields

		private static volatile ITypeExtension _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static ITypeExtension Instance
		{
			get
			{
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
							_instance = new DefaultTypeExtension();
					}
				}

				return _instance;
			}
			set
			{
				if(Equals(_instance, value))
					return;

				lock(_lockObject)
				{
					_instance = value;
				}
			}
		}

		#endregion

		#region Methods

		public static Type AsGenericType(this Type type, Type genericTypeDefinition)
		{
			return Instance.AsGenericType(type, genericTypeDefinition);
		}

		public static string FriendlyFullName(this Type type)
		{
			return Instance.GetFriendlyFullName(type);
		}

		public static string FriendlyName(this Type type)
		{
			return Instance.GetFriendlyName(type);
		}

		#endregion
	}
}