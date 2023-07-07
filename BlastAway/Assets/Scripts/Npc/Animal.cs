using System;
using UnityEngine;

public class Animal : Destructible
{
    private AgentBehavior behavior;
    private Animator anim;

    private void Start()
    {
        currentHp = maxHp;
        anim = GetComponentInChildren<Animator>();
        behavior = GetComponent<AgentBehavior>();
    }

    public override void OnTakeDamage(int damage)
    {
        behavior.isHit = true;
        
        base.OnTakeDamage(damage);
    }

    protected override void OnDeath()
    {
        behavior.isDead = true;
        anim.SetTrigger("onDeath");
        
        base.OnDeath();
    }
}
