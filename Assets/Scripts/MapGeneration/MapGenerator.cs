using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    // what a single level chunk is like
    public GameObject levelChunk;

    public float levelChunkWidth;

    public float levelChunkHeight;

    // how many chunks in width the level has
    public int mapWidth;

    // how many chunks in height the level has
    public int mapHeight;

    [Range(0f, 1f)]
    public float chunkFrequency = 0.5f;

    private GameObject[][] level;

    private float mapSeed;



    void Awake(){
        mapSeed = (float)System.DateTime.Now.Second;
    }

    public void GenerateLevel()
    {
        float startX = Random.value * mapSeed;
        float startY = Random.value * mapSeed;
        ResetMap();
        for (int x = 0; x < mapWidth; x++)
        {
            level[x] = new GameObject[mapHeight];
            for (int z = 0; z < mapHeight; z++)
            {
                float xCoord = (float)x + mapSeed + startX;
                float yCoord = (float)z + mapSeed + startY;
                float v = Mathf.PerlinNoise(xCoord, yCoord);
                if (v > chunkFrequency)
                {
                    level[x][z] = Instantiate(levelChunk, new Vector3(x * levelChunkWidth, 0f, z * levelChunkHeight), Quaternion.identity) as GameObject;
                }
            }
        }
    }

    private void ResetMap()
    {
        DestroyAll();        
        level = new GameObject[mapWidth][];
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
