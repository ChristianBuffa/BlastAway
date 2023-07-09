using UnityEngine;

public class Axe : Item
{
    public int damage;

    private Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim = GetComponent<Animator>();
            anim.SetTrigger("SwingTrigger");
        }
    }

    public override void Use(PlayerInteract player)
    {
        var hitColliders = Physics.OverlapSphere(player.actionPoint.position, 1f, LayerMask.GetMask("Destructible"));

        foreach (Collider collider in hitColliders)
        {
            Debug.Log("Axe has hit " + collider.name);

            if (collider.gameObject.GetComponent<Destructible>())
            {
                collider.gameObject.GetComponent<Destructible>().OnTakeDamage(damage);
            }
        }
    }
}
