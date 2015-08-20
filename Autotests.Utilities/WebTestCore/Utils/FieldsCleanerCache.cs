using System;
using System.Collections.Generic;

namespace Autotests.Utilities.WebTestCore.Utils
{
    internal static class FieldsCleanerCache
    {
        private static readonly IDictionary<long, Action<object>> cache = new Dictionary<long, Action<object>>();

        public static void Clean(object obj)
        {
            Action<object> action;
            Type type = obj.GetType();
            var key = (long) type.TypeHandle.Value;
            if (!cache.TryGetValue(key, out action))
            {
                var cleaner = new FieldsCleaner(type);
                action = cleaner.GetDelegate();
                cache[key] = action;
            }
            action(obj);
        }
    }
}