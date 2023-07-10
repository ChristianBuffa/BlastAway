using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Npc : Destructible
{
    private NpcBehaviour behavior;
    public GameObject bloodFX;
    public GameObject fireFX;
    public GameObject bloodSpawnPosition;

    public List<AudioClip> voiceLines;

    private void Start()
    {
        currentHp = maxHp;
        behavior = GetComponent<NpcBehaviour>();
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

    public override void OnDeath()
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

    private AudioClip GetRandomVoiceLine()
    {
        int index = voiceLines.Count;

        var random = new Random();
        int i = random.Next(voiceLines.Count);

        return voiceLines[i];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(GetRandomVoiceLine());
        }
    }
}
