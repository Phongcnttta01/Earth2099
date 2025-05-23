using System;
using UnityEngine;

public class BasicEnemy : Enemy
{

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(enterDamage);

            }
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
}
