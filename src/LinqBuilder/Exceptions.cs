using System;

namespace LinqBuilder
{
    public static class Exceptions
    {
        public static ArgumentNullException SpecificationCannotBeNull(string paramName)
        {
            return new ArgumentNullException(paramName, "Specification cannot be null!");
        }
    }
}
