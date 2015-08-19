using System;
using System.Collections.Generic;

namespace HansKindberg
{
	public interface IInstanceFactory
	{
		#region Methods

		object Create(Type type);
		object Create(Type type, IEnumerable<object> parameters);
		T Create<T>();
		T Create<T>(IEnumerable<object> parameters);

		#endregion
	}
}