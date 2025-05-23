using UnityEngine;

public class ExplosionEnemy : Enemy
{
    [SerializeField] private GameObject explosionPrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                CreateExplosion();
                Camera.main.GetComponent<CameraShake>().ShakeCamera(0.3f, 0.1f);
            }
        }
    }

    private void CreateExplosion()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab,transform.position,Quaternion.identity);
           Destroy(gameObject);
        }
    }

    protected override void Die()
    {
        CreateExplosion();
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.3f, 0.1f);
        base.Die();
    }
}
