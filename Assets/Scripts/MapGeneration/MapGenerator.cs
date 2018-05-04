using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    // what a single level chunk is like
    public GameObject levelChunk;

    public int roomWidth;

    public int roomHeight;

    // how many chunks in width the level has
    public int mapWidth;

    // how many chunks in height the level has
    public int mapHeight;

    [Range(0f, 1f)]
    public float chunkFrequency = 0.5f;

    private GameObject[][] level;

    private float mapSeed;



    void Awake()
    {
        mapSeed = (float)System.DateTime.Now.Second;
    }

    public void GenerateLevel()
    {
        float startX = Random.value * mapSeed;
        float startY = Random.value * mapSeed;
        ResetMap();
        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                float xCoord = (float)x + mapSeed + startX;
                float yCoord = (float)z + mapSeed + startY;
                float v = Mathf.PerlinNoise(xCoord, yCoord);
                if (v > chunkFrequency)
                {
                    InstantiateRoom(x, z);
                }
            }
        }
    }

    private void InstantiateRoom(int xCoord, int zCoord)
    {
        int startX = xCoord - Mathf.FloorToInt(roomWidth / 2f) - 1;
        int startY = zCoord - Mathf.FloorToInt(roomHeight / 2f) - 1;
        int endX = xCoord + Mathf.FloorToInt(roomWidth / 2f) + 1;
        int endY = zCoord + Mathf.FloorToInt(roomHeight / 2f) + 1;
        List<GameObject> instantiatedChunks = new List<GameObject>();

        if (startX > 0 && startY > 0 && endX < mapWidth && endY < mapHeight)
        {
            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    if (level[x][y] != null)
                    {
                        foreach (GameObject go in instantiatedChunks)
                        {
                            Destroy(go);
                        }
                        instantiatedChunks.Clear();
                        return;
                    }
                    else
                    {
                        level[x][y] = Instantiate(levelChunk, new Vector3(x, 0f, y), Quaternion.identity) as GameObject;
                        instantiatedChunks.Add(level[x][y]);
                    }
                }
            }
        }
    }

    private void ResetMap()
    {
        DestroyAll();
        level = new GameObject[mapWidth][];
        for (int l = 0; l < mapWidth; l++)
            level[l] = new GameObject[mapHeight];
    }

    private void DestroyAll()
    {
        if (level != null)
        {
            for (int gArray = 0; gArray < level.Length; gArray++)
            {
                for (int g = 0; g < level[gArray].Length; g++)
                {
                    DestroyImmediate(level[gArray][g]);
                }
            }
        }
    }
}
