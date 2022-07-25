using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
    [SerializeField] private float timeToNextWave;
    [SerializeField] private float denominatorOfProgression;
    private int[] termOfProgression;
    private int previousSummOfProgression = 0;

    private float platRadiusX;
    private float platRadiusZ;

    private float timeLeft;

    private void Awake()
    {
        platRadiusX = platform.localScale.x * 5;
        platRadiusZ = platform.localScale.z * 5;

        //Start of geometrical progresion of zombie's waves
        termOfProgression = new int[enemies.Count];
        for (int i = 0; i < enemies.Count; i++)
        {
            termOfProgression[i] = enemies[i].countAtFirstWave;
        }

        CreateEnemies();
        for (int i = 0; i < enemiesOnScreen.Count; i++)
        {
            Replacement(enemiesOnScreen[i]);
            enemiesOnScreen[i].SetActive(true);
        }

        //Boxes create and replacement
        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = 0; j < boxes[i].count; j++)
            {
                GameObject box = Instantiate(boxes[i].prefab);
                Replacement(box);
                box.transform.position += Vector3.down; //костыль
            }
        }
    }

    private void Start()
    {
        StartCoroutine(WaveTimer());
    }

    private void CreateEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < termOfProgression[i]; j++)
            {
                EnemyMovement enemyMovement = enemies[i].prefab.GetComponent<EnemyMovement>();
                enemyMovement.target = hero;
                enemyMovement.zombieSounds[0] = enemySounds[Random.Range(0, 4)];
                enemyMovement.zombieSounds[1] = enemySounds[Random.Range(4, 9)];
                enemyMovement.zombieSounds[2] = enemySounds[Random.Range(9, 13)];
                GameObject enemyObject = Instantiate(enemies[i].prefab);
                enemiesOnScreen.Add(enemyObject);
                CreateSoundList(enemyObject);
                enemyObject.SetActive(false);
            }
        }
    }

    private void Replacement(GameObject gameObject)
    {
        Again:
        Vector3 pos = new Vector3(Random.Range(platform.position.x - platRadiusX, platform.position.x + platRadiusX), platform.position.y + 1, Random.Range(platform.position.z - platRadiusZ, platform.position.z + platRadiusZ));
        if (Vector3.Distance(hero.position, pos) < distanceToEnemiesSpawn)
        {
            goto Again;
        }
        gameObject.transform.position = pos;
    }

    private void CreateSoundList(GameObject gameObject)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        GameData.Instance.sounds.Add(audioSource);
    }

    private IEnumerator WaveTimer()
    {
        Again:
        for (int i = 0; i < enemies.Count; i++)
        {
            previousSummOfProgression += termOfProgression[i];
            termOfProgression[i] = Mathf.RoundToInt(termOfProgression[i] * denominatorOfProgression);
        }
        CreateEnemies();

        timeLeft = timeToNextWave;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            GameData.Instance.timeToNextWaveText.text = $"Time to next Wave: {Mathf.FloorToInt(timeLeft)}";
            yield return null;
        }
        int newWaveNum = ++GameData.Instance.currentWave;
        GameData.Instance.waveNumText.text = $"Wave: {newWaveNum}";

        for (int i = previousSummOfProgression; i < enemiesOnScreen.Count; i++)
        {
            Replacement(enemiesOnScreen[i]);
            enemiesOnScreen[i].SetActive(true);
        }

        goto Again;
    }
}
