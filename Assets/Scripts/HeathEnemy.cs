using UnityEngine;

public class HeathEnemy : Enemy
{
    [SerializeField] private float healValue = 10f;

    protected override void Start()
    {
        base.Start();
       
        
    }
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

    private void HealPlayer()
    {
        if (player != null)
        {
            player.Heal(healValue);
        }
    }
    protected override void Die()
    {
        HealPlayer();
        base.Die();
    }
}
