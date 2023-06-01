using System.Collections.Generic;


public static class BillInventory
{
    public static Dictionary<int, int> Stock { get; } = new Dictionary<int, int>
    {
        { 200, 5 }, { 100, 5 }, { 50, 5 }, { 20, 5 }
    };
}

