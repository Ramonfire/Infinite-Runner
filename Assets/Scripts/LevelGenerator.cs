using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject ChunkPrefab;
    [SerializeField] Transform ChunkParent;
    [SerializeField] int startingChunkAmount =12;
    [SerializeField] int ChunkLength = 10;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float minSpeed = 2f;
    [SerializeField] float maxSpeed = 20f;


    List<GameObject> Chunks = new List<GameObject>();
    CameraController camera;
    private void Awake()
    {
        camera = FindFirstObjectByType<CameraController>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateChunks();
    }

    // Update is called once per frame
    void Update()
    {
        MoveChunks();
    }
    private void GenerateChunks()
    {
        if (ChunkPrefab != null)
        {
            for (int i = 0; i < startingChunkAmount; i++)
            {
                Vector3 position= CalCulatePosition(); 

               GameObject CreatedObject =  Instantiate(ChunkPrefab, position, Quaternion.identity, ChunkParent);
               Chunks.Add(CreatedObject);
            }
        }
    }

    private void MoveChunks() 
    {
        for (int i = 0;i<Chunks.Count;i++)
        {
            GameObject Chunk = Chunks[i];
            Chunk.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            ResetChunkPosition(Chunk);
        }
      
    }

    private void ResetChunkPosition(GameObject Chunk)
    {
        if (Chunk.transform.position.z < Camera.main.transform.position.z-ChunkLength)
        {
            Chunks.Remove(Chunk);
            Destroy(Chunk);

            CreateNewChunk();

        }
    }

    private void CreateNewChunk()
    {
        Vector3 position = CalCulatePosition();
        GameObject CreatedObject = Instantiate(ChunkPrefab, position, Quaternion.identity, ChunkParent);

        Chunks.Add(CreatedObject);
    }
    // calculate the chunk position based on the last one before it
    private Vector3 CalCulatePosition()
    {
        Vector3 position;
        if (Chunks.Count==0)
        {
            position = transform.position;
        }
        else
        {
            position = Chunks[Chunks.Count - 1].transform.position;
            position.z = position.z + ChunkLength;
        }
      
        return position;
    }

    public void ChangeLevelSpeedBy(float SpeedAmmount) 
    {
        float newMoveSpeed = moveSpeed+ SpeedAmmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minSpeed, maxSpeed);
        if (moveSpeed != newMoveSpeed)
        {
            moveSpeed = newMoveSpeed;
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - SpeedAmmount);
            camera.ChangeCameraFov(SpeedAmmount);
        } 
    }
}
