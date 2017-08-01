# LinqBuilder
[![Build status](https://ci.appveyor.com/api/projects/status/2hw2qd46rwere3ef?svg=true)](https://ci.appveyor.com/project/Baune8D/linqbuilder)
[![codecov](https://codecov.io/gh/Baune8D/linqbuilder/branch/master/graph/badge.svg)](https://codecov.io/gh/Baune8D/linqbuilder)

NuGet feed: [https://www.myget.org/F/baunegaard/api/v3/index.json](https://www.myget.org/F/baunegaard/api/v3/index.json)

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
public class SampleService : ISampleService
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

### Interface
```csharp
public interface ISpecification<T>
{
    ISpecification<T> And(ISpecification<T> other);
    ISpecification<T> Or(ISpecification<T> other);
    ISpecification<T> Not();
    ISpecification<T> Skip(int count);
    ISpecification<T> Take(int count);
    IQueryable<T> Invoke(IQueryable<T> query);
    IEnumerable<T> Invoke(IEnumerable<T> collection);
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> AsExpression();
}
```

### Extensions
LinqBuilder.Specifications extends the "Where" LINQ extension to support specifications.
```csharp
IQueryable<Entity> query = _sampleContext.Entities.Where(specification);
```

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
public class SampleService : ISampleService
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
public interface IOrderSpecification<T>
{
    ThenBySpecification<T> ThenBy(IOrderSpecification<T> order);
    IOrderedQueryable<T> Invoke(IQueryable<T> query);
    IOrderedQueryable<T> Invoke(IOrderedQueryable<T> query);
    IOrderedEnumerable<T> Invoke(IEnumerable<T> collection);
    IOrderedEnumerable<T> Invoke(IOrderedEnumerable<T> collection);
}
```

### Extensions
LinqBuilder.OrderSpecifications extends the "OrderBy" and "ThenBy" LINQ extensions to support specifications.
```csharp
IOrderedQueryable<Entity> query = _sampleContext.Entities.OrderBy(specification);
IOrderedQueryable<Entity> otherQuery = query.ThenBy(otherSpecification);
```

## Full example

```csharp
public class SampleService : ISampleService
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
            .Skip(skip).Take(take);

        var order = new NumberOrderSpecification(Order.Descending)
            .ThenBy(new OtherNumberOrderSpecification(Order.Descending));

        return await _sampleRepository.GetAsync(filter, order);
    }
}

public class SampleRepository : ISampleRepository
{
    private readonly SampleDbContext _context;

    public SampleRepository(SampleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Entity>> GetAsync(ISpecification<Entity> filter, IOrderSpecification<Entity> order)
    {
        return await _context.Entities.Where(filter).OrderBy(order).ToListAsync();
    }
}
```