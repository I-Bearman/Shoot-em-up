using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class Enemy
{
    public int count;
    public GameObject prefab;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform hero;
    [SerializeField] private Transform platform;
    [SerializeField] private float distanceToEnemiesSpawn;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<AudioClip> enemySounds;
    [SerializeField] private List<GameObject> enemiesOnScreen;

    private float platRadiusX;
    private float platRadiusZ;

    private void Awake()
    {
        platRadiusX = platform.localScale.x * 5;
        platRadiusZ = platform.localScale.z * 5;

        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemies[i].count; j++)
            {
                EnemyMovement enemyMovement = enemies[i].prefab.GetComponent<EnemyMovement>();
                enemyMovement.target = hero;
                enemyMovement.zombieSounds[0] = enemySounds[Random.Range(0, 4)];
                enemyMovement.zombieSounds[1] = enemySounds[Random.Range(4, 9)];
                enemyMovement.zombieSounds[2] = enemySounds[Random.Range(9, 13)];
            Again:
                Vector3 pos = new Vector3(Random.Range(platform.position.x - platRadiusX, platform.position.x + platRadiusX), platform.position.y + 1, Random.Range(platform.position.z - platRadiusZ, platform.position.z + platRadiusZ));
                if (Vector3.Distance(hero.position, pos) < distanceToEnemiesSpawn)
                {
                    goto Again;
                }
                GameObject enemyObject = Instantiate(enemies[i].prefab, pos, Quaternion.identity);
                enemiesOnScreen.Add(enemyObject);
                //enemyObject.SetActive(false);
            }
        }
    }
}
