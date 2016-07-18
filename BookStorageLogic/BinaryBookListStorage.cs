using NLog;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStorageLogic
{
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
}
