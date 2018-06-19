namespace LinqBuilder
{
    public abstract class DynamicSpecification<TEntity, TValue> : QuerySpecificationBase<TEntity>, IDynamicSpecification<TEntity, TValue>
        where TEntity : class
    {
        public TValue Value { get; private set; }

        protected DynamicSpecification() { }

        protected DynamicSpecification(TValue value)
        {
            Set(value);
        }

        public IDynamicSpecification<TEntity, TValue> Set(TValue value)
        {
            Value = value;
            return this;
        }
    }

    public abstract class DynamicSpecification<TEntity, TValue1, TValue2> : QuerySpecificationBase<TEntity>, IDynamicSpecification<TEntity, TValue1, TValue2>
        where TEntity : class
    {
        public TValue1 Value1 { get; private set; }
        public TValue2 Value2 { get; private set; }

        protected DynamicSpecification() { }

        protected DynamicSpecification(TValue1 value1, TValue2 value2)
        {
            Set(value1, value2);
        }

        public IDynamicSpecification<TEntity, TValue1, TValue2> Set(TValue1 value1, TValue2 value2)
        {
            Value1 = value1;
            Value2 = value2;
            return this;
        }
    }

    public abstract class DynamicSpecification<TEntity, TValue1, TValue2, TValue3> : DynamicSpecification<TEntity, TValue1, TValue2>, IDynamicSpecification<TEntity, TValue1, TValue2, TValue3>
        where TEntity : class
    {
        public TValue3 Value3 { get; private set; }

        protected DynamicSpecification() { }

        protected DynamicSpecification(TValue1 value1, TValue2 value2, TValue3 value3) : base(value1, value2)
        {
            Value3 = value3;
        }

        public IDynamicSpecification<TEntity, TValue1, TValue2, TValue3> Set(TValue1 value1, TValue2 value2, TValue3 value3)
        {
            Set(value1, value2);
            Value3 = value3;
            return this;
        }
    }

    public abstract class DynamicSpecification<TEntity, TValue1, TValue2, TValue3, TValue4> : DynamicSpecification<TEntity, TValue1, TValue2, TValue3>, IDynamicSpecification<TEntity, TValue1, TValue2, TValue3, TValue4>
        where TEntity : class
    {
        public TValue4 Value4 { get; private set; }

        protected DynamicSpecification() { }

        protected DynamicSpecification(TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4) : base(value1, value2, value3)
        {
            Value4 = value4;
        }

        public IDynamicSpecification<TEntity, TValue1, TValue2, TValue3, TValue4> Set(TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4)
        {
            Set(value1, value2, value3);
            Value4 = value4;
            return this;
        }
    }
}
