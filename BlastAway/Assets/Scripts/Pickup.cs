using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item pickupObject;

    public void OnInteract(PlayerInteract player)
    {
        player.AddToInventory(pickupObject, out var result);
        if (result)
            Destroy(this);
    }
}
