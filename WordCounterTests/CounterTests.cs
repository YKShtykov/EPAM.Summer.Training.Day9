using System.Collections.Generic;
using NUnit.Framework;
using WordsMemoryzation;
using System;

namespace WordCounterTests
{
  /// <summary>
  /// Test class that tests WordCounter
  /// </summary>
  [TestFixture]
  public class CounterTests
  {
    public Dictionary<string, int> result = new Dictionary<string, int>();
    public Dictionary<string, int> complexResult = new Dictionary<string, int>();
    /// <summary>
    /// Method -initializer initialise dictionaries 
    /// </summary>
    [TestFixtureSetUp]
    public void Initializer()
    {
      result.Add("string", 2);
      complexResult.Add("string", 2);
      complexResult.Add("int", 3);
      complexResult.Add("false", 2);
      complexResult.Add("bool", 1);
    }

    /// <summary>
    /// Simple case
    /// </summary>
    /// <param name="a"></param>
    [TestCase("string string")]
    public void CounterTest(string a)
    {
      Dictionary<string, int> actual = WordCounter.GetWordsCount(a);

      CollectionAssert.AreEqual(result, actual);
    }

    /// <summary>
    /// More complex case
    /// </summary>
    /// <param name="a"></param>
    [TestCase("string string int int int false, false bool")]
    public void ComplexCounterTest(string a)
    {
      Dictionary<string, int> actual = WordCounter.GetWordsCount(a);

      CollectionAssert.AreEqual(complexResult, actual);
    }

    /// <summary>
    /// Case of string equals null
    /// </summary>
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NullCounterTest()
    {
      string a = null;
      Dictionary<string, int> actual = WordCounter.GetWordsCount(a);
    }
  }
}
