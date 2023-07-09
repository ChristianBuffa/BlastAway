using System;
using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour {
    public event Action<GameObject, int> OnTakeDamageEvent;
    public event Action<GameObject> OnDeathEvent;
    public int maxHp;
    public bool isFlammable;

    public int timeBeforeBurn;
    public int burningTime;

    private int burnWaitTime = 1;
    protected bool isBurning = false;
    private int burningTimer = 0;
    protected int currentHp;

    public virtual void OnTakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp > 0) {
            OnTakeDamageEvent?.Invoke(gameObject, damage);
        }
        if (currentHp <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDamageOverTime(int damage, int dps)
    {
        if (isFlammable)
        {
            currentHp -= damage;
            burningTimer++;
            Debug.Log(currentHp);
            if (currentHp > 0) {
                OnTakeDamageEvent?.Invoke(gameObject, damage);
            }

            if (burningTimer > timeBeforeBurn)
            {
                StartCoroutine(DPS(dps, burningTime, burnWaitTime));
            }

            if (currentHp <= 0)
            {
                OnDeath();
            }
        }
    }

    IEnumerator DPS(int dps, int time, int waitTime)
    {
        isBurning = true;
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < time; i++)
        {
            
            currentHp -= dps;
            yield return new WaitForSeconds(waitTime);
            Debug.Log(currentHp);
            if (currentHp > 0) {
                OnTakeDamageEvent?.Invoke(gameObject, dps);
            }

            if (currentHp <= 0)
            {
                OnDeath();
            }
        }

        isBurning = false;
    }
    
    protected virtual void OnDeath()
    {
        OnDeathEvent?.Invoke(gameObject);
        Debug.Log("dead");
        
        Destroy(gameObject, 2f);
    }
}
