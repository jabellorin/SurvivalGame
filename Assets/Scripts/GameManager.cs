using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    public List<PlaceableItem> placeableItemsList;


    private void Awake() {
        Instance = this;
       
    }


    public List<Item> listOfItems;



    public Item GetItemByName(ItemName name) {

        return listOfItems.FirstOrDefault(s => s.itenName == name);
    }

    public PlaceableItem GetPlaceableItem(ItemName itemName ) {

        PlaceableItem item = placeableItemsList.FirstOrDefault(s => s.itenName == itemName);

        if (item!=null) {
            return item;
        }
        return null;
    }

}
