using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{

    public Button inventoryButton;
    public TextMeshProUGUI amountTxt;
    public Image image;
    public Item item;
    public InventoryUI inventoryUI;

    private void Start() {
        inventoryButton.onClick.AddListener(() =>
        {
            if (item!=null && item.isConsumeable) {
                if (item is Food food) {
                  
                    Player.Instance.EatFood(food.hungerTimer,food.healthAdded);
                    Player.Instance.DecreaseConsumableItem(item);
                    inventoryUI.CreateInventory();

                }
            }
        });
    }

    public void ChangeImage(Sprite newSprite) {
        if (image != null && newSprite != null) {
            image.sprite = newSprite;
        }
    }

    public void ChangeAmountTxt(string amount) {

        amountTxt.text = amount;
    }
}
