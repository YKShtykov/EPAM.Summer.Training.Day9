using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace BookStorageLogic
{
  /// <summary>
  /// iterface for book collections serialization
  /// </summary>
  public interface IBookListStorage
  {
    List<Book> LoadBooks();
    void SaveBooks(IEnumerable<Book> books);
  }


  /// <summary>
  /// Class for constructing of  book collection
  /// </summary>
  public class BookListService: IEnumerable<Book>
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    /// <summary>
    /// Collection of books
    /// </summary>
    public List<Book> BookStorage { get; private set; }

    public BookListService()
    {
      BookStorage = new List<Book>();
    }  

    /// <summary>
    /// Adding book in collection
    /// </summary>
    /// <param name="book"></param>
    public void AddBook( Book book)
    {
      logger.Trace("adding new book: {0}", book.ToString());
      BookStorage.Add(book);
    }

    /// <summary>
    /// Remove books from collection
    /// </summary>
    /// <param name="book"></param>
    public void RemoveBook(Book book)
    {
      logger.Trace("removing book: {0}", book.ToString());
      BookStorage.Remove(book);
    }

    /// <summary>
    /// Search of book with input paramether (Author, Title, cost or count of pages)
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>searched book or exception "book was not found"</returns>
    public Book FindBookByTag(object obj)
    {
      if (ReferenceEquals(null, obj)) throw new ArgumentNullException();
      int CostOrPages = (obj is int)?(int)obj :-1;
      string AuthorOrTitle = (obj is string)? (string)obj : string.Empty;
      if (CostOrPages!= -1) logger.Trace("search with te tag {0}", CostOrPages);
      if (AuthorOrTitle != string.Empty) logger.Trace("search with te tag {0}", AuthorOrTitle);
      foreach (Book item in BookStorage)
      {
        if (item.Cost == CostOrPages || item.PageCount == CostOrPages) return item;
        if (item.Author == AuthorOrTitle || item.Title == AuthorOrTitle) return item;
      }
      
      throw new Exception("Book was not found");
    }

    /// <summary>
    /// Sorting of collection by user comparison
    /// </summary>
    /// <param name="comparer"></param>
    public void SortBookByTag(Comparison<Book> comparer)
    {
      logger.Trace("sorting books ");
      BookStorage.Sort(comparer);
    }

    public IEnumerator<Book> GetEnumerator()
    {
      return BookStorage.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
      return BookStorage.GetEnumerator();
    }
  }


  /// <summary>
  /// Class for saving book collection in binary format
  /// </summary>
  public class BinaryBookListStorage : IBookListStorage
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    /// <summary>
    /// Name of binary file
    /// </summary>
    public string FileName { get; private set; }

    public BinaryBookListStorage(string fileName = "Books.dat")
    {
      logger.Trace("creating of BinaryBookListStorage instance ");
      FileName = fileName;
    }

    /// <summary>
    /// transform binary file to book collection
    /// </summary>
    /// <returns></returns>
    public List<Book> LoadBooks()
    {
      logger.Trace("create book collection from binary file");
      List<Book> result = new List<Book>();
      using (BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open)))
      {
        int length = reader.ReadInt32();

        for (int i = 0; i < length; i++)
        {
          result.Add(new Book(reader.ReadString(),
                              reader.ReadString(),
                              reader.ReadInt32(),
                              reader.ReadInt32()));
        }        
      }

      return result;
    }

    /// <summary>
    /// save book collection in binary file
    /// </summary>
    /// <param name="books"></param>
    public void SaveBooks(IEnumerable<Book> books)
    {
      logger.Trace("Save books in binary files");
      using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
      {
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
          int length = books.ToArray().Length;
          writer.Write(length);
          foreach (var item in books)
          {
            writer.Write(item.Author);
            writer.Write(item.Title);
            writer.Write(item.PageCount);
            writer.Write(item.Cost);
          }
        }
      }
    }
  }

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
