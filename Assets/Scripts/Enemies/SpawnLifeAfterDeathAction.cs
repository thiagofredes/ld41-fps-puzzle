using UnityEngine;

public class SpawnLifeAfterDeathAction : AfterDeathAction
{
    public GameObject lifePrefab;

    public Vector3 offset;

    public override void Execute(Vector3 spawnPosition)
    {
        Instantiate(lifePrefab, spawnPosition + offset, Quaternion.identity);
    }
}