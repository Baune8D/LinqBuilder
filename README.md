# LinqBuilder
[![Build status](https://ci.appveyor.com/api/projects/status/v0t8rfsv0d4q3hap?svg=true)](https://ci.appveyor.com/project/Baune8D/linqbuilder)
[![codecov](https://codecov.io/gh/Baune8D/linqbuilder/branch/master/graph/badge.svg)](https://codecov.io/gh/Baune8D/linqbuilder)

**Available on NuGet:** [https://www.nuget.org/packages?q=linqbuilder](https://www.nuget.org/packages?q=linqbuilder)  
**Dev feed:** [https://www.myget.org/F/baunegaard/api/v3/index.json](https://www.myget.org/F/baunegaard/api/v3/index.json)  

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
<br/>

### Example
```csharp
public List<Entity> GetEntitiesWithNumberFiveOrSix()
{
    ISpecification<Entity> specification = new IsValueSpecification().Set(5) // Dynamic specification
        .Or(new IsSixSpecification()); // Static specification

    return _context.Entities.Where(specification).ToList();
}
```
<br/>

### Interfaces
```csharp
public interface ISpecification<TEntity>
    where TEntity : class
{
    Configuration<TEntity> Internal { get; } // Returns the internal configuration object
}
```
```csharp
public interface IQuerySpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class
{
    Expression<Func<TEntity, bool>> AsExpression();
    Func<TEntity, bool> AsFunc();
}
```
<br/>

### Extensions
**LinqBuilder** extends the following LINQ extensions to support specifications.
```csharp
IQueryable<Entity> query = _sampleContext.Entities.Where(specification);
bool result = _sampleContext.Entities.Any(specification);
bool result = _sampleContext.Entities.All(specification);
int result = _sampleContext.Entities.Count(specification);
Entity result = _sampleContext.Entities.First(specification);
Entity result = _sampleContext.Entities.FirstOrDefault(specification);
Entity result = _sampleContext.Entities.Single(specification);
Entity result = _sampleContext.Entities.SingleOrDefault(specification);
```
<br/>

### Methods
```csharp
ISpecification<Entity> specification = Spec<Entity>.All(
    new SomeSpecification(),
    new SomeOtherSpecification();
);

ISpecification<Entity> specification = Spec<Entity>.None(
    new SomeSpecification(),
    new SomeOtherSpecification();
);

ISpecification<Entity> specification = Spec<Entity>.Any(
    new SomeSpecification(),
    new SomeOtherSpecification();
);
```
<br/>

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
<br/>

## Example
```csharp
public List<Entity> Get()
{
    ISpecification<Entity> specification = new DescNumberOrderSpecification()
        .ThenBy(new OtherNumberOrderSpecification());

    return _context.Entities.ExeQuery(specification).ToList();
}
```
<br/>

### Interfaces
```csharp
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
public interface IOrderedSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class { } // Only exist to filter some extension methods
```
<br/>

### Extensions
**LinqBuilder.OrderBy** extends the following LINQ extensions to support order specifications.
```csharp
IOrderedQueryable<Entity> query = _sampleContext.Entities
    .OrderBy(specification);
    .ThenBy(otherSpecification);
```

It also extends regular specifications to support chaining with order specifications.
```csharp
ISpecification<Entity> specification = new IsFiveSpecification()
    .OrderBy(new DescNumberOrderSpecification());

IQueryable<Entity> query = _sampleContext.Entities.ExeQuery(specification);
```
**Note** that chained specifications will not work with the regular LINQ extensions.  
Use the "ExeQuery" extension instead.

Chained OrderSpecifications can also be attatched to a specification later.
```csharp
ISpecification<Entity> orderSpecification = new DescNumberOrderSpecification();
    .ThenBy(new OtherNumberOrderSpecification());

ISpecification<Entity> specification = new IsFiveSpecification()
    .UseOrdering(orderSpecification);
```

The following extension helps to differentiate regular specifications from ordered specifications.
```csharp
ISpecification<Entity> specification = new IsFiveSpecification();
specification.IsOrdered(); // Returns false

ISpecification<Entity> specification = specification
    .OrderBy(new DescNumberOrderSpecification());
specification.IsOrdered(); // Returns true
```
And the following will help to check what kind of filtering is applied.
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
<br/>

### Methods
```csharp
ISpecification specification = new DescNumberOrderSpecification()
    .Take(10);

ISpecification specification = new DescNumberOrderSpecification()
    .Skip(5);

ISpecification specification = new DescNumberOrderSpecification()
    .Paginate(2, 10); // Equals .Skip((2 - 1) * 10).Take(10)
```
<br/>

## LinqBuilder.EF6

### Extensions
**LinqBuilder.EF6** extends the following LINQ extensions to support specifications.
```csharp
bool result = await _sampleContext.Entities.AnyAsync(specification);
bool result = await _sampleContext.Entities.AllAsync(specification);
int result = await _sampleContext.Entities.CountAsync(specification);
Entity result = await _sampleContext.Entities.FirstAsync(specification);
Entity result = await _sampleContext.Entities.FirstOrDefaultAsync(specification);
Entity result = await _sampleContext.Entities.SingleAsync(specification);
Entity result = await _sampleContext.Entities.SingleOrDefaultAsync(specification);
```
<br/>

## LinqBuilder.EFCore
**LinqBuilder.EFCore** extends the following LINQ extensions to support specifications.
```csharp
bool result = await _sampleContext.Entities.AnyAsync(specification);
bool result = await _sampleContext.Entities.AllAsync(specification);
int result = await _sampleContext.Entities.CountAsync(specification);
Entity result = await _sampleContext.Entities.FirstAsync(specification);
Entity result = await _sampleContext.Entities.FirstOrDefaultAsync(specification);
Entity result = await _sampleContext.Entities.SingleAsync(specification);
Entity result = await _sampleContext.Entities.SingleOrDefaultAsync(specification);
```
<br/>

## Full example

```csharp
public class SampleService
{
    private readonly _context = context;

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
        return _context.Entities.ExeQuery(specification).ToList();
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
