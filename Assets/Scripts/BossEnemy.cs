using System;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject enemyBulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedDanThuong = 20f;
    [SerializeField] private float speedDanTron = 10f;
    [SerializeField] private float hpValue = 100f;
    [SerializeField] private GameObject miniEnemy;
    [SerializeField] private float skillCoolDown = 2f;
    [SerializeField] private GameObject usbPrefab;
    private float timeNext;

    protected override void Update()
    {
        base.Update();
        if (Time.time >= timeNext)
        {
            SuDungSkill();
        }
    }

    protected override void Die()
    {
        Instantiate(usbPrefab, transform.position, Quaternion.identity);
        base.Die();
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

    private void BanDanThuong()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position);
            direction.Normalize(); // Tránh đạn bay chéo 
            GameObject bullet = Instantiate(enemyBulletPrefabs,firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(direction*speedDanThuong);
            Destroy(bullet,2f);
        }
    }

    private void BanDanVongTron()
    {
        const int bulletCount = 12;
        float angle = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 directionBullet = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle*i) , Mathf.Sin(Mathf.Deg2Rad * angle*i),0f);
            GameObject bullet = Instantiate(enemyBulletPrefabs, transform.position, Quaternion.identity);
            EnemyBullet enemybullet = bullet.AddComponent<EnemyBullet>();
            enemybullet.SetMovementDirection(directionBullet*speedDanTron);
            Destroy(bullet,2f);
        }
    }

    private void HoiMau(float hpAmount)
    {
        currentHp = Mathf.Min(currentHp+hpAmount, maxHp);
        UpdateHpBar();
    }

    private void SinhMiniEnemy()
    {
        Instantiate(miniEnemy,transform.position,Quaternion.identity);
    }

    private void DichChuyen()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        }
    }

    private void ChonSkillNgauNhien()
    {
        int rand = UnityEngine.Random.Range(0, 5);
        switch (rand)
        {
            case 0 :
                BanDanThuong(); 
                break;
            case 1 :
                BanDanVongTron();
                break;
            case 2 :
                HoiMau(hpValue);
                break;
            case 3 :
                SinhMiniEnemy();
                break;
            case 4 :
                DichChuyen();
                break;
            default :
                break;
        }
    }

    private void SuDungSkill()
    {
        timeNext =Time.time + skillCoolDown;
        ChonSkillNgauNhien();
        
    }
}
