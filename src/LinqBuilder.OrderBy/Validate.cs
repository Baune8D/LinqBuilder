using System;

namespace LinqBuilder.OrderBy
{
    public static class Validate
    {
        public static void Pagination(int pageNo, int pageSize)
        {
            if (pageNo < 1) throw new ArgumentException("Cannot be less than 1!", nameof(pageNo));
            if (pageSize < 1) throw new ArgumentException("Cannot be less than 1!", nameof(pageSize));
        }
    }
}
