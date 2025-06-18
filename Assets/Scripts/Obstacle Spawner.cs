using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject ObstaclePrefab;
    [SerializeField] Vector3 SpawnLocation;
    [SerializeField] float SpawnInterval;
    [SerializeField] int NumberOfObstacle;
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
            Instantiate(ObstaclePrefab, SpawnLocation, Random.rotation, transform);
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
