using System.Diagnostics;
using BenStull.HttpRequestTelemetry.Domain.ScopedStopwatch;

namespace BenStull.HttpRequestTelemetry.Model.Util
{
    /// <summary>
    /// Wraps stopwatch with disposible pattern
    /// Constructor starts stopwatch, 
    /// </summary>
    public class ScopedStopwatch : IScopedStopwatch
    {
        public Stopwatch WrappedStopwatch { get; }

        public ScopedStopwatch(Stopwatch stopwatch)
        {
            WrappedStopwatch = stopwatch;

            if (!WrappedStopwatch.IsRunning)
            {
                WrappedStopwatch.Start();
            }
        }

        public void Dispose()
        {
            if (WrappedStopwatch.IsRunning)
            {
                WrappedStopwatch.Stop();
            }
        }
    }
}