using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool IsAtMaxCapacity => inventory.Count >= maxCapacity;
    
    [Header("Controls")] 
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;

    [SerializeField] private int maxCapacity;

    public Transform actionPoint;
    private List<Item> inventory = new List<Item>();
    private Item equippedItem;

    private void Awake()
    {
        if (inventory.Any())
            equippedItem = inventory[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
            CheckItemsForInteraction();

        if (Input.GetKeyDown(fireKey))
            UseItem();
    }

    private void CheckItemsForInteraction()
    {
        Physics.BoxCast(transform.position, new Vector3(1, 1, 1), Vector3.forward, out RaycastHit hit);
        var interactable = hit.collider.GetComponent<IInteractable>();
        interactable?.OnInteract(this);
    }

    private void UseItem()
    {
        equippedItem.Use(this);
    }

    public void AddToInventory(Item newItem)
    {
        if (inventory.Count < maxCapacity)
            inventory.Add(newItem);
    }
}