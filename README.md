# LinqBuilder
[![Build status](https://ci.appveyor.com/api/projects/status/v0t8rfsv0d4q3hap?svg=true)](https://ci.appveyor.com/project/Baune8D/linqbuilder)
[![codecov](https://codecov.io/gh/Baune8D/linqbuilder/branch/master/graph/badge.svg)](https://codecov.io/gh/Baune8D/linqbuilder)
[![NuGet Badge](https://buildstats.info/nuget/LinqBuilder)](https://www.nuget.org/packages/LinqBuilder)

**Available on NuGet:** [https://www.nuget.org/packages/LinqBuilder/](https://www.nuget.org/packages/LinqBuilder/)  
**MyGet development feed:** [https://www.myget.org/F/baunegaard/api/v3/index.json](https://www.myget.org/F/baunegaard/api/v3/index.json)  

LinqBuilder is based on the specification pattern.

## Table of Contents
1. [LinqBuilder Specifications](#linqbuilder-specifications)
2. [LinqBuilder OrderSpecifications](#linqbuilder-orderspecifications)
3. [LinqBuilder.EFCore / LinqBuilder.EF6](#linqbuilderefcore--linqbuilderef6)
4. [LinqBuilder.EFCore.AutoMapper / LinqBuilder.EF6.AutoMapper](#linqbuilderefcoreautomapper--linqbuilderef6automapper)
5. [Full Example](#full-example)

## LinqBuilder Specifications

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

## LinqBuilder OrderSpecifications

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

### Example
```csharp
var collection = new List<Person>() { ... };

ISpecification<Person> firstnameDescending = OrderSpec<Person, string>.New(p => p.Firstname, Sort.Descending);
ISpecification<Person> lastnameDescending = OrderSpec<Person, string>.New(p => p.Lastname, Sort.Descending);

ISpecification<Person> specification = firstnameDescending.ThenBy(lastnameDescending);

var result = collection.ExeSpec(specification).ToList();
// result = Collection ordered by descending number, then by other number
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

## LinqBuilder.EFCore / LinqBuilder.EF6
| Package            | Version                                                                                                               |
| -------------------|:---------------------------------------------------------------------------------------------------------------------:|
| LinqBuilder.EFCore | [![NuGet Badge](https://buildstats.info/nuget/LinqBuilder.EFCore)](https://www.nuget.org/packages/LinqBuilder.EFCore) |
| LinqBuilder.EF6    | [![NuGet Badge](https://buildstats.info/nuget/LinqBuilder.EF6)](https://www.nuget.org/packages/LinqBuilder.EF6)       |

### Extensions
**LinqBuilder.EF** packages extends the following extensions to support ```ISpecification```.
```csharp
bool result = await _sampleContext.Entities.AnyAsync(specification);
bool result = await _sampleContext.Entities.AllAsync(specification);
int result = await _sampleContext.Entities.CountAsync(specification);
Entity result = await _sampleContext.Entities.FirstAsync(specification);
Entity result = await _sampleContext.Entities.FirstOrDefaultAsync(specification);
Entity result = await _sampleContext.Entities.SingleAsync(specification);
Entity result = await _sampleContext.Entities.SingleOrDefaultAsync(specification);
```

## LinqBuilder.EFCore.AutoMapper / LinqBuilder.EF6.AutoMapper
| Package                       | Version                                                                                                                                     |
| ------------------------------|:-------------------------------------------------------------------------------------------------------------------------------------------:|
| LinqBuilder.EFCore.AutoMapper | [![NuGet Badge](https://buildstats.info/nuget/LinqBuilder.EFCore.AutoMapper)](https://www.nuget.org/packages/LinqBuilder.EFCore.AutoMapper) |
| LinqBuilder.EF6.AutoMapper    | [![NuGet Badge](https://buildstats.info/nuget/LinqBuilder.EF6.AutoMapper)](https://www.nuget.org/packages/LinqBuilder.EF6.AutoMapper)       |

### Extensions
These packages extends the following extensions to support projected ```ISpecification```.
```csharp
bool result = await _sampleContext.Entities.AnyAsync(specification, _mapperConfig);
bool result = await _sampleContext.Entities.AllAsync(specification, _mapperConfig);
int result = await _sampleContext.Entities.CountAsync(specification, _mapperConfig);
ProjectedEntity result = await _sampleContext.Entities.FirstAsync(specification, _mapperConfig);
ProjectedEntity result = await _sampleContext.Entities.FirstOrDefaultAsync(specification, _mapperConfig);
ProjectedEntity result = await _sampleContext.Entities.SingleAsync(specification, _mapperConfig);
ProjectedEntity result = await _sampleContext.Entities.SingleOrDefaultAsync(specification, _mapperConfig);
IQueryable<ProjectedEntity> result = await _sampleContext.Entities.ExeSpec(specification, _mapperConfig);
```

### Example
```csharp
public class Entity
{
    public int Id { get; set; }
}

[AutoMap(typeof(Entity))]
public class ProjectedEntity
{
    public int Id { get; set; }
}

public class Repository
{
    private readonly DbSet<Entity> _dbSet;
    private readonly IConfigurationProvider _mapperConfig;

    public DbService(SampleDbContext context, IMapper mapper)
    {
        _dbSet = context.Set<Entity>();
        _mapperConfig = mapper.ConfigurationProvider;
    }

    public List<ProjectedEntity> Get(ISpecification<ProjectedEntity> specification)
    {
        return _dbSet.ExeSpec(specification, _mapperConfig).ToList();
    }
}
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
