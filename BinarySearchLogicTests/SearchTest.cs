using System;
using BinarySearchLogic;
using NUnit.Framework;

namespace BinarySearchLogicTests
{
  /// <summary>
  /// Testing class for BinarySearch logic 
  /// </summary>
  [TestFixture]
  public class SearchTest
  {
    /// <summary>
    /// Test Binary search in string array
    /// </summary>
    [Test]
    public void StringTest()
    {
      string[] sArray = { "string", "bool", "int" };
      int result = 2;
      Comparison<string> stringComparer = (lhs,rhs) => string.Compare(lhs, rhs); 
      int actual = BinarySearcher.BinarySearch(sArray, "string", stringComparer);

      Assert.AreEqual(result, actual);
    }

    /// <summary>
    /// tests binary search in int array
    /// </summary>
    [Test]
    public void IntTest()
    {
      int[] iArray = { 111, 222, 13 };
      int result = 0;
      Comparison<int> intComparer = (lhs, rhs) => lhs.CompareTo(rhs);
      int actual = BinarySearcher.BinarySearch(iArray, 13, intComparer);

      Assert.AreEqual(result, actual);
    }
  }
}
