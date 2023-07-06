using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool IsAtMaxCapacity => inventory.Count >= maxCapacity;

    private Item EquippedItem
    {
        get => equippedItem;
        set
        { 
            equippedItem = value;
            UpdateEquip();
        }
    }

    [Header("Controls")] 
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode releaseKey = KeyCode.Q;

    [SerializeField] private float maxInteractDistance = 5f;
    [SerializeField] private int maxCapacity = 10;

    [SerializeField] private Transform itemSocket;

    private Camera playerCamera;
    public Transform actionPoint;
    private List<Item> inventory = new List<Item>();
    private Item equippedItem;

    private void Awake()
    { 
        playerCamera = GetComponentInChildren<Camera>();
        if (inventory.Any())
            EquippedItem = inventory[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
            InteractionCheck();

        if (equippedItem != null)
        {
            if (Input.GetButtonDown("Fire1"))
                UseItem();

            if (Input.GetKeyDown(releaseKey))
                ReleaseEquipped();
        }
    }

    private void InteractionCheck()
    {
        var ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Physics.Raycast(ray, out RaycastHit hit, maxInteractDistance);
        var interactable = hit.collider.GetComponent<IInteractable>();
        interactable?.OnInteract(this);
    }
    
    private void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            // Draw a Gizmo representation of the raycast
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * maxInteractDistance);
        }
    }

    private void UseItem()
    {
        EquippedItem.Use(this);
    }

    private void ReleaseEquipped()
    {
        inventory.Remove(EquippedItem);
        
        // add check for other objects before releasing
        Instantiate(EquippedItem.pickUpPrefab, actionPoint.position, Quaternion.identity);
            
        EquippedItem = null;
    }

    private void UpdateEquip()
    {
        if (itemSocket.childCount > 0)
        {
            for(int i = 0; i < itemSocket.childCount; i++) 
                Destroy(itemSocket.GetChild(i).gameObject);
        }

        if (equippedItem == null) return;
        Instantiate(equippedItem.itemPrefab, itemSocket);
    }

    public void AddToInventory(Item newItem)
    {
        if (inventory.Count < maxCapacity)
            inventory.Add(newItem);
        
        EquippedItem = newItem;
    }
}