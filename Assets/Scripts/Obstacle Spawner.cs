using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] ObstaclePrefabs;
    [SerializeField] Vector3 SpawnLocation;
    [SerializeField] float SpawnInterval;
    [SerializeField] int NumberOfObstacle;
    [SerializeField] float XRange = 3f;
    [SerializeField] float[] Lanes = { -2f, 0, 2f };
    Transform player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


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
            SpawnLocationFromPlayerPosition();

            int Index = Random.Range(0, ObstaclePrefabs.Length);
            Instantiate(ObstaclePrefabs[Index], SpawnLocation, Random.rotation, transform);
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    private void SpawnLocationFromPlayerPosition()
    {
        Vector3 playerPosition = player.position;
        if (playerPosition.x < -1)
        {
            SpawnLocation.x = Lanes[0];
        }
        else if (playerPosition.x < 1)
        {
            SpawnLocation.x = Lanes[1];
        }
        else
        {
            SpawnLocation.x = Lanes[2];
        }
    }
}
