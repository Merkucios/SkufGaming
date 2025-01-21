using System;

public class ItemCollectedEventArgs : EventArgs
{
    public string ItemName { get; }

    public ItemCollectedEventArgs(string itemName)
    {
        ItemName = itemName;
    }
}