using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item pickupObject;

    public void OnInteract(PlayerInteract player)
    {
        if (!player.IsAtMaxCapacity)
        {
            player.AddToInventory(pickupObject);
            Destroy(this);
        }
        else
        {
            Debug.Log("Inventory is at Max Capacity");
        }
    }
}
