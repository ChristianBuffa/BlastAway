using UnityEngine;

public class Axe : Item
{
    public override void Use(PlayerInteract player)
    {
        player.EquippedItem.GetComponent<Animator>().SetTrigger("SwingTrigger");
        var hitColliders = Physics.OverlapSphere(player.actionPoint.position, 1f, LayerMask.GetMask("Destructible"));

        foreach (Collider collider in hitColliders)
        {
            Debug.Log("Axe has hit " + collider.name);
        }
    }
}
