using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] ObstaclePrefabs;
    [SerializeField] Vector3 SpawnLocation;
    [SerializeField] float SpawnInterval;
    [SerializeField] int NumberOfObstacle;
    [SerializeField] float XRange = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    // Update is called once per frame
    IEnumerator SpawnObstacle() 
    {   
        while (true)
        {

            int Index = Random.Range(0, ObstaclePrefabs.Length);
            SpawnLocation.x = Random.Range(-XRange, XRange);
            Instantiate(ObstaclePrefabs[Index], SpawnLocation, Random.rotation, transform);
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
