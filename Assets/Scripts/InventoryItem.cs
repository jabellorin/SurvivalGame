using UnityEngine;

public class InventoryItem 
{


    public Item baseItem;
    public int stackCount;

    public float currentDurability;
    public float timeUntilSpoil;


    public InventoryItem(Item item) {
        baseItem = item;
        stackCount = item.amount;
    }


}
