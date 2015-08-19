# LINQ-Query-Provider

## Information
- [**Google** - and search for "query provider"](https://google.com)
- [**LINQ: Building an IQueryable Provider**](http://blogs.msdn.com/b/mattwar/archive/2008/11/18/linq-links.aspx)
	- [**IQToolkit (GitHub)**](https://github.com/mattwar/iqtoolkit)
- [**Walkthrough: Creating an IQueryable LINQ Provider**](https://msdn.microsoft.com/en-us/library/vstudio/bb546158(v=vs.110).aspx)

## Reminder - where I am at the moment
Do we need HansKindberg.Linq.QueryProvider&lt;T&gt;? The idea with this class is to control expressions sent to method "CreateQuery" and validate that they have an element-type with the same type as "T". But maybe this is unnecessary because maybe the whole process with IQueryProvider and IQueryable will handle it for me.

- [**HansKindberg.Linq.QueryProvider&lt;T&gt;**](/Project/Linq/QueryProvider.cs)
- [**Integration-tests for it**](/IntegrationTests/Linq/QueryProviderTest.cs)
- [**Unit-tests for it**](/UnitTests/Linq/QueryProviderTest.cs)

You are also keeping on here: - [**BasicDirectory integration-tests**](/IntegrationTests/DirectoryServices/BasicDirectoryTest.cs). At the moment with dummy implementation of [**HansKindberg.DirectoryServices.Linq.QueryProvider.GetCommand(Expression expression)**](/Project/DirectoryServices/Linq/QueryProvider.cs#L20). The [**commands**](/Project/DirectoryServices/Commands/) also have dummy-implementations.

