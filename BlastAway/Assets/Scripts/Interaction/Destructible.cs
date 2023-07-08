using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int maxHp;
    public bool isFlammable;

    private bool isBurning = false;
    private int burningTimer = 0;
    protected int currentHp;

    public virtual void OnTakeDamage(int damage)
    {
        Debug.Log( "Damage " + damage);
        currentHp -= damage;
        Debug.Log( "hp " + currentHp);

        if (currentHp <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDamageOverTime(int damage, int dps, int time, int waitTime)
    {
        if (isFlammable)
        {
            currentHp -= damage;
            burningTimer++;
            Debug.Log(currentHp);

            if (burningTimer > 3)
            {
                Debug.Log("burning");
                StartCoroutine(DPS(dps, time, waitTime));
            }
        }
    }

    IEnumerator DPS(int dps, int time, int waitTime)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < time; i++)
        {
            currentHp -= dps;
            yield return new WaitForSeconds(waitTime);

            if (currentHp <= 0)
            {
                OnDeath();
            }
        }
    }
    
    protected virtual void OnDeath()
    {
        Debug.Log("dead");
        
        Destroy(gameObject, 2f);
    }
}
