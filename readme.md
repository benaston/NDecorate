NDecorate
=====

Enables composition of multi-functional types using the decorator pattern without requiring constructor injection or a concrete inheritance chain.

Example
=====

```C#

  //this query simply retrieves some stuff
  var query = new MyQuery();
  
  //decoratedQuery also has caching and logging capability
  var decoratedQuery = query.Decorate<IMyQuery>(new [] { new CacheDecorator(), new LogDecorator() });

```