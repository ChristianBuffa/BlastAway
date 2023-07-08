using System;
using UnityEngine;

public class Animal : Destructible
{
    private AgentBehavior behavior;
    private Animator anim;
    public GameObject bloodFX;
    public GameObject fireFX;

    private void Start()
    {
        currentHp = maxHp;
        anim = GetComponentInChildren<Animator>();
        behavior = GetComponent<AgentBehavior>();
    }

    private void Update()
    {
        if (base.isBurning)
        {
            fireFX.SetActive(true);
        }
        else
        {
            fireFX.SetActive(false);
        }
    }

    public override void OnTakeDamage(int damage)
    {
        behavior.isHit = true;
        Instantiate(bloodFX, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1.2f, gameObject.transform.position.z), Quaternion.identity);
        
        base.OnTakeDamage(damage);
    }

    public override void OnDamageOverTime(int damage, int dps, int time, int waitTime)
    {
        behavior.isHit = true;

        base.OnDamageOverTime(damage, dps, time, waitTime);
    }

    protected override void OnDeath()
    {
        behavior.isDead = true;
        anim.SetTrigger("onDeath");
        
        base.OnDeath();
    }
}
