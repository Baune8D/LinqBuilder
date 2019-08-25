namespace LinqBuilder
{
    public abstract class DynamicSpecification<TEntity, TValue1, TValue2> : QuerySpecification<TEntity>, IDynamicSpecification<TEntity, TValue1, TValue2>
        where TEntity : class
    {
        protected DynamicSpecification()
        {
        }

        protected DynamicSpecification(TValue1 value1, TValue2 value2)
        {
            Set(value1, value2);
        }

        public TValue1 Value1 { get; private set; }

        public TValue2 Value2 { get; private set; }

        public IDynamicSpecification<TEntity, TValue1, TValue2> Set(TValue1 value1, TValue2 value2)
        {
            Value1 = value1;
            Value2 = value2;
            return this;
        }
    }
}
