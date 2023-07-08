using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int maxHp;
    public bool isFlammable;

    protected bool isBurning = false;
    private int burningTimer = 0;
    protected int currentHp;

    public virtual void OnTakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDamageOverTime(int damage, int dps, int burningTime, int waitTime)
    {
        if (isFlammable)
        {
            currentHp -= damage;
            burningTimer++;
            Debug.Log(currentHp);

            if (burningTimer > waitTime)
            {
                StartCoroutine(DPS(dps, burningTime, waitTime));
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

            if (currentHp <= 0)
            {
                OnDeath();
            }
        }

        isBurning = false;
    }
    
    protected virtual void OnDeath()
    {
        Debug.Log("dead");
        
        Destroy(gameObject, 2f);
    }
}
