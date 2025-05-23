using System;
using UnityEngine;
using UnityEngine.UI ;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 1f; 
    protected Player player;
    [SerializeField] protected float maxHp = 50f;
    [SerializeField] protected float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] protected float enterDamage = 15f;
    [SerializeField] protected float stayDamage = 0.5f;
   
    protected virtual void Start()
    {
        player = FindFirstObjectByType<Player>();
        
        currentHp = maxHp;
        UpdateHpBar();
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }

    protected void MoveToPlayer()
    {
        if (player != null)
        {
            // Di chuyển liên tục enemy đến player 
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                enemyMoveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }

    protected void FlipEnemy()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(transform.position.x < player.transform.position.x ? 1 : -1, 1, 1);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp); // Giới hạn lượng máu có thể có là maxHp và min là 0 
        UpdateHpBar();

        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
}