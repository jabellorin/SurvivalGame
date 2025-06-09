using TMPro;
using UnityEngine;

public class StoneUI : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI amounTxt;
    private resourceName itenName = resourceName.SmallRock;
    private void Start() {
        Player.Instance.OnItemAddedResourceDict += Instance_OnItemAddedResourceDict;
        Player.Instance.OnItemRemoveResourceDict += Instance_OnItemRemoveResourceDict;
        amounTxt.text = "0";
    }

    private void Instance_OnItemRemoveResourceDict(int amount) {
        UpdateUI(amount);
    }

    private void Instance_OnItemAddedResourceDict(int amount, resourceName itemName) {
      
        if (this.itenName.Equals(itemName)) {
           
            UpdateUI(amount);
        }
    }


    public void UpdateUI(int amount) {
       
        amounTxt.text = amount.ToString();
    }
 
}
