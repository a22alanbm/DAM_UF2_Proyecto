using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject enemy0;
    [SerializeField] public GameObject enemy1;
    [SerializeField] public GameObject enemy2;
    [SerializeField] public GameObject enemy3;
    [SerializeField] public GameObject enemy4;

    [SerializeField] public DataManager dataManager;

    [SerializeField] public int maxEnemiesLVL0 = 5;
    [SerializeField] public int maxEnemiesLVL1 = 4;
    [SerializeField] public int maxEnemiesLVL2 = 3;
    [SerializeField] public int maxEnemiesLVL3 = 2;
    [SerializeField] public int maxEnemiesLVL4 = 1;

    [SerializeField] public float spawnDelay = 2f;
    [SerializeField] public float spawnRangeX = 10f;

    private int currentEnemiesLVL0 = 0;
    private int currentEnemiesLVL1 = 0;
    private int currentEnemiesLVL2 = 0;
    private int currentEnemiesLVL3 = 0;
    private int currentEnemiesLVL4 = 0;

    private bool spawneando = false;

    void Start()
    {
        dataManager = DataManager.Instance;
        StartCoroutine(SpawnEnemies());
    }

    void Update(){
        if(! spawneando){
            StartCoroutine(SpawnEnemies());
        }
    }


    IEnumerator SpawnEnemies(){
        spawneando = true;
        yield return new WaitForSeconds(spawnDelay);
        SpawnEnemiesForLevel0();
        SpawnEnemiesForLevel1();
        SpawnEnemiesForLevel2();
        SpawnEnemiesForLevel3();
        SpawnEnemiesForLevel4();
        spawneando = false;
    }



    void SpawnEnemiesForLevel0()
    {
        int level = dataManager.GetLevel();
        if (level == 0)
        {
            if (currentEnemiesLVL0 < maxEnemiesLVL0)
            {
                currentEnemiesLVL0++;
                SpawnEnemy(enemy0);
            }
        }
    }

    void SpawnEnemiesForLevel1()
    {
        int level = dataManager.GetLevel();
        if (level == 1)
        {
            if (currentEnemiesLVL1 < maxEnemiesLVL1)
            {
                currentEnemiesLVL1++;
                SpawnEnemy(enemy1);
            }
        }
    }

    void SpawnEnemiesForLevel2()
    {
        int level = dataManager.GetLevel();
        if (level == 2)
        {
            if (currentEnemiesLVL2 < maxEnemiesLVL2)
            {
                currentEnemiesLVL2++;
                SpawnEnemy(enemy2);
            }
        }
    }

    void SpawnEnemiesForLevel3()
    {
        int level = dataManager.GetLevel();
        if (level == 3)
        {
            if (currentEnemiesLVL3 < maxEnemiesLVL3)
            {
                currentEnemiesLVL3++;
                SpawnEnemy(enemy3);
            }
        }
    }

    void SpawnEnemiesForLevel4()
    {
        int level = dataManager.GetLevel();
        if (level == 4)
        {
            if (currentEnemiesLVL4 < maxEnemiesLVL4)
            {
                currentEnemiesLVL4++;
                SpawnEnemy(enemy4);
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        float randomX = transform.position.x + Random.Range(-spawnRangeX, spawnRangeX);
        GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(randomX, transform.position.y, transform.position.z), Quaternion.identity);
        newEnemy.GetComponent<EnemyController>().SetSpawner(this);
    }

    public void EnemyDestroyed(int enemyType)
    {
        switch (enemyType)
        {
            case 0:
                currentEnemiesLVL0--;
                break;
            case 1:
                currentEnemiesLVL1--;
                break;
            case 2:
                currentEnemiesLVL2--;
                break;
            case 3:
                currentEnemiesLVL3--;
                break;
            case 4:
                currentEnemiesLVL4--;
                break;
        }
    }

}


