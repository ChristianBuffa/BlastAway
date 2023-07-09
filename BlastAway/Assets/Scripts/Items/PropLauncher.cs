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

    [SerializeField] private Transform launchPoint;
    [SerializeField] private float launchForce = 1000f;

    private GameObject equippedProp;

    public override void Use(PlayerInteract player)
    {
        if (EquippedProp == null)
        {
            var hitColliders = Physics.OverlapSphere(player.actionPoint.position, 3f, LayerMask.GetMask("Destructible"));
            var target = hitColliders[0].gameObject;
            EquippedProp = target;
            Destroy(target);
        }
        else if(EquippedProp != null)
        {
            EquippedProp.SetActive(false);
            var launchable = Instantiate(EquippedProp, launchPoint.position, Quaternion.identity);
            EquippedProp = null;
            launchable.GetComponent<Rigidbody>().AddForce(player.actionPoint.forward * launchForce);
        }
    }

    private void UpdateEquip()
    {
        if (launchPoint.childCount > 0)
        {
            for (int i = 0; i < launchPoint.childCount; i++)
                Destroy(launchPoint.GetChild(i).gameObject);
        }

        if (EquippedProp == null) return;
        Instantiate(EquippedProp.gameObject, launchPoint);
    }
}
