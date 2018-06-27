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
public class IsFiveSpecification : Specification<Entity>
{
    public override Expression<Func<Entity, bool>> AsExpression()
    {
        return entity => entity.Number == 5;
    }
}

ISpecification<Entity> isFiveSpecification = new IsFiveSpecification();
```

**By extending DynamicSpecification:**
```csharp
public class IsValueSpecification : DynamicSpecification<Entity, int>
{
    public override Expression<Func<Entity, bool>> AsExpression()
    {
        return entity => entity.Number == Value;
    }
}

ISpecification<Entity> isFiveSpecification = new IsValueSpecification().Set(5);
```

**By static New method:**
```csharp
ISpecification<Entity> isFiveSpecification = Specification<Entity>.New(entity => entity.Number == 5);
// Or by alias
ISpecification<Entity> isFiveSpecification = Spec<Entity>.New(entity => entity.Number == 5);
```

### Example
```csharp
var collection = new List<Entity>() { ... };

ISpecification<Entity> specification = new IsFiveSpecification()
    .Or(new IsSixSpecification());

var result = collection.ExeSpec(specification).ToList();
// result = collection items satisfied by specification
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
**LinqBuilder** extends the following extensions to support ```ISpecification``` on ```IQueryable``` and ```IEnumerable```.
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
public class DescNumberOrderSpecification : OrderSpecification<Entity, int>
{
    public DescNumberOrderSpecification() : base(Sort.Descending) { }

    public override Expression<Func<Entity, int>> AsExpression()
    {
        return entity => entity.Number;
    }
}

ISpecification<Entity> descNumberOrderSpecification = new DescNumberOrderSpecification();
```

**By static New method:**
```csharp
ISpecification<Entity> descNumberOrderSpecification = OrderSpecification<Entity, int>.New(entity => entity.Number, Sort.Descending);
// Or by alias
ISpecification<Entity> descNumberOrderSpecification = OrderSpec<Entity, int>.New(entity => entity.Number, Sort.Descending);
```

## Example
```csharp
var collection = new List<Entity>() { ... };

ISpecification<Entity> specification = new DescNumberOrderSpecification()
    .ThenBy(new OtherNumberOrderSpecification());

var result = collection.ExeSpec(specification).ToList();
// result = collection ordered by descending number, then by other number
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
ISpecification specification = new DescNumberOrderSpecification()
    .Take(10);

ISpecification specification = new DescNumberOrderSpecification()
    .Skip(5);

ISpecification specification = new DescNumberOrderSpecification()
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
ISpecification<Entity> specification = new IsFiveSpecification()
    .OrderBy(new DescNumberOrderSpecification());
```

Chained ```OrderSpecification```'s can also be attatched to a specification later.
```csharp
ISpecification<Entity> orderSpecification = new DescNumberOrderSpecification();
    .ThenBy(new OtherNumberOrderSpecification());

ISpecification<Entity> specification = new IsFiveSpecification()
    .UseOrdering(orderSpecification);
```

The following extensions will help to check what kind of ordering is applied.
```csharp
ISpecification<Entity> specification = new IsFiveSpecification();
specification.IsOrdered(); // Returns false

ISpecification<Entity> specification = specification.OrderBy(new DescNumberOrderSpecification());
specification.IsOrdered(); // Returns true
```
```csharp
ISpecification<Entity> specification = new IsFiveSpecification();
specification.HasSkip(); // Returns false

ISpecification<Entity> specification = specification.Skip(10);
specification.HasSkip(); // Returns true
```
```csharp
ISpecification<Entity> specification = new IsFiveSpecification();
specification.HasTake(); // Returns false

ISpecification<Entity> specification = specification.Take(10);
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
public class SampleService
{
    private readonly SampleDbContext _context;

    public SampleService(SampleDbContext context)
    {
        _context = context;
    }

    public int Count(ISpecification<Entity> specification)
    {
        return _context.Entities.Count(specification);
    }

    public List<Entity> Get(ISpecification<Entity> specification)
    {
        return _context.Entities.ExeSpec(specification).ToList();
    }

    public (List<Entity> items, int count) GetAndCount(ISpecification<Entity> specification)
    {
        return (Get(specification), Count(specification));
    }
}

ISpecification<Entity> specification = new SomeValueSpecification()
    .And(new SomeOtherValueSpecification())
    .OrderBy(new IdOrderSpecification())
    .Paginate(1, 10);

var result = _sampleService.GetAndCount(specification);
// result.items = Paginated list of items
// result.count = Total unpaginated result count
```
