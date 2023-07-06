using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;

    [SerializeField] private int maxCapacity;
    
    public Transform actionPoint;
    private List<Item> inventory = new List<Item>();

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            CheckItemsForInteraction();
        }
    }

    private void CheckItemsForInteraction()
    {
        Physics.BoxCast(transform.position, new Vector3(1, 1, 1), Vector3.forward, out RaycastHit hit);
        var interactable = hit.collider.GetComponent<IInteractable>();
        interactable?.OnInteract(this);
    }

    public void AddToInventory(Item newItem, out bool result)
    {
        if (inventory.Count < maxCapacity)
        {
            inventory.Add(newItem);
            result = true;
        }
        else
            result = false;
    }
}