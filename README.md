# LinqBuilder
[![Build status](https://ci.appveyor.com/api/projects/status/v0t8rfsv0d4q3hap?svg=true)](https://ci.appveyor.com/project/Baune8D/linqbuilder)
[![codecov](https://codecov.io/gh/Baune8D/linqbuilder/branch/master/graph/badge.svg)](https://codecov.io/gh/Baune8D/linqbuilder)

**Available on NuGet**  
**Dev feed:** [https://www.myget.org/F/baunegaard/api/v3/index.json](https://www.myget.org/F/baunegaard/api/v3/index.json)  
**Build feed:** [https://ci.appveyor.com/nuget/linqbuilder-uwyim2pji227](https://ci.appveyor.com/nuget/linqbuilder-uwyim2pji227)

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

var isFiveSpecification = new IsFiveSpecification();
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

var isFiveSpecification = new IsValueSpecification().Set(5);
```

**By static New method:**
```csharp
var isFiveSpecification = Specification<Entity>.New(entity => entity.Number == 5);
// Or by alias
var isFiveSpecification = Spec<Entity>.New(entity => entity.Number == 5);
```
<br/>

### Example
```csharp
public List<Entity> GetEntitiesWithNumberFiveOrSix()
{
    var specification = new IsValueSpecification().Set(5) // Dynamic specification
        .Or(new IsSixSpecification()); // Static specification

    return _sampleRepository.Get(specification);
}
```
<br/>

### Interfaces
```csharp
public interface ISpecificationQuery<TEntity>
{
    IQueryable<TEntity> Invoke(IQueryable<TEntity> query);
    IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection);
}
```
```csharp
public interface ISpecification<TEntity> : ISpecificationQuery<TEntity>
{
    IFilterSpecification<TEntity> And(IFilterSpecification<TEntity> specification);
    IFilterSpecification<TEntity> Or(IFilterSpecification<TEntity> specification);
    IFilterSpecification<TEntity> Not();
    bool IsSatisfiedBy(TEntity entity);
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

var descNumberOrderSpecification = new DescNumberOrderSpecification();
```

**By static New method:**
```csharp
var descNumberOrderSpecification = OrderSpecification<Entity, int>.New(entity => entity.Number, Sort.Descending);
// Or by alias
var descNumberOrderSpecification = OrderSpec<Entity, int>.New(entity => entity.Number, Sort.Descending);
```
<br/>

## Example
```csharp
public List<Entity> Get()
{
    var specification = new DescNumberOrderSpecification()
        .ThenBy(new OtherNumberOrderSpecification());

    return _sampleRepository.Get(specification);
}
```
<br/>

### Interfaces
```csharp
public interface IBaseOrderSpecification<TEntity> : ISpecificationQuery<TEntity>
{
    IOrderedSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> orderSpecification);
    IOrderedSpecification<TEntity> Skip(int count);
    IOrderedSpecification<TEntity> Take(int count);
    IOrderedSpecification<TEntity> Paginate(int pageNo, int pageSize);
}
```
```csharp
public interface IOrderedSpecification<TEntity> : IBaseOrderSpecification<TEntity>
{
    Ordering<TEntity> GetOrdering();
}
```
```csharp
public interface IOrderSpecification<TEntity> : IBaseOrderSpecification<TEntity>
{
    IOrderedQueryable<TEntity> InvokeSort(IQueryable<TEntity> query);
    IOrderedEnumerable<TEntity> InvokeSort(IEnumerable<TEntity> collection);
    IOrderedQueryable<TEntity> InvokeSort(IOrderedQueryable<TEntity> query);
    IOrderedEnumerable<TEntity> InvokeSort(IOrderedEnumerable<TEntity> collection);
}
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
IOrderedSpecification<Entity> specification = new IsFiveSpecification()
    .OrderBy(new DescNumberOrderSpecification());

IQueryable<Entity> query = _sampleContext.Entities.ExeQuery(specification);
```
**Note** that chained specifications will not work with the regular LINQ extensions.  
Use the "ExeQuery" extension instead.

Chained OrderSpecifications can also be attatched to a specification later.
```csharp
IOrderedSpecification<Entity> orderSpecification = new DescNumberOrderSpecification();
    .ThenBy(new OtherNumberOrderSpecification());

IOrderedSpecification<Entity> specification = new IsFiveSpecification()
    .UseOrdering(orderSpecification);
```

The following extensions help with differentiate regular specifications from ordered specifications.
```csharp
ISpecificationQuery<Entity> specification = new IsFiveSpecification();
specification.IsOrdered(); // Returns false
specification.AsOrdered(); // Returns null

ISpecificationQuery<Entity> specification = specification
    .OrderBy(new DescNumberOrderSpecification());
specification.IsOrdered(); // Returns true
specification.AsOrdered(); // Returns IOrderedSpecification<Entity>
```
<br/>

### Methods
```csharp
new DescNumberOrderSpecification();
    .Paginate(2, 10); // Equals .Skip((2 - 1) * 10).Take(10)

new DescNumberOrderSpecification()
    .ThenBy(new OtherNumberOrderSpecification())
    .GetOrdering(); // Returns object containing ordering configuration
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
    private readonly SampleRepository _sampleRepository;

    public SampleService(SampleRepository sampleRepository)
    {
        _sampleRepository = sampleRepository;
    }

    public List<Entity> GetEntitiesWithNumberFiveOrSix(int pageNumber = 1, int pageSize = 10)
    {
        var filter = new IsFiveSpecification()
            .Or(new IsSixSpecification())
            .OrderBy(new NumberOrderSpecification())
            .Paginate(pageNumber, pageSize);

        return _sampleRepository.Get(filter);
    }
}

public class SampleRepository
{
    private readonly SampleDbContext _context;

    public SampleRepository(SampleDbContext context)
    {
        _context = context;
    }

    public List<Entity> Get(ISpecificationQuery<Entity> query)
    {
        return _context.Entities.ExeQuery(query).ToList();
    }
}
```
