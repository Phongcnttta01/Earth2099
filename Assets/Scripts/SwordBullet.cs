using UnityEngine;

public class SwordBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private GameObject bloodPrefab;
    void Start()
    {
      //  transform.localRotation = new Quaternion(transform.localScale.x, transform.localScale.y, transform.localScale.z,0f);
        Destroy(gameObject, lifeTime);
    }

   
    void Update()
    {
        moveBullet();
    }

    void moveBullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            GameObject blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            Destroy(blood,1f);
            Destroy(gameObject);   
        }
    }
}
