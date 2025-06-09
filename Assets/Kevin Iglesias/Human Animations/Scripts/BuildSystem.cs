using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.UI;

public class BuildSystem : MonoBehaviour {
    // Ensure field names are unique to avoid ambiguity
    public Button axeButtonUI;
    public Button pickAxeButtonUI;
    public Button bucketButtonUI;

    public Dictionary<ItemName, List<CraftItem>> crafteableItemDict;

    private void Start() {
        axeButtonUI.onClick.AddListener(() =>
        {
            foreach (InventoryItem item in Player.Instance.inventoryList) {
                if (true) {
                    // Logic here
                }
            }
        });
    }

    public void CraftableItem() {

        BuildItem(ItemName.Axe, new List<CraftItem>
         {
            new CraftItem(3, resourceName.SmallRock, true),
            new CraftItem(3, resourceName.Wood, true),
            new CraftItem(1, resourceName.Fiber, true),
         });

        BuildItem(ItemName.PickAxe, new List<CraftItem>
         {
            new CraftItem(3, resourceName.SmallRock, true),
            new CraftItem(2, resourceName.Wood, true),
         });
    }

    private void BuildItem(ItemName itemName, List<CraftItem> craftItemList) {
        // Example commented-out code
         crafteableItemDict.Add(itemName, craftItemList);
    }

  
    // public bool IsAbleTocraftItem(ItemName itemName) {
      
    //    var craftItemsList = crafteableItemDict.FirstOrDefault(s => s.Key == itemName);
    //    foreach (CraftItem item in craftItemsList.Value) {
           
    //    }
    //}
}
