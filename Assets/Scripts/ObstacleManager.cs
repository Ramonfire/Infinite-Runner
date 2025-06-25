using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] Transform ObstacleTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObstacleTransform = GetComponentInChildren<Rigidbody>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if the object has fallen bellow to where we can see it or if it went past the camera then delete it
        if (ObstacleTransform.position.y < -10 || ObstacleTransform.position.z < Camera.main.transform.position.z-5)
            Destroy(gameObject);
    }
}
