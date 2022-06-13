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
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<GameObject> enemiesOnScreen;

    private void Awake()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemies[i].count; j++)
            {
                GameObject enemyObject = Instantiate(enemies[i].prefab);
                enemiesOnScreen.Add(enemyObject);
                enemyObject.SetActive(false);
            }
        }
    }
}
