using System;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public abstract class DynamicSpecification<TEntity, TValue> : BaseSpecification<TEntity>
        where TEntity : class
    {
        public TValue Value { get; private set; }

        public ISpecification<TEntity> Set(TValue value)
        {
            Value = value;
            return this;
        }

        public abstract override Expression<Func<TEntity, bool>> AsExpression();
    }

    public abstract class DynamicSpecification<TEntity, TValue1, TValue2> : BaseSpecification<TEntity>
        where TEntity : class
    {
        public TValue1 Value1 { get; private set; }
        public TValue2 Value2 { get; private set; }

        public ISpecification<TEntity> Set(TValue1 value1, TValue2 value2)
        {
            Value1 = value1;
            Value2 = value2;
            return this;
        }

        public abstract override Expression<Func<TEntity, bool>> AsExpression();
    }

    public abstract class DynamicSpecification<TEntity, TValue1, TValue2, TValue3> : DynamicSpecification<TEntity, TValue1, TValue2>
        where TEntity : class
    {
        public TValue3 Value3 { get; private set; }

        public ISpecification<TEntity> Set(TValue1 value1, TValue2 value2, TValue3 value3)
        {
            Set(value1, value2);
            Value3 = value3;
            return this;
        }
    }

    public abstract class DynamicSpecification<TEntity, TValue1, TValue2, TValue3, TValue4> : DynamicSpecification<TEntity, TValue1, TValue2, TValue3>
        where TEntity : class
    {
        public TValue4 Value4 { get; private set; }

        public ISpecification<TEntity> Set(TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4)
        {
            Set(value1, value2, value3);
            Value4 = value4;
            return this;
        }
    }
}
