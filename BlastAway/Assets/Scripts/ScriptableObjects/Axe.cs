using UnityEngine;

[CreateAssetMenu(menuName = "Items/Axe")]
public class Axe : Item
{
    public override void Use(PlayerInteract player)
    {
        var hitColliders = Physics.OverlapSphere(player.actionPoint.position, 1f, LayerMask.GetMask("Destructible"));

        foreach (Collider collider in hitColliders)
        {
            Debug.Log("Axe has hit " + collider.name);
        }
    }
}
