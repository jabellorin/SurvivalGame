using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DrinkFromContainerWater : MonoBehaviour
{
    public int amountOfWater;
    public Image waterImage;

    //private void OnCollisionEnter(Collision collision) {
    //    if (collision.collider.CompareTag("Player")) {
    //        if  (Player.Instance.lastHit.GetComponent<DrinkFromContainerWater>() != null) {
    //            Player.Instance.DrinkLiquid(30);
    //        }
    //    }
    //}


    public void TakeWater() {

        if (amountOfWater>0) {
            amountOfWater -= 10;
        }
       
    }


    public void ShowWater() {

        waterImage.gameObject.SetActive(true);
    }

    public void HideWater() {

        waterImage.enabled = false;
    }
}
