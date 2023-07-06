using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private GameObject itemPrefab;
    public abstract void Use(PlayerInteract player);
}
