using UnityEngine;

public interface IDropable 
{
    public Transform ItenDroped { get; set; }
    int Amount { get; set; }

    Sprite Icon { get; set; }

    resourceName ItemName { get; set; }


}
