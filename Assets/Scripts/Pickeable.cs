using UnityEngine;
using UnityEngine.UI;

public class Pickeable : MonoBehaviour
{
  [SerializeField]  ItemName itemName;
  [SerializeField] Sprite icon;
  [SerializeField] int amount;

    public ItemName ItemName { get => itemName; set => itemName = value; }
    public Sprite Icon { get => icon; set => icon = value; }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
          //  ItemName itemName = other.transform.GetComponent<Pickeable>().itemName;
           
            
            Item item = GameManager.Instance.GetItemByName(itemName);
            if (item!=null) {
                Player.Instance.AddInventoryItem(item);
            }
            else {
                Debug.Log("item is null" + item);
            }


                Destroy(gameObject);
        }
    }

}
