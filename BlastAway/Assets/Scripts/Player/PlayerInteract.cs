using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool IsAtMaxCapacity => inventory.Count >= maxCapacity;
    private bool ShouldInteract => Input.GetKeyDown(interactKey);
    private bool ShouldUseItem => EquippedItem != null && Input.GetButtonDown("Fire1");

    public Item EquippedItem
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
    [SerializeField] private KeyCode dropKey = KeyCode.Q;

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
        InteractionCheck();
        UseItem();
        
        if(Input.GetKeyDown(dropKey)) 
            DropItem(EquippedItem);
    }

    private void InteractionCheck()
    {
        if (ShouldInteract)
        {
            var ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            Physics.Raycast(ray, out RaycastHit hit, maxInteractDistance);
            var interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.OnInteract(this);
        }
    }

    private void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * maxInteractDistance);
        }
    }

    private void UseItem()
    {
        if (ShouldUseItem)
            EquippedItem.Use(this);
    }

    private void DropItem(Item itemToDrop)
    {
        if (CheckDropPoint(itemToDrop.pickUpPrefab))
        {
            inventory.Remove(itemToDrop);
            Instantiate(itemToDrop.pickUpPrefab, actionPoint.position, Quaternion.identity);
            if (itemToDrop == EquippedItem)
                EquippedItem = null;
        }
        else
            Debug.Log("can't drop");
    }

    private bool CheckDropPoint(GameObject itemToDrop)
    {
        var itemPrefabSize = itemToDrop.GetComponent<BoxCollider>().size;
        var boxSize = itemPrefabSize * 0.5f;
        return !Physics.BoxCast(actionPoint.position, boxSize, playerCamera.transform.forward);
    }

    private void UpdateEquip()
    {
        if (itemSocket.childCount > 0)
        {
            for (int i = 0; i < itemSocket.childCount; i++)
                Destroy(itemSocket.GetChild(i).gameObject);
        }

        if (equippedItem == null) return;
        Debug.Log("has to instantiate");
        Instantiate(equippedItem.gameObject, itemSocket);
    }

    public void AddToInventory(Item newItem)
    {
        if (inventory.Count < maxCapacity)
            inventory.Add(newItem);

        EquippedItem = newItem;
    }
}