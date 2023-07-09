using System;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Destructible
{
    private AnimalBehavior behavior;
    public GameObject bloodFX;
    public GameObject fireFX;
    public GameObject bloodSpawnPosition;

    public List<AudioClip> voiceLines;

    private void Start()
    {
        currentHp = maxHp;
        behavior = GetComponent<AnimalBehavior>();
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
        AchivementManager.Instance.destroyedVillagers++;
        AchivementManager.Instance.CheckVillagerNumber();
        
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(voiceLines[0]);
        }
    }
}
