using UnityEngine;

public class CraftItem 
{

  public int amount;
  public  ItemName name;
  public bool canBeCrafted;



    public CraftItem(int amount, ItemName name, bool canBeCrafted) {
        this.amount = amount;
        this.name = name;
        this.canBeCrafted = canBeCrafted;
    }
}
