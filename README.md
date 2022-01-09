# Monster Hunter: World Max Inventory
Set the player's inventory items count (items in item pouch and item box) to 9999.

**Use it at your own risk.**

## Building
Using [`dotnet`](https://dotnet.microsoft.com/en-us/):
```
$ dotnet build -c Release
```

Using [Visual Studio](https://visualstudio.microsoft.com/):
1. Open `MhwInventory.sln` using Visual Studio
2. Debug -> Release
3. Build -> Build solution

## Usage
```
$ MhwInventory <inventory base address>
```

## Finding Base Address
1. In [Cheat Engine](https://cheatengine.org/), find the address for the count of any item box item
2. Right click address -> Browse this memory location
3. Right click memory cell -> Display Type -> 4 Byte decimal
4. Scroll up until you see `4294967295 4294967295 4294967295 4294967295 4294967295 4294967295`
5. Scroll up more until you see the first `1123518944 1`
6. Right click the first `1123518944` cell -> Add this address to the list
7. Copy the address and input it in program: `dotnet run -c Release --project MhwInventory <address>`

## Notice
If the program breaks for some reason, validate the address offsets in `InventoryHelper.MaxInventory(GameHandle handle, IntPtr address)`.
