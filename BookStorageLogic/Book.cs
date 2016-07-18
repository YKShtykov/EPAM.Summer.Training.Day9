using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStorageLogic
{
  [Serializable]
  public class Book : IEquatable<Book>, IComparable<Book>
  {
    public string Author { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public int Cost { get; set; }

    public Book(string author,
                string title,
                int pageCount,
                int cost)
    {
      Author = author;
      Title = title;
      PageCount = pageCount;
      Cost = cost;
    }

    /// <summary>
    /// Interface method CompareTo
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Book other)
    {
      return Cost.CompareTo(other.Cost);
    }

    /// <summary>
    /// Class method CompareTo
    /// </summary>
    /// <param name="other"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public int CompareTo(Book other, IComparer<Book> comparer)
    {
      return comparer.Compare(this, other);
    }

    /// <summary>
    /// Class method Equals
    /// </summary>
    /// <returns></returns>
    public bool Equals(Book other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;

      return (Author == other.Author && Title == other.Title && PageCount == other.PageCount);
    }

    /// <summary>
    /// Overriding object method equals
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;

      if (obj.GetType() == typeof(Book)) return Equals((Book)obj);
      return false;
    }

    /// <summary>
    /// Overriding object method GetHashCode
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
      return 555 * Author.GetHashCode() + 55 * Title.GetHashCode() + 5 * PageCount + Cost;
    }

    /// <summary>
    /// Overriding object method ToString
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return string.Format("Book: {0}; Author: {1}; {2} pages; Cost {3}", Title, Author, PageCount, Cost);
    }
  }

  /// <summary>
  /// Class compares two books authors
  /// </summary>
  public class AuthorComparer : IComparer<Book>
  {
    public int Compare(Book x, Book y)
    {
      return x.Author.CompareTo(y.Author);
    }
  }

  /// <summary>
  /// Class compares two books titles
  /// </summary>
  public class TitleComparer : IComparer<Book>
  {
    public int Compare(Book x, Book y)
    {
      return x.Title.CompareTo(y.Title);
    }
  }

  /// <summary>
  /// Class compares two books counts of page
  /// </summary>
  public class PageCountComparer : IComparer<Book>
  {
    public int Compare(Book x, Book y)
    {
      return x.PageCount.CompareTo(y.PageCount);
    }
  }
}
