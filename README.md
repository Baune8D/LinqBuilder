# LinqBuilder
[![Build status](https://ci.appveyor.com/api/projects/status/xergduelkce5icm4?svg=true)](https://ci.appveyor.com/project/baunegaard/linqbuilder)  
NuGet feed: [https://www.myget.org/F/baunegaard/api/v3/index.json](https://www.myget.org/F/baunegaard/api/v3/index.json)

## LinqBuilder.Specifications

### Example
```csharp
public class IsFiveSpecification : CompositeSpecification<Entity>
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

### Interface
```csharp
public interface ICompositeSpecification<T>
{
    ICompositeSpecification<T> And(ICompositeSpecification<T> other);
    ICompositeSpecification<T> Or(ICompositeSpecification<T> other);
    ICompositeSpecification<T> Not();
    ICompositeSpecification<T> Skip(int count);
    ICompositeSpecification<T> Take(int count);
    IQueryable<T> Invoke(IQueryable<T> query);
    IEnumerable<T> Invoke(IEnumerable<T> collection);
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> AsExpression();
}
```

## LinqBuilder.OrderSpecifications

### Example
```csharp
public class OrderByNumberSpecification : OrderBySpecification<Entity>
{
    public OrderByNumberSpecification(Order order = Order.Ascending)
        : base(order) { }

    public override Expression<Func<Entity, IComparable>> OrderExpression()
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
        var order = new OrderByNumberSpecification(Order.Descending)
            .ThenBy(new OrderByOtherNumberSpecification(Order.Descending));

        return await _sampleRepository.GetAsync(order);
    }
}
```

### Interfaces
```csharp
public interface IOrderBySpecification<T>
{
    Order Order { get; set; }
    ThenBySpecification<T> ThenBy(IOrderBySpecification<T> other);
    Expression<Func<T, IComparable>> AsExpression();
}
```

```csharp
public interface IOrderSpecification<T>
{
    IOrderedQueryable<T> Invoke(IQueryable<T> query);
    IOrderedEnumerable<T> Invoke(IEnumerable<T> collection);
}
```

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
            .Skip(skip)
            .Take(take);

        var order = new OrderByNumberSpecification(Order.Descending)
            .ThenBy(new OrderByOtherNumberSpecification(Order.Descending));

        return await _sampleRepository.GetAsync(filter, order);
    }
}

public class SampleRepository
{
    private readonly SampleDbContext _context;

    public SampleRepository(SampleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Entity>> GetAsync(ICompositeSpecification<Entity> filter, IOrderSpecification<Entity> order)
    {
        return await order.Invoke(filter.Invoke(_context.Entities)).ToListAsync();
    }
}
```