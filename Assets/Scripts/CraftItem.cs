using UnityEngine;

public class CraftItem 
{

  public int amount;
  public  resourceName name;
  public bool canBeCrafted;



    public CraftItem(int amount, resourceName name, bool canBeCrafted) {
        this.amount = amount;
        this.name = name;
        this.canBeCrafted = canBeCrafted;
    }
}
