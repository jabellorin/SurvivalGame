using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;



    private void Awake() {
        Instance = this;
       
    }


    public List<Item> listOfItems;



    public Item GetItemByName(ItemName name) {

        return listOfItems.FirstOrDefault(s => s.itenName == name);
    }

}
