using UnityEngine;

public class Explosion : MonoBehaviour
{
   [SerializeField] private float damage = 25f;
  
   private void OnTriggerEnter2D(Collider2D collision)
   {
      Player player = collision.gameObject.GetComponent<Player>();
      Enemy enemy = collision.gameObject.GetComponent<Enemy>();
      if (collision.CompareTag("Player"))
      {
         player.TakeDamage(damage);
      }
      if (collision.CompareTag("Enemy"))
      {
         enemy.TakeDamage(damage);
      }
   
   }

   public void DestroyExplosion()
   {
      Destroy(gameObject);
   }
}
