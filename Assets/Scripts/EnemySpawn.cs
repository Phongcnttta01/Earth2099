using UnityEngine;
using System.Collections;
public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnTime = 2f;

    void Start()
    {
             StartCoroutine(SpawnEnemy()); // Bắt đầu trình tự 
    }

    

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
                yield return new WaitForSeconds(spawnTime); // Đợi thời gian spawn

                GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            
        }
    }
}
