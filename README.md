# LinqBuilder
[![Build status](https://ci.appveyor.com/api/projects/status/2hw2qd46rwere3ef?svg=true)](https://ci.appveyor.com/project/Baune8D/linqbuilder)
[![codecov](https://codecov.io/gh/Baune8D/linqbuilder/branch/master/graph/badge.svg)](https://codecov.io/gh/Baune8D/linqbuilder)

NuGet dev feed: [https://www.myget.org/F/baunegaard/api/v3/index.json](https://www.myget.org/F/baunegaard/api/v3/index.json)


## LinqBuilder.Specifications

### Example
```csharp
public class IsFiveSpecification : Specification<Entity>
{
    public override Expression<Func<Entity, bool>> AsExpression()
    {
        return entity => entity.Number == 5;
    }
}
```

### Usage
```csharp
public class SampleService
{
    private readonly ISampleRepository _sampleRepository;

    public SampleService(ISampleRepository sampleRepository)
    {
        _sampleRepository = sampleRepository;
    }

    public async Task<List<Entity>> GetEntitiesWithNumberFiveOrSixAsync()
    {
        var filter = new IsFiveSpecification()
            .Or(new IsSixSpecification());

        return await _sampleRepository.GetAsync(filter);
    }
}
```

### Interfaces
```csharp
public interface ISpecification<T>
{
    IQueryable<T> Invoke(IQueryable<T> query);
    IEnumerable<T> Invoke(IEnumerable<T> collection);
}
```
```csharp
public interface IFilterSpecification<T> : ISpecification<T>
{
    IFilterSpecification<T> And(IFilterSpecification<T> other);
    IFilterSpecification<T> Or(IFilterSpecification<T> other);
    IFilterSpecification<T> Not();
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> AsExpression();
}
```

### Extensions
**LinqBuilder.Specifications** extends the "Where" LINQ extension to support IFilterSpecification.
```csharp
IQueryable<Entity> query = _sampleContext.Entities.Where(specification);
```
<br/>

## LinqBuilder.OrderSpecifications

### Example
```csharp
public class NumberOrderSpecification : OrderSpecification<Entity>
{
    public OrderByNumberSpecification(Order order = Order.Ascending)
        : base(order) { }

    public override Expression<Func<Entity, IComparable>> AsExpression()
    {
        return entity => entity.Number;
    }
}
```

### Usage
```csharp
public class SampleService
{
    private readonly ISampleRepository _sampleRepository;

    public SampleService(ISampleRepository sampleRepository)
    {
        _sampleRepository = sampleRepository;
    }

    public async Task<List<Entity>> GetAsync()
    {
        var order = new NumberOrderSpecification(Order.Descending)
            .ThenBy(new OtherNumberOrderSpecification(Order.Descending));

        return await _sampleRepository.GetAsync(order);
    }
}
```

### Interfaces
```csharp
public interface IBaseOrderSpecification<T> : ISpecification<T>
{
    ICompositeOrderSpecification<T> ThenBy(IOrderSpecification<T> other);
    ICompositeOrderSpecification<T> Skip(int count);
    ICompositeOrderSpecification<T> Take(int count);
}
```
```csharp
public interface IOrderSpecification<T> : IBaseOrderSpecification<T>
{
    IOrderedQueryable<T> InvokeOrdered(IOrderedQueryable<T> query);
    IOrderedQueryable<T> InvokeOrdered(IQueryable<T> query);
    IOrderedEnumerable<T> InvokeOrdered(IOrderedEnumerable<T> collection);
    IOrderedEnumerable<T> InvokeOrdered(IEnumerable<T> collection);
}
```
```csharp
public interface ICompositeOrderSpecification<T> : IBaseOrderSpecification<T> { }
```

### Extensions
**LinqBuilder.OrderSpecifications** extends the "OrderBy" and "ThenBy" LINQ extensions to support OrderSpecification\<T\>.
```csharp
IOrderedQueryable<Entity> query = _sampleContext.Entities.OrderBy(specification);
IOrderedQueryable<Entity> otherQuery = query.ThenBy(otherSpecification);
```

It also extends regular filter specifications to support chaining with order specifications.
```csharp
var specification = new Value1Specification(5) // From LinqBuilder.Specifications
    .OrderBy(new Value1OrderSpecification()); // From LinqBuilder.OrderSpecifications

IQueryable<Entity> query = _sampleContext.Entities.ExeSpec(specification);
```
**Note** that chained specifications will not work with the regular LINQ extensions.  
We have to use the "ExeSpec" extension instead.

## Full example

```csharp
public class SampleService
{
    private readonly ISampleRepository _sampleRepository;

    public SampleService(ISampleRepository sampleRepository)
    {
        _sampleRepository = sampleRepository;
    }

    public async Task<List<Entity>> GetEntitiesWithNumberFiveOrSixAsync(int skip = 0, int take = int.MaxValue)
    {
        var filter = new IsFiveSpecification()
            .Or(new IsSixSpecification())
            .OrderBy(new NumberOrderSpecification(Order.Descending))
            .Skip(skip).Take(take);

        return await _sampleRepository.GetAsync(filter);
    }
}

public class SampleRepository : ISampleRepository
{
    private readonly SampleDbContext _context;

    public SampleRepository(SampleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Entity>> GetAsync(ISpecification<Entity> specification)
    {
        return await _context.Entities.ExeSpec(specification).ToListAsync();
    }
}
```