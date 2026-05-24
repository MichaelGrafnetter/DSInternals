using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DSInternals.Common.Interop;

/// <summary>
/// Represents a Win32 window handle (<c>HWND</c>).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct WindowHandle : IEquatable<WindowHandle>
{
    private readonly IntPtr handle;

    public WindowHandle(IntPtr handle) => this.handle = handle;

    public static WindowHandle Null => default;

    public static WindowHandle ConsoleWindow => NativeMethods.GetConsoleWindow();

    public static WindowHandle MainWindow
    {
        get
        {
            using Process currentProcess = Process.GetCurrentProcess();
            return new WindowHandle(currentProcess.MainWindowHandle);
        }
    }

    public static WindowHandle ForegroundWindow => NativeMethods.GetForegroundWindow();

    public bool IsValid => this.handle != IntPtr.Zero;

    public bool Equals(WindowHandle other) => this.handle == other.handle;

    public override bool Equals(object? obj) => obj is WindowHandle other && this.Equals(other);

    public override int GetHashCode() => this.handle.GetHashCode();

    public static bool operator ==(WindowHandle left, WindowHandle right) => left.handle == right.handle;

    public static bool operator !=(WindowHandle left, WindowHandle right) => left.handle != right.handle;
}
