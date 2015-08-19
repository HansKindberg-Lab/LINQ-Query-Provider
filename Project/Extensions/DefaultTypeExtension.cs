using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;

namespace HansKindberg.Extensions
{
	public class DefaultTypeExtension : ITypeExtension
	{
		#region Methods

		[Obsolete("Maybe not the correct name for the method.")]
		public virtual Type AsGenericType(Type type, Type genericTypeDefinition)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			if(genericTypeDefinition == null)
				throw new ArgumentNullException("genericTypeDefinition");

			if(!genericTypeDefinition.IsGenericTypeDefinition)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The generic-type-definition-type \"{0}\" does not represent a generic type definition. A type representing a generic type definition is for example typeof(IEnumerable<>).", genericTypeDefinition), "genericTypeDefinition");

			while(type != null && type != typeof(object))
			{
				if(type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
					return type;

				if(genericTypeDefinition.IsInterface)
				{
					// ReSharper disable LoopCanBePartlyConvertedToQuery
					foreach(var interfaceType in type.GetInterfaces())
					{
						var genericType = this.AsGenericType(interfaceType, genericTypeDefinition);

						if(genericType != null)
							return genericType;
					}
					// ReSharper restore LoopCanBePartlyConvertedToQuery
				}

				type = type.BaseType;
			}

			return null;
		}

		public virtual string GetFriendlyFullName(Type type)
		{
			return this.GetFriendlyName(type, t => t.FullName);
		}

		public virtual string GetFriendlyName(Type type)
		{
			return this.GetFriendlyName(type, t => t.Name);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual string GetFriendlyName(Type type, Expression<Func<Type, string>> nameFunctionExpression)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			if(nameFunctionExpression == null)
				throw new ArgumentNullException("nameFunctionExpression");

			if(type.IsGenericParameter)
				return type.Name;

			var name = nameFunctionExpression.Compile().Invoke(type);

			if(!type.IsGenericType)
				return name;

			name = name.Substring(0, name.IndexOf("`", StringComparison.OrdinalIgnoreCase));

			var genericArgumentFriendlyName = string.Empty;

			foreach(var genericArgument in type.GetGenericArguments())
			{
				if(!string.IsNullOrEmpty(genericArgumentFriendlyName))
					genericArgumentFriendlyName += ", ";

				genericArgumentFriendlyName += this.GetFriendlyName(genericArgument, nameFunctionExpression);
			}

			return string.Format(CultureInfo.InvariantCulture, "{0}<{1}>", name, genericArgumentFriendlyName);
		}

		#endregion
	}
}