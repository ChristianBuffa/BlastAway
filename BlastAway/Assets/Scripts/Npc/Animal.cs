using System;
using UnityEngine;

public class Animal : Destructible
{
    private AgentBehavior behavior;
    private Animator anim;
    public GameObject bloodFX;

    private void Start()
    {
        currentHp = maxHp;
        anim = GetComponentInChildren<Animator>();
        behavior = GetComponent<AgentBehavior>();
    }

    public override void OnTakeDamage(int damage)
    {
        behavior.isHit = true;
        Instantiate(bloodFX, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1.2f, gameObject.transform.position.z), Quaternion.identity);
        
        base.OnTakeDamage(damage);
    }

    protected override void OnDeath()
    {
        behavior.isDead = true;
        anim.SetTrigger("onDeath");
        
        base.OnDeath();
    }
}
