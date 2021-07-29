using System;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item _item;
    private SlotData _slotData;
    public bool IsEmpty => _item == null;

    public void SetItem(Item item)
    {
        _item = item;
        _slotData.ItemName = item?.name ?? string.Empty;
    }

    public void Bind(SlotData slotData)
    {
        _slotData = slotData;
        Debug.LogError("Attempted to load item {_slotData.ItemName}");
        //SetItem(slotData.ItemName);
    }
}

[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;
}