namespace DSInternals.PowerShell.Commands;

/// <summary>
/// An <see cref="IProgress{T}"/> implementation that invokes the supplied handler synchronously on the calling thread.
/// </summary>
/// <remarks>
/// PowerShell cmdlets must call <c>WriteProgress</c> from the pipeline thread. The BCL's <see cref="Progress{T}"/>
/// dispatches asynchronously through the captured <see cref="SynchronizationContext"/> (or the thread pool when none
/// is present), which would marshal the callback off the pipeline thread and cause <c>WriteProgress</c> to throw.
/// </remarks>
internal sealed class SynchronousProgress<T> : IProgress<T>
{
    private readonly Action<T> _handler;

    public SynchronousProgress(Action<T> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        _handler = handler;
    }

    public void Report(T value) => _handler(value);
}
