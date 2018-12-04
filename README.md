# LinqBuilder
[![Build status](https://ci.appveyor.com/api/projects/status/v0t8rfsv0d4q3hap?svg=true)](https://ci.appveyor.com/project/Baune8D/linqbuilder)
[![codecov](https://codecov.io/gh/Baune8D/linqbuilder/branch/master/graph/badge.svg)](https://codecov.io/gh/Baune8D/linqbuilder)
[![Analytics](https://ga-beacon.appspot.com/UA-121440026-1/linqbuilder)](https://github.com/igrigorik/ga-beacon)

**Available on NuGet:** [https://www.nuget.org/packages?q=linqbuilder](https://www.nuget.org/packages?q=linqbuilder)  
**Dev build feed:** [https://www.myget.org/F/baunegaard/api/v3/index.json](https://www.myget.org/F/baunegaard/api/v3/index.json)  

LinqBuilder is based on the specification pattern.

## Table of Contents
1. [LinqBuilder](#linqbuilder)
2. [LinqBuilder.OrderBy](#linqbuilder.orderby)
3. [LinqBuilder.EF6](#linqbuilder.ef6)
4. [LinqBuilder.EFCore](#linqbuilder.efcore)
4. [Full Example](#full-example)

## LinqBuilder

### Usage
Specifications can be constructed in three different ways.

**By extending Specification:**
```csharp
public class FirstnameIsFoo : Specification<Person>
{
    public override Expression<Func<Person, bool>> AsExpression()
    {
        return person => person.Firstname == "Foo";
    }
}

ISpecification<Person> firstnameIsFoo = new FirstnameIsFoo();
```

**By extending DynamicSpecification:**
```csharp
public class FirstnameIs : DynamicSpecification<Person, string>
{
    public override Expression<Func<Person, bool>> AsExpression()
    {
        return person => person.Firstname == Value;
    }
}

ISpecification<Person> firstnameIsFoo = new FirstnameIs().Set("Foo");
```

**By extending MultiSpecification:**
```csharp
public class FirstnameIsFoo : MultiSpecification<Person, OtherPerson>
{
    public override Expression<Func<Person, bool>> AsExpressionForEntity1()
    {
        return person => person.Firstname == "Foo";
    }

    public override Expression<Func<OtherPerson, bool>> AsExpressionForEntity2()
    {
        return person => person.Firstname == "Foo";
    }
}

ISpecification<Person> firstnameIsFoo = new FirstnameIsFoo(); // First generic is default
ISpecification<Person> firstnameIsFoo = new FirstnameIsFoo().For<Person>();
ISpecification<OtherPerson> firstnameIsFoo = new FirstnameIsFoo().For<OtherPerson>();
```

**By static New method:**
```csharp
ISpecification<Person> firstnameIsFoo = Specification<Person>.New(p => p.Firstname == "Foo");
// Or by alias
ISpecification<Person> firstnameIsFoo = Spec<Person>.New(p => p.Firstname == "Foo");
```

### Example
```csharp
var collection = new List<Person>() { ... };

ISpecification<Person> firstnameIsFoo = Spec<Person>.New(p => p.Firstname == "Foo");
ISpecification<Person> firstnameIsBar = Spec<Person>.New(p => p.Firstname == "Bar");

ISpecification<Entity> specification = firstnameIsFoo.Or(firstnameIsBar);

var result = collection.ExeSpec(specification).ToList();
// result = Collection items satisfied by specification
```
The extension ```ExeSpec``` allows all types of ```ISpecification``` to be executed on ```IQueryable``` and ```IEnumerable```.

### Interfaces
```csharp
// All types of specifications implements this interface
public interface ISpecification<TEntity>
    where TEntity : class
{
    Configuration<TEntity> Internal { get; } // Returns the internal configuration object
}
```
```csharp
// Specification and DynamicSpecification implements this interface
public interface IQuerySpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class
{
    Expression<Func<TEntity, bool>> AsExpression();
    Func<TEntity, bool> AsFunc();
}
```

### Methods
```csharp
ISpecification<Entity> specification = Spec<Entity>.All(
    new SomeSpecification(),
    new SomeOtherSpecification(),
    ...
);

ISpecification<Entity> specification = Spec<Entity>.None(
    new SomeSpecification(),
    new SomeOtherSpecification(),
    ...
);

ISpecification<Entity> specification = Spec<Entity>.Any(
    new SomeSpecification(),
    new SomeOtherSpecification(),
    ...
);
```

### Extensions
```csharp
ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> left, ISpecification<TEntity> right);
ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> left, ISpecification<TEntity> right);
ISpecification<TEntity> Not<TEntity>(this ISpecification<TEntity> specification);
bool IsSatisfiedBy<TEntity>(this ISpecification<TEntity> specification, TEntity entity);
ISpecification<TEntity> Clone<TEntity>(this ISpecification<TEntity> specification);
```

**LinqBuilder** also extends the following extensions to support ```ISpecification``` on ```IQueryable``` and ```IEnumerable```.
```csharp
IEnumerable<Entity> collection = collection.Where(specification);
bool result = collection.Any(specification);
bool result = collection.All(specification);
int result = collection.Count(specification);
Entity result = collection.First(specification);
Entity result = collection.FirstOrDefault(specification);
Entity result = collection.Single(specification);
Entity result = collection.SingleOrDefault(specification);
```

## LinqBuilder.OrderBy

### Usage
Order specifications can be constructed in almost the same way as regular specifications.

**By extending OrderSpecification:**
```csharp
public class FirstnameDescending : OrderSpecification<Person, string>
{
    public DescNumberOrderSpecification() : base(Sort.Descending) { }

    public override Expression<Func<Person, string>> AsExpression()
    {
        return person => person.Firstname;
    }
}

ISpecification<Person> firstnameDescending = new FirstnameDescending();
```

**By static New method:**
```csharp
ISpecification<Person> firstnameDescending = OrderSpecification<Person, string>.New(p => p.Firstname, Sort.Descending);
// Or by alias
ISpecification<Person> firstnameDescending = OrderSpec<Person, string>.New(p => p.Firstname, Sort.Descending);
```

## Example
```csharp
var collection = new List<Person>() { ... };

ISpecification<Person> firstnameDescending = OrderSpec<Person, string>.New(p => p.Firstname, Sort.Descending);
ISpecification<Person> lastnameDescending = OrderSpec<Person, string>.New(p => p.Lastname, Sort.Descending);

ISpecification<Person> specification = firstnameDescending.ThenBy(lastnameDescending);

var result = collection.ExeSpec(specification).ToList();
// result = Collection ordered by descending number, then by other number
```

### Interfaces
```csharp
// OrderSpecification implements this interface
public interface IOrderSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class
{
    IOrderedQueryable<TEntity> InvokeSort(IQueryable<TEntity> query);
    IOrderedEnumerable<TEntity> InvokeSort(IEnumerable<TEntity> collection);
    IOrderedQueryable<TEntity> InvokeSort(IOrderedQueryable<TEntity> query);
    IOrderedEnumerable<TEntity> InvokeSort(IOrderedEnumerable<TEntity> collection);
}
```
```csharp
// Is used to properly specify when to allow ThenBy
public interface IOrderedSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class { }
```

### Methods
```csharp
ISpecification<Person> specification = OrderSpec<Person, string>.New(p => p.Firstname)
    .Take(10);

ISpecification<Person> specification = OrderSpec<Person, string>.New(p => p.Firstname)
    .Skip(5);

ISpecification<Person> specification = OrderSpec<Person, string>.New(p => p.Firstname)
    .Paginate(2, 10); // Equals .Skip((2 - 1) * 10).Take(10)
```

### Extensions
**LinqBuilder.OrderBy** extends the following extensions to support ```ISpecification``` on ```IQueryable``` and ```IEnumerable```.
```csharp
IOrderedEnumerable<Entity> collection = collection
    .OrderBy(specification);
    .ThenBy(otherSpecification);
```

It also extends regular LinqBuilder specifications to support chaining with ```OrderSpecification```'s.
```csharp
ISpecification<Person> firstnameIsFoo = Spec<Person>.New(p => p.Firstname == "Foo");
ISpecification<Person> firstnameAscending = OrderSpec<Person, string>.New(p => p.Firstname);

ISpecification<Entity> specification = firstnameIsFoo.OrderBy(firstnameAscending);
```

Chained ```OrderSpecification```'s can also be attatched to a specification later.
```csharp
ISpecification<Person> firstnameIsFoo = Spec<Person>.New(p => p.Firstname == "Foo");
ISpecification<Person> firstnameAscending = OrderSpec<Person, string>.New(p => p.Firstname);
ISpecification<Person> lastnameAscending = OrderSpec<Person, string>.New(p => p.Firstname);

ISpecification<Person> orderSpecification = firstnameAscending.ThenBy(lastnameAscending);

ISpecification<Person> specification = firstnameIsFoo.UseOrdering(orderSpecification);
```

The following extensions will help to check what kind of ordering is applied.
```csharp
ISpecification<Person> firstnameIsFoo = Spec<Person>.New(p => p.Firstname == "Foo");
ISpecification<Person> firstnameAscending = OrderSpec<Person, string>.New(p => p.Firstname);

firstnameIsFoo.IsOrdered(); // Returns false

ISpecification<Person> specification = firstnameIsFoo.OrderBy(firstnameAscending);
specification.IsOrdered(); // Returns true
```
```csharp
ISpecification<Person> specification = Spec<Person>.New(p => p.Firstname == "Foo");
specification.HasSkip(); // Returns false

ISpecification<Person> specification = specification.Skip(10);
specification.HasSkip(); // Returns true
```
```csharp
ISpecification<Person> specification = Spec<Person>.New(p => p.Firstname == "Foo");
specification.HasTake(); // Returns false

ISpecification<Person> specification = specification.Take(10);
specification.HasTake(); // Returns true
```

## LinqBuilder.EF6

### Extensions
**LinqBuilder.EF6** extends the following extensions to support ```ISpecification```.
```csharp
bool result = await _sampleContext.Entities.AnyAsync(specification);
bool result = await _sampleContext.Entities.AllAsync(specification);
int result = await _sampleContext.Entities.CountAsync(specification);
Entity result = await _sampleContext.Entities.FirstAsync(specification);
Entity result = await _sampleContext.Entities.FirstOrDefaultAsync(specification);
Entity result = await _sampleContext.Entities.SingleAsync(specification);
Entity result = await _sampleContext.Entities.SingleOrDefaultAsync(specification);
```

## LinqBuilder.EFCore
**LinqBuilder.EFCore** extends the following extensions to support ```ISpecification```.
```csharp
bool result = await _sampleContext.Entities.AnyAsync(specification);
bool result = await _sampleContext.Entities.AllAsync(specification);
int result = await _sampleContext.Entities.CountAsync(specification);
Entity result = await _sampleContext.Entities.FirstAsync(specification);
Entity result = await _sampleContext.Entities.FirstOrDefaultAsync(specification);
Entity result = await _sampleContext.Entities.SingleAsync(specification);
Entity result = await _sampleContext.Entities.SingleOrDefaultAsync(specification);
```

## Full example

```csharp
public class Person
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
}

public class SampleDbContext : DbContext // Simplified DbContext
{
    public virtual DbSet<Person> Persons { get; set; }
}

public class DbService<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public DbService(SampleDbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public int Count(ISpecification<TEntity> specification)
    {
        return _dbSet.Count(specification);
    }

    public List<Entity> Get(ISpecification<TEntity> specification)
    {
        return _dbSet.ExeSpec(specification).ToList();
    }

    public (List<Person> items, int count) GetAndCount(ISpecification<TEntity> specification)
    {
        return (Get(specification), Count(specification));
    }
}

ISpecification<Person> firstnameIsFoo = Spec<Person>.New(p => p.Firstname == "Foo");
ISpecification<Person> lastnameIsBar = Spec<Person>.New(p => p.Lastname == "Bar");
ISpecification<Person> idDescending = OrderSpec<Person, int>.New(p => p.Id, Sort.Descending);

ISpecification<Person> specification = firstnameIsFoo.And(lastnameIsBar)
    .OrderBy(idDescending)
    .Paginate(1, 5); // pageNo = 1, pageSize = 5

using (var context = new SampleDbContext())
{
    var result = new DbService<Person>(context).GetAndCount(specification);
    // result.items = Paginated list of Person's with name: Foo Bar
    // result.count = Total unpaginated result count
}
```
