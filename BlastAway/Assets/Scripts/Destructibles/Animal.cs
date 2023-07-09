using System;
using UnityEngine;

public class Animal : Destructible
{
    private AgentBehavior behavior;
    private Animator anim;
    public GameObject bloodFX;
    public GameObject fireFX;
    public GameObject bloodSpawnPosition;

    private void Start()
    {
        currentHp = maxHp;
        anim = GetComponentInChildren<Animator>();
        behavior = GetComponent<AgentBehavior>();
    }

    private void Update()
    {
        CheckBurnState();
    }

    public override void OnTakeDamage(int damage)
    {
        behavior.isHit = true;
        Instantiate(bloodFX, bloodSpawnPosition.transform.position, Quaternion.identity);
        
        base.OnTakeDamage(damage);
    }

    public override void OnDamageOverTime(int damage, int dps)
    {
        behavior.isHit = true;

        base.OnDamageOverTime(damage, dps);
    }

    protected override void OnDeath()
    {
        behavior.isDead = true;
        anim.SetTrigger("onDeath");
        AchivementManager.Instance.destroyedAnimals++;
        AchivementManager.Instance.CheckAnimalNumber();
        
        base.OnDeath();
    }

    private void CheckBurnState()
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
}
