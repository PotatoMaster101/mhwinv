namespace MhwInventory;

public static class InventoryHelper
{
    /// <summary>
    /// Max out items in inventory (9999).
    /// </summary>
    /// <param name="handle">The game handle.</param>
    /// <param name="address">The inventory start address.</param>
    /// <returns>Number of inventory items updated.</returns>
    public static int MaxInventory(GameHandle handle, IntPtr address)
    {
        var count = MaxInventory(handle, address, 288);     // section for item pouch
        address += 0x4E8;
        return count + MaxInventory(handle, address, 8880);    // section for item box
    }

    /// <summary>
    /// Max out items in inventory (9999).
    /// </summary>
    /// <param name="handle">The game handle.</param>
    /// <param name="address">The inventory start address.</param>
    /// <param name="count">The max number of entries in the inventory.</param>
    /// <returns>Number of inventory items updated.</returns>
    private static int MaxInventory(GameHandle handle, IntPtr address, int count)
    {
        var data = handle.ReadMemory(address, count);
        var result = 0;
        foreach (var chunk in data.Chunk(4))
        {
            if (chunk.Length is not 4)
                continue;
            if (chunk[0].Item2 is not 1123518944 || chunk[1].Item2 is not 1 || chunk[2].Item2 is 0 || chunk[3].Item2 is 9999)
                continue;

            handle.WriteMemory(chunk[3].Item1, 9999);
            result++;
        }
        return result;
    }
}
