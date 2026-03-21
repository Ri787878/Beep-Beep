using System;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public GameObject[] pathPrefabs; // Array of different road pieces
    public float tileLength = 8.5f; // Length of each tile
    public Transform playerTransform;
    public float spawnZ = 0.0f; // Where the next tile starts
    public int amountOfTiles = 10; // How many tiles on screen
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        float originalSpawnZ = spawnZ;
        for (int i = 0; i < amountOfTiles; i++)
        {
            SpawnTile(Vector3.forward * spawnZ);
            spawnZ += tileLength;
        }
        spawnZ = originalSpawnZ;
    }

    void Update()
    {
        MoveTiles();
        
        if(PlayerIsNearEndOfTile())
        {
            var position = Vector3.forward * (spawnZ + tileLength * (amountOfTiles - 1));
            SpawnTile(position);
            DeleteOldTile();
        }
    }

    public void SpawnTile(Vector3 position)
    {
        var index = UnityEngine.Random.Range(0, pathPrefabs.Length - 1);
        GameObject go = Instantiate(pathPrefabs[index], position, Quaternion.identity);
        activeTiles.Add(go);
    }

    private void DeleteOldTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    
    private bool PlayerIsNearEndOfTile()
    {
        var lastTileEndZ = activeTiles[0].transform.position.z;
        return playerTransform.position.z - tileLength > lastTileEndZ;
    }
    
    private void MoveTiles()
    {
        foreach (var tile in activeTiles)
        {
            tile.transform.Translate(Vector3.back * (Time.deltaTime * 5));
        }
    }
}
