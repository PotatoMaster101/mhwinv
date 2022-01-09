using System.Diagnostics;

namespace MhwInventory;

/// <summary>
/// Represents a handle to the game process.
/// </summary>
public class GameHandle : IDisposable
{
    /// <summary>
    /// Gets the process handle for the game process.
    /// </summary>
    /// <value>The process handle for the game process.</value>
    public IntPtr Handle { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="GameHandle"/>.
    /// </summary>
    public GameHandle()
    {
        var game = Process.GetProcessesByName("MonsterHunterWorld").First();
        Handle = Native.OpenProcess(Native.AllAccess, false, game.Id);
    }

    /// <summary>
    /// Writes a 4 byte value to the game process.
    /// </summary>
    /// <param name="address">The address to start writing.</param>
    /// <param name="value">The value to write.</param>
    public void WriteMemory(IntPtr address, int value)
    {
        var bytes = BitConverter.GetBytes(value);
        Native.WriteProcessMemory(Handle, address, bytes, bytes.Length, out _);
    }

    /// <summary>
    /// Reads memory as 4 byte values from the game process.
    /// </summary>
    /// <param name="address">The address to start reading.</param>
    /// <param name="size">The number of 4 byte values to read.</param>
    /// <returns>The read 4 byte values read.</returns>
    public IEnumerable<(IntPtr, int)> ReadMemory(IntPtr address, int size)
    {
        var buffer = new byte[size * 4];
        Native.ReadProcessMemory(Handle, address, buffer, buffer.Length, out _);

        var result = new List<(IntPtr, int)>(size);
        var chunks = buffer.Chunk(4).ToArray();
        result.AddRange(chunks.Select((t, i) => (address + 4 * i, BitConverter.ToInt32(t))));
        return result;
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Native.CloseHandle(Handle);
    }
}
