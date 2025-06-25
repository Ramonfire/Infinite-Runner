using NUnit.Framework;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject FencePrefab;
    [SerializeField] GameObject HealthPrefab;
    [SerializeField] GameObject CoinPrefab;
    [SerializeField] float[] Lanes = { -3f,0,3f };
    [SerializeField] float appleSpawnChace = 0.3f;
    [SerializeField] float CoinSpawnChance = 0.5f;
    [SerializeField] int maxCoins = 10;
    bool ChunkIsOccupied = false;
    int safeLane = 1;// middle lane is the safe lane by default
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        SpawnFence();
        SpawnApple();
        SpawnCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnFence()
    {
        safeLane = Random.Range(0, Lanes.Length);//choose a random safe lane
        int spawnOneOrNoFence = Random.Range(0, 3);// Get a Random Number of fence 1,2 or none

        int FenceCount = 0;//How many fences we Instantiated

        for (int i = 0; i < Lanes.Length; i++)
        {

            if (i == safeLane)// dont spawn a fence if it s the designated safe lane
                continue;

            InstantiatePrefab(i, FencePrefab);
            FenceCount++;

            if (FenceCount == spawnOneOrNoFence)// If we are the max number of fences then we can safely leave the loop
                break;
        }
    }
    private void SpawnApple()
    {
        if (Random.value > appleSpawnChace)
            return;
        InstantiatePrefab(safeLane, HealthPrefab);
        ChunkIsOccupied = true;
    }

    private void SpawnCoins()
    {
        if (Random.value > CoinSpawnChance || ChunkIsOccupied) // if we spawn an apple then dont spawn coins
            return;

        int zOffset = -3;
        for (int i = 0; i < Random.Range(0,maxCoins); i++)
        {
            zOffset++; ;
            InstantiatePrefab(safeLane, CoinPrefab,zOffset);
        }
    }


    private void InstantiatePrefab(int laneIndex,GameObject Prefab,float zOffset = 0)
    {
        Vector3 coords = new Vector3(Lanes[laneIndex], transform.position.y, transform.position.z+zOffset);
        Instantiate(Prefab, coords, Quaternion.identity, transform);
    }

  



}


