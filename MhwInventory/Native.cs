using System.Runtime.InteropServices;

namespace MhwInventory;

/// <summary>
/// Windows API functions.
/// </summary>
public static class Native
{
    /// <summary>
    /// Permission value for all access in <c>dwDesiredAccess</c>.
    /// </summary>
    public const int AllAccess = 0x001F0FFF;

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    public static extern int CloseHandle(IntPtr hProcess);
}
