using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HansKindberg
{
	public class ActivatorWrapper : IInstanceFactory
	{
		#region Fields

		private const BindingFlags _bindings = BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		#endregion

		#region Properties

		protected internal virtual BindingFlags Bindings
		{
			get { return _bindings; }
		}

		#endregion

		#region Methods

		public virtual object Create(Type type)
		{
			return this.Create(type, Enumerable.Empty<object>());
		}

		public virtual object Create(Type type, IEnumerable<object> parameters)
		{
			return Activator.CreateInstance(type, this.Bindings, null, (parameters ?? Enumerable.Empty<object>()).ToArray(), null, null);
		}

		public virtual T Create<T>()
		{
			return (T) this.Create(typeof(T));
		}

		public virtual T Create<T>(IEnumerable<object> parameters)
		{
			return (T) this.Create(typeof(T), parameters);
		}

		#endregion
	}
}