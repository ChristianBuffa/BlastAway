using UnityEngine;

[CreateAssetMenu(menuName = "Items/Axe")]
public class Axe : Item
{
    [SerializeField] LayerMask woodMask = LayerMask.GetMask("Wood");
    
    public override void Use(PlayerInteract player)
    {
        var hitColliders = Physics.OverlapSphere(player.actionPoint.position, 1f, woodMask);
        
        foreach (Collider collider in hitColliders)
        {
            // Check if the object has a IDestructable component attached
        }
    }
}
