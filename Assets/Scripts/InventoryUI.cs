using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public Button inventoryButton;

    public Transform container;
    public Transform template;
    public Transform panel;


    private void Awake() {
        panel.gameObject.SetActive(false);
    }
    private void Start() {
        inventoryButton.onClick.AddListener(() =>
        {
            panel.gameObject.SetActive(!panel.gameObject.activeSelf);
            CreateInventory();
        });
    }

    public void CreateInventory() {

    // Dictionary<ItemName, Item> items = Player.Instance.inventoryList;

    foreach (Transform item in container)
	{
            if (item==template) {
                item.gameObject.SetActive(false);
                continue;
            }

            Destroy(item.gameObject);
	}


        foreach (InventoryItem inventoryItem in Player.Instance.inventoryList) {

            Transform slot = Instantiate(template, container);
            slot.gameObject.SetActive(true);
            SlotUI slotUI = slot.GetComponent<SlotUI>();
            slotUI.ChangeImage(inventoryItem.baseItem.icon);
            slotUI.ChangeAmountTxt(inventoryItem.stackCount.ToString());
            slotUI.item = inventoryItem.baseItem;


        }



    }
}
