using UnityEngine;

[CreateAssetMenu(fileName = "NewFoodSO", menuName = "FoodSO")]

public class Food : Item
{
   
    public float spoilTimer;
    public float hungerTimer = 0f;
    public int healthAdded;


}
