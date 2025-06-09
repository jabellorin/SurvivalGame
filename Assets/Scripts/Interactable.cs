using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

  [SerializeField]  ItemName itemName;

   [SerializeField] Transform imageTransform;

    public ItemName ItemName { get => itemName; set => itemName = value; }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            imageTransform.gameObject.SetActive(true);
        }
    }


}
