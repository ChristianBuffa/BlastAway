using UnityEngine;

public class PropLauncher : Item
{
    private GameObject EquippedProp
    {
        get => equippedProp;
        set
        {
            equippedProp = value;
            UpdateEquip();
        }
    }

    [SerializeField] private float launchForce = 30f;

    private GameObject equippedProp;
    private GameObject launchableProp;

    public override void Use(PlayerInteract player)
    {
        if (EquippedProp == null)
        {
            var hitColliders =
                Physics.OverlapSphere(player.actionPoint.position, 3f, LayerMask.GetMask("Destructible"));
            if (hitColliders.Length > 0)
            {
                var target = hitColliders[0].gameObject;
                target.GetComponent<Rigidbody>().isKinematic = true;
                EquippedProp = Instantiate(target, player.actionPoint.position, Quaternion.identity, player.actionPoint);
                Destroy(target);
            }
        }
        else if (EquippedProp != null)
        {
            var scripts = launchableProp.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.enabled = false;
            }
            if (launchableProp.GetComponent<Rigidbody>().isKinematic)
                launchableProp.GetComponent<Rigidbody>().isKinematic = false;
            
            launchableProp.transform.parent = null;
            launchableProp.GetComponent<Rigidbody>().AddForce(player.transform.forward * launchForce, ForceMode.VelocityChange);
            EquippedProp = null;
        }
    }

    private void UpdateEquip()
    {
        if (EquippedProp != null)
            launchableProp = EquippedProp;
    }
}