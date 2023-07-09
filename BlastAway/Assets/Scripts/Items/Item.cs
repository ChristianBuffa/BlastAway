using UnityEngine;

public abstract class Item : MonoBehaviour {
    public GameObject pickUpPrefab;
    public abstract void Use(PlayerInteract player);
    
}
