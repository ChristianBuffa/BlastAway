using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject pickupObject;

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
