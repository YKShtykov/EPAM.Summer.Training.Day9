using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsMemoryzation
{
  /// <summary>
  /// Class consists mehtod that counts equal words
  /// </summary>
  public static class WordCounter
  {
    /// <summary>
    /// Method counts equal words and returns dictionary 
    /// in wich key is word and value is count the word in string
    /// </summary>
    /// <param name="text">input string, words will be counted in</param>
    /// <returns>dictionary(string, int)</returns>
    public static Dictionary<string, int> GetWordsCount(string text)
    {
      Dictionary<string, int> result = new Dictionary<string, int>();
      if (ReferenceEquals(text, null)) throw new ArgumentNullException();

      char[] charArray = text.ToCharArray();
      for (int i = 0; i < charArray.Length; i++)
      {
        if (!char.IsLetter(charArray[i]))
        {
          charArray[i] = ' ';
        }
      }

      string[] textArray = new string(charArray).Split(new char[] { ' ' });
      foreach (var item in textArray)
      {
        if (!(item == string.Empty))
        {
          if (!result.ContainsKey(item))
          {
            result.Add(item, 1);
          }
          else
          {
            result[item]++;
          }
        }
      }

      return result;
    }
  }
}
