using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour {


    public static Player Instance;
    public event Action OnDrinkWater;

    private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] private float health;
    [SerializeField] private float hunger;
    [SerializeField] private float thirst;
    [SerializeField] private float temperature;


    [SerializeField] private float hungerTimer;
    [SerializeField] private float maxHungerTimer = 5f;
    [SerializeField] private float damageRateWhenHungry = 0.4f;

    [SerializeField] private float thirstTimer=0;
    [SerializeField] private float maxThirstTimer = 5f;
    [SerializeField] private float damageRateWhenThirsty = 0.5f;


    [SerializeField] private float temperatureTimer;
    [SerializeField] private float maxTemperatureTimer = 120f;
    [SerializeField] private float damageRateWhenIsCold = 0.2f;
    [SerializeField] private Material selectableMaterial;
    [SerializeField] private Transform handObjects;


    private bool isHungry = false;
    private bool isThirsty = false;
    private bool isCold = false;
    public ItemName currentItem;
    public LayerMask layerMask;
   public Transform lastHit = null;
    Material previewMaterial = null;
    Vector3 direction;
    private float timeSinceLastHit = 0f;
    private float restoreDelay = 0.05f; // 50 ms de margen



    public float Health { get => health; set => health = value; }
    public float Hunger { get => hunger; set => hunger = value; }
    public float Thirst { get => thirst; set => thirst = value; }
    public float Temperature { get => temperature; set => temperature = value; }
    public Dictionary<resourceName, int> ResourceDict { get => resourceDict; set => resourceDict = value; }


    public event Action<int, resourceName> OnItemAddedResourceDict;
    public event Action<int> OnItemRemoveResourceDict;

    public List<Item> firstItenOnInventory;


    // [SerializeField] private float stamina;     // Energía (opcional)


    private Dictionary<resourceName, int> resourceDict;

    public List<InventoryItem> inventoryList;


    private void Awake() {
        Instance = this;
        resourceDict = new();
          firstItenOnInventory = new();
        inventoryList = new();
    }

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        // _animator = GetComponent<Animator>();

        Hunger = 100;
        Health = 100;
        Thirst = 100;

        currentItem = ItemName.PickAxe;

        foreach (Item item in firstItenOnInventory) {

            AddInventoryItem(item);
           
        }
    }


    private void FixedUpdate() {

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 movePosition = new Vector3(xMovement, 0, zMovement);

        direction = movePosition.normalized;

        if (direction.magnitude > 0.1f) {
            _rb.MovePosition(_rb.position + movePosition * speed * Time.deltaTime);

            Quaternion rotation = Quaternion.LookRotation(direction);

            _rb.rotation = Quaternion.Slerp(_rb.rotation, rotation, rotationSpeed * Time.deltaTime);

            _animator.SetBool("IsRunning", true);
        }
        else {
            _animator.SetBool("IsRunning", false);
        }




    }


    private void Update() {

        hungerTimer += Time.deltaTime;
        thirstTimer += Time.deltaTime;
       

        if (hungerTimer >= maxHungerTimer) {
           
            isHungry = true;
        }
       

        if (thirstTimer >= maxThirstTimer) {
           
            isThirsty = true;
        }

        if (isHungry) {
            Hunger -= damageRateWhenHungry * Time.deltaTime;
            if (Hunger<=20) {
                Health -= damageRateWhenHungry*Time.deltaTime;
            }
            if (Hunger <= 0) {
                Hunger = 0;
            }
        }

        if (isThirsty) {
            thirst -= damageRateWhenThirsty * Time.deltaTime;
            if (thirst <= 20) {
                Health -= damageRateWhenThirsty*Time.deltaTime;
            }

            if (thirst<0) {
                thirst = 0;
            }
        }

        if (isCold) {
            temperatureTimer += Time.deltaTime;
            if (temperatureTimer >= maxTemperatureTimer) {
                Health -= damageRateWhenIsCold * Time.deltaTime;
            }

        }


        Interact();

        DrawRayCast();
        timeSinceLastHit += Time.deltaTime;
    }

    private void Interact() {
        if (Input.GetMouseButtonDown(0)) {

            if (currentItem == ItemName.Axe || currentItem == ItemName.PickAxe) {
                _animator.SetBool("Mining", true);
                _rb.position += Vector3.forward * 0.001f;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (lastHit != null && lastHit.GetComponent<DrinkFromContainerWater>() != null) {
                if (lastHit.GetComponent<DrinkFromContainerWater>().amountOfWater > 0) {
                    DrinkLiquid(60);
                    lastHit.GetComponent<DrinkFromContainerWater>().amountOfWater -= 10;
                }
                else {
                    lastHit.GetComponent<DrinkFromContainerWater>().HideWater();
                    Debug.Log("not enough water");
                }

            }

            if (lastHit != null && lastHit.GetComponent<Interactable>() != null) {
               
                Item item = GameManager.Instance.GetItemByName(lastHit.GetComponent<Interactable>().ItemName);
                if (item != null) {
                    if (item is Food food) {
                        if (lastHit.GetComponent<FoodItem>() != null) {
                            int amount = lastHit.GetComponent<FoodItem>().amount;
                            item.amount = amount;
                        }


                        InventoryItem inventoryItem = new InventoryItem(item);
                        inventoryItem.timeUntilSpoil = food.spoilTimer;
                        inventoryItem.currentDurability = food.hungerTimer;
                        AddInventoryItem(item);
                        Destroy(lastHit.gameObject);
                    }
                  


                }
                if (lastHit.GetComponent<Interactable>().ItemName == ItemName.Captus) {

                    float drinkDuration = 120f;
                    DrinkLiquid(drinkDuration);
                    Destroy(lastHit.gameObject);
                }
            }
           
        }
    }

    public void EatFood(float hungerDuration,int heathAdded) {
        hungerTimer = 0f;
        maxHungerTimer = hungerDuration;
        isHungry = false;
        AddHealt(heathAdded);
        Hunger += 20;
    }

    public void DrinkLiquid(float thirstDuration) {
        thirstTimer = 0f;
        maxThirstTimer = thirstDuration;
        isThirsty = false;
        thirst += 20;
       
    }

    public void ChangeColdState(bool state) {
        temperatureTimer = 0;
        isCold = state;
    }

    public void AddResource(int value, resourceName itemName) {

        if (resourceDict.ContainsKey(itemName)) {
            resourceDict[itemName] += value;
            OnItemAddedResourceDict?.Invoke(resourceDict[itemName], itemName);

        }
        else {
            resourceDict.Add(itemName, value);
            OnItemAddedResourceDict?.Invoke(resourceDict[itemName], itemName);
        }


    }
    public void RemoveResource(int value, resourceName itemName) {

        if (resourceDict.ContainsKey(itemName)) {
            resourceDict[itemName] -= value;
            OnItemRemoveResourceDict?.Invoke(resourceDict[itemName]);

        }



    }




    public void DrawRayCast() {

        Vector3 boxSize = new Vector3(0.4f, 0.4f, 0.4f);
        Vector3 castOrigin = transform.position + Vector3.up * 0.5f + transform.forward * 0.1f;

        Collider[] hitCollider = Physics.OverlapBox(castOrigin, boxSize / 2, Quaternion.identity, layerMask);
        bool found = false;
        foreach (Collider item in hitCollider) {
            if (item.GetComponent<MeshRenderer>() != null) {

               

                if (lastHit != item.transform) {
                 

                    lastHit = item.transform;
                    previewMaterial = item.transform.GetComponent<MeshRenderer>().sharedMaterial;
                    item.transform.GetComponent<MeshRenderer>().sharedMaterial = selectableMaterial;
                    found = true;
                }
                else {
                    return;
                }
                    break;
               
            }

       



        }
        if (lastHit != null && !found ) {
            lastHit.GetComponent<MeshRenderer>().sharedMaterial = previewMaterial;
            lastHit = null;
            previewMaterial = null;
            timeSinceLastHit = 0f;
        }

    }


    public void AddInventoryItem(Item item) {
        InventoryItem inventoryItem = new InventoryItem(item);

        if (item is Food food) {
          
            inventoryItem.timeUntilSpoil = food.spoilTimer;
            inventoryItem.currentDurability = food.hungerTimer;
           
        }
        
        InventoryItem itemFound =  inventoryList.FirstOrDefault(s =>  s.baseItem.itenName==item.itenName);
        if (itemFound != null && item.isStackable) {
    
            itemFound.stackCount+=item.amount;
        }
        else {
            inventoryList.Add(inventoryItem);
            
        }



    }


    public void ReduceItemFromInventory(ItemName item,int amount) {

      InventoryItem inventoryItem =  inventoryList.FirstOrDefault(s => s.baseItem.itenName == item);
        inventoryItem.stackCount-=amount;
        if (inventoryItem.stackCount==0) {
            inventoryList.Remove(inventoryItem);
        }

    }

    public void AddHealt(int amount) {
        Health += amount;
    }


    public void SetHandObject(ItemName itemName) {

        Transform itemToSet=null;
        foreach (Transform item in handObjects) {
            if (itemName == item.GetComponent<HandObject>().itemName) {
                itemToSet = item;
            }
            item.gameObject.SetActive(false);
        }

        itemToSet.gameObject.SetActive(true);

    }
}
      


  

