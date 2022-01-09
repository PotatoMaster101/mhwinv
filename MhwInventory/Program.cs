using MhwInventory;

if (args.Length is 0)
{
    Console.Error.WriteLine("No start address given");
    return;
}

var address = new IntPtr(Convert.ToInt64(args[0], 16));
using var game = new GameHandle();
Console.WriteLine($"Inventory items updated: {InventoryHelper.MaxInventory(game, address)}");
