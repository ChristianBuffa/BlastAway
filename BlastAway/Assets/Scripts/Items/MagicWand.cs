using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MagicWand : Item
{
    [SerializeField] private float rayMaxDistance = 100f;

    [Header("Magic Settings")] 
    [SerializeField] private float enlargeMultiplier = 10;
    [SerializeField] private float minimizeMultiplier = 0.1f;
    [SerializeField] private GameObject chickenPrefab;

    private List<Action<Transform>> methodPool;

    private void Awake()
    {
        methodPool = new List<Action<Transform>>
        {
            Enlarge,
            Minimize,
            ChickenForm,
            DuxForm
        };
    }

    public override void Use(PlayerInteract player)
    {
        var ray = new Ray(player.actionPoint.position, player.transform.forward);
        var hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, rayMaxDistance, LayerMask.GetMask("Destructible"));

        if (hasHit)
        {
            var random = new Random();
            var index = random.Next(methodPool.Count);
            methodPool[index].Invoke(hitInfo.transform);
        }
    }

    private void Enlarge(Transform target)
    {
        target.localScale *= enlargeMultiplier;
    }

    private void Minimize(Transform target)
    {
        target.localScale *= minimizeMultiplier;
    }

    private void ChickenForm(Transform target)
    {
        Instantiate(chickenPrefab, target.position, Quaternion.identity);
        Destroy(target.gameObject);
    }
    
    private void DuxForm(Transform target)
    {
        target.localScale *= -1;
    }
}