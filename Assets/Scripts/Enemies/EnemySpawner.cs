using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : BaseGameObject
{
    // helper class to aid in specifing thing in inspector
    [System.Serializable]
    public class EnemySpawnerRestriction
    {
        public PuzzlePiece piece;
        public int number;
    }

    public EnemySpawnerRestriction[] spawnRestrictions;

    public Transform[] spawnPoints;

    public GameObject enemyPrefab;

    public GameObject ammoSpawnerPrefab;

    public GameObject healthSpawnerPrefab;

    public GameObject puzzlePieceSpawnerPrefab;

    private Dictionary<PuzzlePiece, int> _enemiesAlive;

    private Dictionary<PuzzlePiece, int> _enemiesRestriction;


    void Awake()
    {
        _enemiesRestriction = new Dictionary<PuzzlePiece, int>();
        _enemiesAlive = new Dictionary<PuzzlePiece, int>();
        foreach (EnemySpawnerRestriction spawnRestriction in spawnRestrictions)
        {
            _enemiesRestriction.Add(spawnRestriction.piece, spawnRestriction.number);
            if (!_enemiesAlive.ContainsKey(spawnRestriction.piece))
                _enemiesAlive.Add(spawnRestriction.piece, 0);
        }
    }


    public void SpawnInit()
    {
        // spawns all enemies from the restrictions
        foreach (KeyValuePair<PuzzlePiece, int> restriction in _enemiesRestriction)
        {
            for (int e = 0; e < restriction.Value; e++)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoints[ChooseSpawnPoint()].position, Quaternion.identity);
                GameObject puzzlePc = Instantiate(puzzlePieceSpawnerPrefab, enemy.transform.position, Quaternion.identity);
                ExecutePuzzleStepAfterDeathAction puzzleAfterAction;
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                puzzlePc.transform.parent = enemy.transform;
                puzzlePc.transform.localPosition = Vector3.zero;
                puzzleAfterAction = puzzlePc.GetComponent<ExecutePuzzleStepAfterDeathAction>();
                puzzleAfterAction.piece = restriction.Key;
                enemyController.afterDeath = puzzleAfterAction;
                enemyController.EnemyKilled += OnEnemyKilled;

                _enemiesAlive[restriction.Key]++;
            }
        }

        // spawns more random enemies

    }

    // called over time to spawn random enemies
    public void Spawn()
    {

    }

    private void OnEnemyKilled(EnemyController enemy)
    {
        if (!gameEnded && !gamePaused)
        {
            ExecutePuzzleStepAfterDeathAction puzzlePiece = enemy.afterDeath as ExecutePuzzleStepAfterDeathAction;

            enemy.EnemyKilled -= OnEnemyKilled;
            _enemiesAlive[puzzlePiece.piece]--;

            if (puzzlePiece != null)
            {
                if (_enemiesAlive[puzzlePiece.piece] < _enemiesRestriction[puzzlePiece.piece])
                {
                    GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[ChooseSpawnPoint()].position, Quaternion.identity);
                    GameObject puzzlePc = Instantiate(puzzlePieceSpawnerPrefab, newEnemy.transform.position, Quaternion.identity);
                    ExecutePuzzleStepAfterDeathAction puzzleAfterAction;
                    EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
                    puzzlePc.transform.parent = newEnemy.transform;
                    puzzlePc.transform.localPosition = Vector3.zero;
                    puzzleAfterAction = puzzlePc.GetComponent<ExecutePuzzleStepAfterDeathAction>();
                    puzzleAfterAction.piece = puzzlePiece.piece;
                    enemyController.afterDeath = puzzleAfterAction;
                    enemyController.EnemyKilled += OnEnemyKilled;

                    _enemiesAlive[puzzlePiece.piece]++;
                }
            }
        }
    }

    private int ChooseSpawnPoint()
    {
        int index = 0;
        float largestAngle = 0f;

        for (int sp = 0; sp < spawnPoints.Length; sp++)
        {
            Vector3 playerSpawnPointVector = spawnPoints[sp].position - FPSPlayerController.FPSPlayerInstance.transform.position;
            float angle = Vector3.Angle(FPSPlayerController.FPSPlayerInstance.transform.forward, playerSpawnPointVector);

            if (angle > largestAngle)
            {
                largestAngle = angle;
                index = sp;
            }
        }
        return index;
    }
}