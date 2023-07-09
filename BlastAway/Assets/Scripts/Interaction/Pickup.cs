using System;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{

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
}
