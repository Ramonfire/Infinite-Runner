using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject FencePrefab;
    [SerializeField] float[] Lanes = { -2.5f,0,2.5f };
    [SerializeField] int safeLane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnFence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnFence()
    {
        safeLane = Mathf.RoundToInt(Random.Range(0, Lanes.Length));//choose a random safe lane

        for (int i = 0; i < Lanes.Length; i++)
        {

            if (i == safeLane)
                continue;

            Vector3 coords = new Vector3(Lanes[i], transform.position.y, transform.position.z);
            Instantiate(FencePrefab, coords,Quaternion.identity,transform);
        }
    }
}
