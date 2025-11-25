using System.Diagnostics.CodeAnalysis;
namespace Sol3
{
  class ValueTupleComparer : IEqualityComparer<ValueTuple<int, int>>
  {
    public bool Equals(ValueTuple<int, int> val1, ValueTuple<int,int> val2)
    {
      if (val1.Item1 != val2.Item1)
        return false;
      if (val1.Item2 != val2.Item2)
        return false;
      return true;
    }

    public int GetHashCode([DisallowNull] ValueTuple<int, int> obj)
    {
      return obj.Item1.GetHashCode() ^ obj.Item2.GetHashCode();
    }
  }
}
