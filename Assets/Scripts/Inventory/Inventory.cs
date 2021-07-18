using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items = new List<Item>();
    [SerializeField] private Item _debugItem;

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem() => AddItem(_debugItem);
    
    

}
