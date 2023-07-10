using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Animal : Destructible
{
    private AnimalBehavior behavior;
    private Animator anim;
    public GameObject bloodFX;
    public GameObject fireFX;
    public GameObject bloodSpawnPosition;

    private float sfxTimer;
    
    public List<AudioClip> voiceLines;

    private void Start()
    {
        sfxTimer = 0;
        currentHp = maxHp;
        anim = GetComponentInChildren<Animator>();
        behavior = GetComponent<AnimalBehavior>();
    }

    private void Update()
    {
        CheckBurnState();

        sfxTimer++;
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
    
    private AudioClip GetRandomVoiceLine()
    {
        int index = voiceLines.Count;

        var random = new Random();
        int i = random.Next(voiceLines.Count);

        return voiceLines[i];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sfxTimer > 2000)
        {
            sfxTimer = 0;
            AudioManager.Instance.PlaySound(GetRandomVoiceLine());
        }
    }
}
