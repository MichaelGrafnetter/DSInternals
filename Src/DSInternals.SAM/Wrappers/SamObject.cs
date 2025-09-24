using DSInternals.SAM.Interop;

namespace DSInternals.SAM;

/// <summary>
/// Base class for objects in the SAM database.
/// </summary>
public abstract class SamObject : IDisposable
{
    private protected SafeSamHandle Handle
    {
        get;
        set;
    }

    private protected SamObject(SafeSamHandle handle)
    {
        this.Handle = handle;
    }

    /// <summary>
    /// Disposes the object.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the object.
    /// </summary>
    /// <param name="disposing">Indicates whether the method was called directly or by the garbage collector.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (this.Handle != null)
            {
                this.Handle.Dispose();
                this.Handle = null;
            }
        }
    }
}
