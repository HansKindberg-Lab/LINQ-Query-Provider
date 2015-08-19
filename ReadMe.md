# LINQ-Query-Provider

## Information
- [**Google** - and search for "query provider"](https://google.com)
- [**LINQ: Building an IQueryable Provider**](http://blogs.msdn.com/b/mattwar/archive/2008/11/18/linq-links.aspx)
	- [**IQToolkit (GitHub)**](https://github.com/mattwar/iqtoolkit)
- [**Walkthrough: Creating an IQueryable LINQ Provider**](https://msdn.microsoft.com/en-us/library/vstudio/bb546158(v=vs.110).aspx)

## Reminder - where I am at the moment
[**BasicDirectory integration-tests**](/IntegrationTests/DirectoryServices/BasicDirectoryTest.cs). At the moment with dummy implementation of [**HansKindberg.DirectoryServices.Linq.QueryProvider.GetCommand(Expression expression)**](/Project/DirectoryServices/Linq/QueryProvider.cs#L24). The [**commands**](/Project/DirectoryServices/Commands/) also have dummy-implementations.