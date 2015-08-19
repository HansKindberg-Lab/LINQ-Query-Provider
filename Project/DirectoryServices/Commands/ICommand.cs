namespace HansKindberg.DirectoryServices.Commands
{
	public interface ICommand
	{
		#region Methods

		object Execute();

		#endregion
	}

	public interface ICommand<out T> : ICommand
	{
		#region Methods

		new T Execute();

		#endregion
	}
}