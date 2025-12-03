namespace Sol_02
{
   public static class Expansions
  {
    public static bool Equals<T>(this ref ReadOnlySpan<T> a, ref ReadOnlySpan<T> b)
    {
      if (a.Length != b.Length)
        return false;

      for (int i = 0; i < a.Length; i++)
        if (!a[i]!.Equals(b[i]))
          return false;
      return true;
    }
  }
}
