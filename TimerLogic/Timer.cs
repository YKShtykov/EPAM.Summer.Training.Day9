using System;
using System.Threading;

namespace TimerLogic
{
  /// <summary>
  /// Custom EventArgs parameter, that contains waiting time
  /// </summary>
  public sealed class TimeOutEventArgs: EventArgs
  {
    private readonly TimeSpan time;
    public TimeOutEventArgs(TimeSpan time)
    {
      this.time = time;
    }
    public TimeSpan Time { get { return time; } }
  }

  /// <summary>
  /// Class timer, that invokates methods of objects-describers
  /// </summary>
  public sealed class Timer
  {
    /// <summary>
    /// event contains list of invocated methods
    /// </summary>
    public event EventHandler<TimeOutEventArgs> TimeOut = delegate { };

    /// <summary>
    /// Method initialising event
    /// </summary>
    /// <param name="e"></param>
    private void OnTimeOut (TimeOutEventArgs e)
    {
      TimeOut(this, e);
    }

    /// <summary>
    /// Method that start timer.
    /// </summary>
    /// <param name="waiting"> input TimeSpan parameter, sets time of waiting</param>
    public void TimerOn(TimeSpan waiting)
    {
      Thread.Sleep(waiting);
      OnTimeOut(new TimeOutEventArgs(waiting));
    }

  }
}
