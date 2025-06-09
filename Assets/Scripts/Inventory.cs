using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int slots = 10;
    bool isActive = false;
    int availableSlots;
    bool isEmpty = false;

    [SerializeField] Transform container;
    [SerializeField] Transform template;




    private void Awake() {
        
    }

}
