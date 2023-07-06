using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool IsAtMaxCapacity => inventory.Count >= maxCapacity;
    
    [Header("Controls")] 
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;

    [SerializeField] private float maxInteractDistance = 5f;
    [SerializeField] private int maxCapacity = 10;

    private Camera playerCamera;
    public Transform actionPoint;
    private List<Item> inventory = new List<Item>();
    private Item equippedItem;

    private void Awake()
    { 
        playerCamera = GetComponentInChildren<Camera>();
        if (inventory.Any())
            equippedItem = inventory[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
            InteractionCheck();

        if (Input.GetKeyDown(fireKey))
            UseItem();
    }

    private void InteractionCheck()
    {
        var ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Physics.Raycast(ray, out RaycastHit hit, maxInteractDistance);
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