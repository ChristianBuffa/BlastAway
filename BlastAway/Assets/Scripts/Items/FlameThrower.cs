using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        if (mouseDownTimer > 150)
        {
            mouseDownTimer = 0;
            Debug.Log("over 30");
            Use(GetComponentInParent<PlayerInteract>());
        }
    }

    public override void Use(PlayerInteract player)
    {
        var hitColliders = Physics.OverlapSphere(player.actionPoint.position, 1f, LayerMask.GetMask("Destructible"));

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
