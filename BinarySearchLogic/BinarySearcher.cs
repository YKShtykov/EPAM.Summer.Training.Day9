using System;

namespace BinarySearchLogic
{
  /// <summary>
  /// Class does Binary search for array of any values.
  /// </summary>
  public static class BinarySearcher
  {
    /// <summary>
    /// Binary search method with default comparision hash codes
    /// </summary>
    /// <param name="array"></param>
    /// <param name="searchedElement"></param>
    /// <returns>number of searched element in array or -1 if array has no the element</returns>
    public static int BinarySearch<T>(T[] array,T searchedElement)
    {
      return BinarySearch(array, searchedElement, DefaultComparision);
    }
    /// <summary>
    /// overloaded method takes array of values and user comparision type of wich int Comparision<T>(T lhs, T rhs)
    /// </summary>
    /// <param name="array"></param>
    /// <param name="searchedElement"></param>
    /// <param name="comparer"></param>
    /// <returns>number of searched element in array or -1 if array has no the element</returns>
    public static int BinarySearch<T>(T[] array,
                                      T searchedElement,
                                      Comparison<T> comparer)
    {
      if (ReferenceEquals(array, null)) throw new ArgumentException();
      if (ReferenceEquals(searchedElement, null)) return -1;
      if (ReferenceEquals(comparer, null)) throw new ArgumentException();

      Sort(array, comparer);
      int bot = 0;
      int mid = array.Length / 2;
      int top = array.Length;

      while (top != bot)
      {
        int compareResult = comparer( array[mid], searchedElement);
        if (compareResult>0)
        {
          top = mid;
          mid = top / 2;
        }
        if (compareResult < 0)
        {
          bot = mid;
          mid = (bot + top) / 2;
        }
        if (compareResult == 0) return mid;
      }

      return -1;
    }

    /// <summary>
    /// Default comparision method
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    private static int DefaultComparision<T>(T lhs,T rhs)
    {
      return lhs.GetHashCode().CompareTo(rhs.GetHashCode());
    }

    /// <summary>
    /// Sort of incoming array
    /// </summary>
    /// <param name="array"></param>
    /// <param name="comparer"></param>
    private static void Sort<T>(T[] array, Comparison<T> comparer)
    {
      bool sorted = false;
      while (!sorted)
      {
        sorted = true;
        for (int i = 0; i < array.Length-1; i++)
        {
          if (comparer(array[i], array[i + 1]) == 1)
          {
            Swap(ref array[i], ref array[i + 1]);
            sorted = false;
          }
        }
      }
    }

    private static void Swap<T>(ref T lhs, ref T rhs)
    {
      T temp = lhs;
      lhs = rhs;
      rhs = temp;
    }
  }
}
