using UnityEngine;

[CreateAssetMenu(fileName = "NewDropableSO", menuName = "DropableSo")]
public class DropableSO : ScriptableObject, IDropable
{
    
    public Transform dropPrefab;
    public int amount;
    public Sprite icon;
    public resourceName itemName;

    public Transform ItenDroped { get => dropPrefab; set => dropPrefab = value; }
    public int Amount { get => amount; set => amount = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public resourceName ItemName { get => itemName; set => itemName = value; }





}
