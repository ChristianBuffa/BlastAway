using System;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    public event Action<GameObject> InRange;
    public event Action<GameObject> OutOfRange;
    [SerializeField] private GameObject itemPrefab;

    public void OnInteract(PlayerInteract player)
    {
        if (!player.IsAtMaxCapacity)
        {
            player.AddToInventory(itemPrefab.GetComponent<Item>());
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory is at Max Capacity");
        }
    }
    public void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerController>()) {
            Debug.Log("Enter");
            InRange?.Invoke(gameObject);
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerController>()) {
            Debug.Log("Exit");
            OutOfRange?.Invoke(gameObject);
        }
    }
}
