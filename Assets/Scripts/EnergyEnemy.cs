using UnityEngine;

public class EnergyEnemy : Enemy
{
    [SerializeField] private GameObject energyObject;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(player != null)
                player.TakeDamage(enterDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(stayDamage);
            }
                    
        }
    }

    protected override void Die()
    {
        if (energyObject != null)
        {
           GameObject eneegy =   Instantiate(energyObject, transform.position, Quaternion.identity);
           Destroy(eneegy, 5f);
        }
        base.Die();
    }
}
