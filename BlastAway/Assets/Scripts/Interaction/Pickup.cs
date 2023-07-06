using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item pickupData;

    public void OnInteract(PlayerInteract player)
    {
        if (!player.IsAtMaxCapacity)
        {
            player.AddToInventory(pickupData);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory is at Max Capacity");
        }
    }
}
