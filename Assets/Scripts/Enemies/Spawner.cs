using System.Collections;

using Assets.Scripts.Enemies.CasualEnemy;

using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.0f; // Intervalle entre chaque spawn d'ennemi dans une vague
    public int enemiesPerWave = 4;     // Nombre d'ennemis par vague
    public int totalSpawnEnemy = 12;   // Nombre total d'ennemis
    public int maxSpawnEnemyParallel = 3; // Nombre maximum d'ennemis simultanés

    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    private int currentSpawnedEnemies = 0;
    private int totalSpawned = 0;
    private bool spawnFromRight = true; // Commence à spawner à droite

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (totalSpawned < totalSpawnEnemy)
        {
            // Lancer une vague de 4 ennemis
            for (int i = 0; i < enemiesPerWave && totalSpawned < totalSpawnEnemy; i++)
            {
                if (currentSpawnedEnemies < maxSpawnEnemyParallel)
                {
                    SpawnEnemy();
                    totalSpawned++;
                }
                yield return new WaitForSeconds(spawnInterval);
            }

            // Alterne le point de spawn entre droite et gauche après chaque vague
            spawnFromRight = !spawnFromRight;

            // Attente entre les vagues
            yield return new WaitForSeconds(spawnInterval * 4); 
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnFromRight ? rightSpawnPoint : leftSpawnPoint;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        currentSpawnedEnemies++;

        newEnemy.GetComponent<StateMachineEnemy>().OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void HandleEnemyDestroyed()
    {
        currentSpawnedEnemies--;
    }
}
