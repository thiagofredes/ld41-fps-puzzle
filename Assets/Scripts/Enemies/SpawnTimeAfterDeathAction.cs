
using UnityEngine;

public class SpawnTimeAfterDeathAction : AfterDeathAction
{
    public GameObject timePrefab;

    public Vector3 offset;

    public override void Execute(Vector3 spawnPosition)
    {
        Instantiate(timePrefab, spawnPosition + offset, timePrefab.transform.rotation);
    }
}