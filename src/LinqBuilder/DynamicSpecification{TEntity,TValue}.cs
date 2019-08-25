namespace LinqBuilder
{
    public abstract class DynamicSpecification<TEntity, TValue> : QuerySpecification<TEntity>, IDynamicSpecification<TEntity, TValue>
        where TEntity : class
    {
        protected DynamicSpecification()
        {
        }

        protected DynamicSpecification(TValue value)
        {
            Set(value);
        }

        public TValue Value { get; private set; }

        public IDynamicSpecification<TEntity, TValue> Set(TValue value)
        {
            Value = value;
            return this;
        }
    }
}
