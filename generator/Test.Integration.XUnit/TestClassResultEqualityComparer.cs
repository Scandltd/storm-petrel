using System.Collections.Generic;
using System.Linq;

namespace Test.Integration.XUnit
{
    public class TestClassResultEqualityComparer : IEqualityComparer<TestClassResult>
    {
        public bool Equals(TestClassResult? x, TestClassResult? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.StringProperty == y.StringProperty &&
                   x.StringNullableProperty == y.StringNullableProperty &&
                   x.IntProperty == y.IntProperty &&
                   x.IntNullableProperty == y.IntNullableProperty &&
                   x.EnumProperty == y.EnumProperty &&
                   x.EnumNullableProperty == y.EnumNullableProperty &&
                   x.DateTimeProperty == y.DateTimeProperty &&
                   x.DateTimeNullableProperty == y.DateTimeNullableProperty &&
                   x.BooleanProperty == y.BooleanProperty &&
                   x.BooleanNullableProperty == y.BooleanNullableProperty &&
                   Equals(x.TestClassResultObject, y.TestClassResultObject) &&
                   (x.TestClassResultEnumerable?.SequenceEqual(y.TestClassResultEnumerable ?? Enumerable.Empty<TestClassResult>(), this) ?? y.TestClassResultEnumerable == null) &&
                   (x.TestClassResultList?.SequenceEqual(y.TestClassResultList ?? new List<TestClassResult>(), this) ?? y.TestClassResultList == null) &&
                   (x.TestClassResultDict?.SequenceEqual(y.TestClassResultDict ?? new Dictionary<string, TestClassResult>(), new KeyValuePairComparer(this)) ?? y.TestClassResultDict == null);
        }

        public int GetHashCode(TestClassResult obj)
        {
            if (obj == null)
                return 0;

            int hash = 17;
            hash = hash * 23 + (obj.StringProperty?.GetHashCode(StringComparison.Ordinal) ?? 0);
            hash = hash * 23 + (obj.StringNullableProperty?.GetHashCode(StringComparison.Ordinal) ?? 0);
            hash = hash * 23 + obj.IntProperty.GetHashCode();
            hash = hash * 23 + (obj.IntNullableProperty?.GetHashCode() ?? 0);
            hash = hash * 23 + obj.EnumProperty.GetHashCode();
            hash = hash * 23 + (obj.EnumNullableProperty?.GetHashCode() ?? 0);
            hash = hash * 23 + obj.DateTimeProperty.GetHashCode();
            hash = hash * 23 + (obj.DateTimeNullableProperty?.GetHashCode() ?? 0);
            hash = hash * 23 + obj.BooleanProperty.GetHashCode();
            hash = hash * 23 + (obj.BooleanNullableProperty?.GetHashCode() ?? 0);
            hash = hash * 23 + (obj.TestClassResultObject?.GetHashCode() ?? 0);
            hash = hash * 23 + (obj.TestClassResultEnumerable?.GetHashCode() ?? 0);
            hash = hash * 23 + (obj.TestClassResultList?.GetHashCode() ?? 0);
            hash = hash * 23 + (obj.TestClassResultDict?.GetHashCode() ?? 0);

            return hash;
        }

        private sealed class KeyValuePairComparer : IEqualityComparer<KeyValuePair<string, TestClassResult>>
        {
            private readonly IEqualityComparer<TestClassResult> _valueComparer;

            public KeyValuePairComparer(IEqualityComparer<TestClassResult> valueComparer)
            {
                _valueComparer = valueComparer;
            }

            public bool Equals(KeyValuePair<string, TestClassResult> x, KeyValuePair<string, TestClassResult> y)
            {
                return x.Key == y.Key && _valueComparer.Equals(x.Value, y.Value);
            }

            public int GetHashCode(KeyValuePair<string, TestClassResult> obj)
            {
                int hash = 17;
                hash = hash * 23 + obj.Key.GetHashCode(StringComparison.Ordinal);
                hash = hash * 23 + _valueComparer.GetHashCode(obj.Value);
                return hash;
            }
        }
    }
}
