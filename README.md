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

    public async Task<List<Entity>> GetFiveOrSixAsync(int take = int.MaxValue)
    {
        var filter = new IsFiveSpecification()
            .Or(new IsSixSpecification())
            .Take(take);

        return await _sampleRepository.GetAsync(filter);
    }
}
```

## LinqBuilder.OrderSpecifications

### Example
```csharp
public class OrderByNumberSpecification : OrderBySpecification<Entity>
{
    public OrderByNumberSpecification(Order direction = Order.Ascending)
        : base(direction) { }

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

    public async Task<List<Entity>> GetFiveOrSixAsync(int take = int.MaxValue)
    {
        var order = new OrderByNumberSpecification(Order.Descending)
            .ThenBy(new OrderByOtherNumberSpecification(Order.Descending));

        return await _sampleRepository.GetAsync(order);
    }
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
    private readonly DbContext _context;

    public SampleRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<List<Entity>> GetAsync(ICompositeSpecification<Advert> filter, IOrderSpecification<Advert> order)
    {
        return await order.Invoke(filter.Invoke(_context.Entities)).ToListAsync();
    }
}
```