using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
  public ref struct MySortedList<T> where T : INumber<T>
  {
    private T[] _array;
    // User innerArray for any iteration of the array
    private Span<T> _innerArray;
    // index of "last" element
    private int _size = -1;
    public MySortedList(int size)
    {
      _array = new T[size];
      _innerArray = _array.AsSpan<T>();
    }

    public void Add(T item)
    {
      // Easy first and last matches
      if (_size == -1 || _innerArray[_size] <= item)
      {
        _innerArray[++_size] = item;
        return;
      }

      // First check where it should be inserted at
      int ind = Search(ref item);

      // Shift array
      // Need to test if tis better to copy or just shift 1 by 1, prob shift?
      Array.Copy(_array, ind, _array, ind + 1, _size++ - ind + 1);
      _innerArray[ind] = item;

    }

    // returns 
    int Search(ref T val)
    {
      // In my case array won't be longer than 1k records so linear seach will be good
      for (int i = 0; i <= _size; i++)
      {
        if (_innerArray[i] >= val)
          return i;
      }
      return _size + 1;
    }

    public int Count
    {
      get
      {
        return _size + 1;
      }
    }

    public T this[int i]
    {
      get { return _innerArray[i]; }
    }
  }
}
