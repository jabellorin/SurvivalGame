using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public DropableSO dropableItem;

    public int amount;
    private int currentTimes;
    [SerializeField] int timesToBrake;

    private void Start() {
        if (amount<=0) {
            amount = dropableItem.amount;
        }
    }

    public DropableSO GetItemData() {

        return dropableItem;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Pick")) {
          
            DropItem();
            currentTimes++;
            if (currentTimes>=timesToBrake) {
                Destroy(gameObject);
            }

        }
    }
  


    public void DropItem() {

        Vector3 dropsPos = new Vector3(2, 0, 2);
        Instantiate(dropableItem.dropPrefab, transform.position + dropsPos, Quaternion.identity);


    }
}
