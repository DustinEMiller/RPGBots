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
        var item = Resources.Load<Item>("Items/" + _slotData.ItemName);
        SetItem(item);
    }
}

[Serializable]
public class SlotData
{
    public string SlotName;
    public string ItemName;
}