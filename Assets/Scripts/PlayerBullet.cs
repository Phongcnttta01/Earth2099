using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private GameObject bloodPrefab;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

   
    void Update()
    {
        moveBullet();
    }

    void moveBullet()
    {
        // Di chuyển sang bên phải 
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
