NDecorate
=====

Enables composition of multi-functional types using the decorator pattern without requiring constructor injection or a concrete inheritance chain.

Weirldy enough, an [old (2004) article on NHibernate](http://www.theserverside.net/tt/articles/showarticle.tss?id=NHibernate) espouses the reason for NDecorate very well:

 > Last month, Bruce Tate and I released a new book called “Better, Faster, Lighter Java”. Don’t let that “j” word in the title throw you too much; the principles we espouse in the book are equally applicable to any modern development platform. One of those principles is transparency; the key to any enterprise application is the domain model. These are the classes that model, and solve, your customers’ business problems. If you customer is a bank, your domain model is filled with Accounts, Deposits and Loans. If your customer is a travel agent, your domain is filled with Tours and Hotels and Airlines. It is in these classes that your customers’ problems are addressed; everything else is just a service to support the domain. I mean things like data storage, message transport, transactional control, etc. As much as possible, you want those services to be transparent to your domain model. Transparency means that your model benefits from those services without being modified by them. It shouldn’t require special code in your domain to utilize those services, it shouldn’t require specific containers, or interfaces to implement. Which means that your domain architecture can be 100% focused on the business problem at hand, not technical problems outside the business. A side effect of achieving transparency is that you can replace services with alternate providers or add new services without changing your domain.

Example
=====

```C#

  //this query simply retrieves some stuff
  var query = new MyQuery();
  
  //decoratedQuery also has caching and logging capability
  var decoratedQuery = query.Decorate<IMyQuery>(new [] { new CacheDecorator(), new LogDecorator() });

```

TODO
=====
 - better IOC container integration (add method to decorate all types in the container?)
 - file-based configuration to take advantage of environment-based transforms
