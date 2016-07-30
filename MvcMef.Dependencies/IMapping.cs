using System;

namespace MvcMef.Dependencies
{
    using System.Collections.Generic;

    public interface IMapping<TSource, TTarget> : IMap
    {
        TTarget Map(TSource source);
        TSource Map(TTarget source);

        IEnumerable<TTarget> Map(IEnumerable<TSource> source);
        IEnumerable<TSource> Map(IEnumerable<TTarget> source);

    }

    public interface IMap
    {
        bool CanMap(Type sourceType, Type targetType);
        bool CanMapToTarget(Type targetType);
        bool CanMapFromSource(Type targetType);
    }
}

