NDecorate
=====

Enables composition of multi-functional types using the decorator pattern.

Example
=====

```C#

  var query = new MyQuery();
  var decoratedQuery = query.Decorate<IMyQuery>(new [] { new CacheDecorator(), new LogDecorator() });

```