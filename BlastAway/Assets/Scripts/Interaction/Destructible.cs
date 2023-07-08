using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int maxHp;

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

    protected virtual void OnDeath()
    {
        Debug.Log("dead");
        
        Destroy(gameObject, 2f);
    }
}
