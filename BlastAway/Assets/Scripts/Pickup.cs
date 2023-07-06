using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item pickupObject;

    public void OnInteract(PlayerInteract player)
    {
        if (!player.IsAtMaxCapacity)
        {
            Debug.Log("picking up");
            player.AddToInventory(pickupObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory is at Max Capacity");
        }
    }
}
