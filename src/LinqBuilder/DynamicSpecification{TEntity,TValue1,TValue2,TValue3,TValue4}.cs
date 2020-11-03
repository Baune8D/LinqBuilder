namespace LinqBuilder
{
    public abstract class DynamicSpecification<TEntity, TValue1, TValue2, TValue3, TValue4>
        : DynamicSpecification<TEntity, TValue1, TValue2, TValue3>, IDynamicSpecification<TEntity, TValue1, TValue2, TValue3, TValue4>
        where TEntity : class
    {
        protected DynamicSpecification()
        {
        }

        protected DynamicSpecification(TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4)
            : base(value1, value2, value3)
        {
            Value4 = value4;
        }

        public TValue4 Value4 { get; private set; } = default!;

        public IDynamicSpecification<TEntity, TValue1, TValue2, TValue3, TValue4> Set(TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4)
        {
            Set(value1, value2, value3);
            Value4 = value4;
            return this;
        }
    }
}
