using System.Collections;

using Assets.Scripts.Enemies.CasualEnemy;

using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;          // Prefab du boss
    public float minSpawnInterval = 0.5f;  // Intervalle minimal entre chaque spawn d'ennemi
    public float maxSpawnInterval = 2.0f;  // Intervalle maximal entre chaque spawn d'ennemi
    public int enemiesPerWave = 4;         // Nombre d'ennemis par vague
    public int totalSpawnEnemy = 12;       // Nombre total d'ennemis
    public int maxSpawnEnemyParallel = 3;  // Nombre maximum d'ennemis simultanés

    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public Transform bossSpawnPoint;       // Point de spawn du boss

    private int currentSpawnedEnemies = 0;
    private int totalSpawned = 0;
    private bool spawnFromRight = true;    // Commence à spawner à droite
    private bool bossSpawned = false;      // Indique si le boss a été spawné

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (totalSpawned < totalSpawnEnemy)
        {
            // Lancer une vague d'ennemis
            for (int i = 0; i < enemiesPerWave && totalSpawned < totalSpawnEnemy; i++)
            {
                if (currentSpawnedEnemies < maxSpawnEnemyParallel)
                {
                    SpawnEnemy();
                    totalSpawned++;
                }

                // Intervalle aléatoire entre chaque ennemi dans une vague
                float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
                yield return new WaitForSeconds(randomInterval);
            }

            // Alterne le point de spawn entre droite et gauche après chaque vague
            spawnFromRight = !spawnFromRight;

            yield return new WaitForSeconds(maxSpawnInterval * 4);
        }
    }

    private void SpawnEnemy()
    {
        // Détermine le point de spawn
        Transform spawnPoint = spawnFromRight ? rightSpawnPoint : leftSpawnPoint;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        currentSpawnedEnemies++;

        // Abonnement à l'événement de destruction de l'ennemi
        newEnemy.GetComponent<StateMachineEnemy>().OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void HandleEnemyDestroyed()
    {
        currentSpawnedEnemies--;

        // Vérifie si tous les ennemis ont été spawnés et détruits
        if (totalSpawned >= totalSpawnEnemy && currentSpawnedEnemies <= 0 && !bossSpawned)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossSpawned = true;  
    }
}
