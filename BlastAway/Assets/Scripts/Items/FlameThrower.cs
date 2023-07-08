using UnityEngine;

public class FlameThrower : Item
{
    public int damage;
    public int dps;
    public int waitTime;
    public int burningTime;

    public GameObject fireParticle;

    private float mouseDownTimer = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            fireParticle.SetActive(true);
            mouseDownTimer++;
        }
        else
        {
            fireParticle.SetActive(false);
        }

        if (mouseDownTimer > 100)
        {
            mouseDownTimer = 0;
            Use(GetComponentInParent<PlayerInteract>());
        }
    }

    public override void Use(PlayerInteract player)
    {
        var hitColliders = Physics.OverlapSphere(player.actionPoint.position, 2f, LayerMask.GetMask("Destructible"));

        foreach (Collider collider in hitColliders)
        {
            Debug.Log("Flame thrower hit " + collider.name);

            if (collider.gameObject.GetComponent<Destructible>())
            {
                collider.gameObject.GetComponent<Destructible>().OnDamageOverTime(damage, dps, burningTime, waitTime);
            }
        }
    }
}
