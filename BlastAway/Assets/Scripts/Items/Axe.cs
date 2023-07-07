using System;
using UnityEngine;

public class Axe : Item
{
    public int damage;

    public override void Use(PlayerInteract player)
    {
        
        player.EquippedItem.GetComponentInChildren<Animator>().SetTrigger("SwingTrigger");
        var hitColliders = Physics.OverlapSphere(player.actionPoint.position, 1f, LayerMask.GetMask("Destructible"));

        foreach (Collider collider in hitColliders)
        {
            Debug.Log("Axe has hit " + collider.name);

            if (collider.gameObject.GetComponent<Destructible>())
            {
                collider.gameObject.GetComponent<Destructible>().OnTakeDamage(damage);
            }
        }
    }
}
