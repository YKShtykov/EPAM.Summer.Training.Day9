using System;
using TimerLogic;

namespace TimerCUI
{
  /// <summary>
  /// Class describer, writes string in consolewhen evetn happend
  /// </summary>
  class FirstSubscriber
  {
    /// <summary>
    /// constructor describes object of class on event
    /// </summary>
    /// <param name="timer"></param>
    public FirstSubscriber(Timer timer)
    {
      timer.TimeOut += InvokatedMethod;
    }

    /// <summary>
    /// invocated method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    public void InvokatedMethod(object sender, TimeOutEventArgs eventArgs)
    {
      Console.WriteLine("Method of the first describer was invoked by timer event. Waiting time: {0}", eventArgs.Time);
    }
  }

  /// <summary>
  /// Class describer, writes string in consolewhen evetn happend
  /// </summary>
  class SecondSubscriber
  {
    /// <summary>
    /// constructor describes object of class on event
    /// </summary>
    /// <param name="timer"></param>
    public SecondSubscriber(Timer timer)
    {
      timer.TimeOut += InvokatedMethod;
    }

    /// <summary>
    /// invocated method
    /// </summary>
    public void InvokatedMethod(object sender, TimeOutEventArgs eventArgs)
    {
      Console.WriteLine("Method of the second describer was invoked by timer event. Waiting time: {0}", eventArgs.Time);
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      Timer timer = new Timer();
      FirstSubscriber first = new FirstSubscriber(timer);
      SecondSubscriber second = new SecondSubscriber(timer);

      Console.WriteLine("enter waiting time in seconds");
      TimeSpan waiting = TimeSpan.FromSeconds(int.Parse(Console.ReadLine()));
      timer.TimerOn(waiting);
      Console.ReadKey();
    }
  }
}
