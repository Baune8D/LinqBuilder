﻿using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications.Tests.TestHelpers
{
    public class Value1Specification : Specification<TestEntity>
    {
        private readonly int _value;

        public Value1Specification(int value)
        {
            _value = value;
        }

        public override Expression<Func<TestEntity, bool>> AsExpression()
        {
            return entity => entity.Value1 == _value;
        }
    }
}
