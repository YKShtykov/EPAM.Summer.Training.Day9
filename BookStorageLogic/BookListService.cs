using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Search of book with input paramether (Author or Title)
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>searched book or exception "book was not found"</returns>
    public Book FindBookByTag(string authorNameOrTitle)
    {
      if (ReferenceEquals(null, authorNameOrTitle)) throw new ArgumentNullException();

      foreach (Book item in BookStorage)
      {
        if (item.Author == authorNameOrTitle) return item;
        if (item.Title == authorNameOrTitle) return item;
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
}
