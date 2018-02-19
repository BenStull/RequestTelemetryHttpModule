using System;
using System.Diagnostics;

namespace BenStull.HttpRequestTelemetry.Domain.ScopedStopwatch
{
    /// <summary>
    /// Wraps a Stopwatch with disposible pattern
    /// </summary>
    public interface IScopedStopwatch : IDisposable
    {
        Stopwatch WrappedStopwatch { get; }
    }
}