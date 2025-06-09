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
    public Button craftPanelButtonUI;
    public Transform craftPanel;

    public Dictionary<ItemName, List<CraftItem>> crafteableItemDict;

    private void Awake() {
        crafteableItemDict = new Dictionary<ItemName, List<CraftItem>>();
        craftPanel.gameObject.SetActive(false);
    }

    private void Start() {
        CraftableItem();

        axeButtonUI.onClick.AddListener(() =>
        {

          
                
                    if (IsAbleTocraftItem(ItemName.Axe)) {
                    
                        ReduceCraftItemFromInventory(ItemName.Axe);
                        Player.Instance.AddInventoryItem(GameManager.Instance.GetItemByName(ItemName.Axe));
                    }  

                
            
        });


        pickAxeButtonUI.onClick.AddListener(() =>
        {
          
                
                    if (IsAbleTocraftItem(ItemName.PickAxe)) {
                
                    ReduceCraftItemFromInventory(ItemName.PickAxe);
                    Player.Instance.AddInventoryItem(GameManager.Instance.GetItemByName(ItemName.PickAxe));
                }
                
            
        });


        craftPanelButtonUI.onClick.AddListener(() =>
        {
            craftPanel.gameObject.SetActive(!craftPanel.gameObject.activeSelf);
        });
    }

    public void CraftableItem() {

        BuildItem(ItemName.Axe, new List<CraftItem>
         {
            new CraftItem(3, ItemName.Stone, true),
            new CraftItem(3, ItemName.Loog, true),
           // new CraftItem(1, resourceName.Fiber, true),
         });

        BuildItem(ItemName.PickAxe, new List<CraftItem>
         {
            new CraftItem(3, ItemName.Stone, true),
            new CraftItem(2, ItemName.Loog, true),
         });
    }

    private void BuildItem(ItemName itemName, List<CraftItem> craftItemList) {
        // Example commented-out code
         crafteableItemDict.Add(itemName, craftItemList);
    }


    public bool IsAbleTocraftItem(ItemName itemName) {

        var craftItemsList = crafteableItemDict.FirstOrDefault(s => s.Key == itemName);
       

       

        List<InventoryItem> inventoryItems = Player.Instance.inventoryList;
        int objectToCraftAmount = craftItemsList.Value.Count;
        int index = 0;
        foreach (CraftItem craftItem in craftItemsList.Value) {

            foreach (var inventoryItem in inventoryItems) {

             

                if (craftItem.name==inventoryItem.baseItem.itenName && inventoryItem.stackCount>= craftItem.amount) {
                    index++;
                    Debug.Log("encontrado " + craftItem.name + " "+ index);
                    break;
                }
            }
        
        }
        if (index==objectToCraftAmount) {
            return true;
        }

        return false;
    }

    public void ReduceCraftItemFromInventory(ItemName itemName) {

      var itemToCraft =  crafteableItemDict.FirstOrDefault(s => s.Key == itemName);

        foreach (var itemList in itemToCraft.Value) {
            
            Player.Instance.ReduceItemFromInventory(itemList.name, itemList.amount);
        }

    }
}
