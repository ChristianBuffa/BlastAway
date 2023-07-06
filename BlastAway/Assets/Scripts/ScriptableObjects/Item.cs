using UnityEngine;

public abstract class Item : ScriptableObject
{
    public GameObject itemPrefab;
    public GameObject pickUpPrefab;
    public abstract void Use(PlayerInteract player);
}
