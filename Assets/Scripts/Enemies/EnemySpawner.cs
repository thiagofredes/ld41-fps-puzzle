using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : BaseGameObject
{
    public float spawnSeconds = 4f;

    public Transform[] spawnPoints;

    public GameObject enemyPrefab;

    public GameObject lifePickupPrefab;

    public GameObject puzzlePieceSpawnerPrefab;

    public GameObject puzzlePieceParticles;

    public int maxAllowedEnemies = 20;

    private Dictionary<PuzzlePiece, int> _enemiesRestriction;

    private Coroutine _spawnCoroutine = null;

    private int _currentAliveEnemies;



    void Awake()
    {
        _currentAliveEnemies = 0;
    }

    public void Spawn(PuzzlePiece[] levelRestrictions)
    {
        _enemiesRestriction = new Dictionary<PuzzlePiece, int>();

        foreach (PuzzlePiece pPiece in levelRestrictions)
        {
            if (!_enemiesRestriction.ContainsKey(pPiece))
            {
                _enemiesRestriction.Add(pPiece, 1);
            }
            else
            {
                _enemiesRestriction[pPiece]++;
            }
            SpawnPuzzleEnemies();
            SpawnSimpleEnemies();
        }

        if (_spawnCoroutine == null)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    private void OnEnemyKilled(EnemyController enemy)
    {
        _currentAliveEnemies--;
        enemy.EnemyKilled -= OnEnemyKilled;
    }

    private void SpawnPuzzleEnemies()
    {
        foreach (PuzzlePiece pPiece in _enemiesRestriction.Keys)
        {
            if (_currentAliveEnemies < maxAllowedEnemies)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoints[ChooseSpawnPoint()].position, Quaternion.identity);
                GameObject puzzlePc = Instantiate(puzzlePieceSpawnerPrefab, enemy.transform.position, Quaternion.identity);
                GameObject pieceParticles = Instantiate(puzzlePieceParticles, enemy.transform.position, Quaternion.identity);
                ExecutePuzzleStepAfterDeathAction puzzleAfterAction;
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                ParticleSystem.MainModule particlesMainModule = pieceParticles.GetComponent<ParticleSystem>().main;
                particlesMainModule.startColor = pPiece.firewallRule;
                pieceParticles.transform.parent = enemy.transform;
                puzzlePc.transform.parent = enemy.transform;
                puzzlePc.transform.localPosition = Vector3.zero;
                puzzleAfterAction = puzzlePc.GetComponent<ExecutePuzzleStepAfterDeathAction>();
                puzzleAfterAction.piece = pPiece;
                enemyController.afterDeath = puzzleAfterAction;
                enemyController.EnemyKilled += OnEnemyKilled;
                enemyController.shotProbability = Random.Range(0.4f, 0.8f);
                enemyController.shotAccuracy = Random.Range(0.3f, 0.6f);
                _currentAliveEnemies++;
            }
        }
    }

    private void SpawnSimpleEnemies()
    {
        int num = Mathf.Min(2, maxAllowedEnemies - _currentAliveEnemies);
        for (int e = 0; e < num; e++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[ChooseSpawnPoint()].position, Quaternion.identity);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            SpawnLifeAfterDeathAction healthAfterAction = enemy.AddComponent<SpawnLifeAfterDeathAction>();
            healthAfterAction.lifePrefab = lifePickupPrefab;
            enemyController.afterDeath = healthAfterAction;
            enemyController.shotProbability = Random.Range(0.3f, 0.7f);
            enemyController.shotAccuracy = Random.Range(0.3f, 0.6f);
            _currentAliveEnemies++;
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        YieldInstruction waitSeconds = new WaitForSeconds(spawnSeconds);

        yield return waitSeconds;

        while (true && !gameEnded && !gamePaused)
        {
            yield return waitSeconds;
            SpawnPuzzleEnemies();
        }
    }

    private int ChooseSpawnPoint()
    {
        // int index = 0;
        // float minimumAngle = 60f;

        // for (int sp = 0; sp < spawnPoints.Length; sp++)
        // {
        //     Vector3 playerSpawnPointVector = spawnPoints[sp].position - FPSPlayerController.FPSPlayerInstance.transform.position;
        //     float angle = Vector3.Angle(FPSPlayerController.FPSPlayerInstance.transform.forward, playerSpawnPointVector);

        //     if (angle > minimumAngle)
        //     {
        //         index = sp;
        //     }
        // }
        // return index;

        return Mathf.FloorToInt(Random.value * spawnPoints.Length);
    }
}