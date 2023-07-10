using UnityEngine;

public class Tree : Destructible
{
    private Rigidbody rb; 
    public GameObject fireFX;
    
    private void Start()
    {
        currentHp = maxHp;
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        CheckBurnState();
    }

    public override void OnDeath()
    {
        rb.isKinematic = false;
        AchivementManager.Instance.destroyedTrees++;
        AchivementManager.Instance.CheckTreeNumber();

        Destroy(gameObject, 20f);
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
