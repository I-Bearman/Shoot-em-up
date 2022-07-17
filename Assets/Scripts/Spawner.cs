using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Enemy
{
    public int countAtFirstWave;
    public GameObject prefab;
}
[System.Serializable]
public class Boxes
{
    public int count;
    public GameObject prefab;
}

public class Spawner : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Transform hero;
    [SerializeField] private Transform platform;
    [SerializeField] private float distanceToEnemiesSpawn;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Boxes> boxes;
    [SerializeField] private List<AudioClip> enemySounds;
    [SerializeField] private List<GameObject> enemiesOnScreen;

    [Header("Waves")]
    [SerializeField] private int countOfWaves;
    [SerializeField] private float denominatorOfProgression;
    private int[] summOfProgression;

    private float platRadiusX;
    private float platRadiusZ;

    private void Awake()
    {
        platRadiusX = platform.localScale.x * 5;
        platRadiusZ = platform.localScale.z * 5;

        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemies[i].countAtFirstWave; j++)
            {
                EnemyMovement enemyMovement = enemies[i].prefab.GetComponent<EnemyMovement>();
                enemyMovement.target = hero;
                enemyMovement.zombieSounds[0] = enemySounds[Random.Range(0, 4)];
                enemyMovement.zombieSounds[1] = enemySounds[Random.Range(4, 9)];
                enemyMovement.zombieSounds[2] = enemySounds[Random.Range(9, 13)];
                Vector3 pos = Replacement();
                GameObject enemyObject = Instantiate(enemies[i].prefab, pos, Quaternion.identity);
                enemiesOnScreen.Add(enemyObject);
                CreateSoundList(enemyObject);
                //enemyObject.SetActive(false);
            }
        }

        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = 0; j < boxes[i].count; j++)
            {
                Vector3 pos = Replacement();
                pos += Vector3.down;
                Instantiate(boxes[i].prefab, pos, Quaternion.identity);
            }
        }
    }

    private Vector3 Replacement()
    {
        Again:
        Vector3 pos = new Vector3(Random.Range(platform.position.x - platRadiusX, platform.position.x + platRadiusX), platform.position.y + 1, Random.Range(platform.position.z - platRadiusZ, platform.position.z + platRadiusZ));
        if (Vector3.Distance(hero.position, pos) < distanceToEnemiesSpawn)
        {
            goto Again;
        }
        return pos;
    }

    private void Start()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            summOfProgression[i] = Mathf.RoundToInt(enemies[i].countAtFirstWave * ((1f - Mathf.Pow(denominatorOfProgression, countOfWaves)) / (1f - denominatorOfProgression)));
            Debug.Log(summOfProgression[i]);
        }
    }

    private void CreateSoundList(GameObject gameObject)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        GameData.Instance.sounds.Add(audioSource);
    }
}
