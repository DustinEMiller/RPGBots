using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int GENERAL_SIZE = 9;
    public ItemSlot[] GeneralInventory = new ItemSlot[GENERAL_SIZE];
    [SerializeField] private Item _debugItem;

    void Awake()
    {
        for (int i = 0; i < GENERAL_SIZE; i++)
            GeneralInventory[i] = new ItemSlot();
    }
    public void AddItem(Item item)
    {
        var firstAvailableSlot = GeneralInventory.FirstOrDefault(t=> t.IsEmpty);
        firstAvailableSlot.SetItem(item);
    }

    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem() => AddItem(_debugItem);
    
    

}