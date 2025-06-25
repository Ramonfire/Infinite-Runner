using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float minFOV =35f;
    [SerializeField] float maxFOV=85f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float ZoomSpeed = 5f;
    CinemachineCamera camera;

    private void Awake()
    {
        camera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFov(float SpeedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(SpeedAmount));
    }

    IEnumerator ChangeFOVRoutine(float SpeedAmmount) 
    {
        float StartFOV = camera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(StartFOV+SpeedAmmount*ZoomSpeed, minFOV, maxFOV);

        float elapsedTime = 0f;
         while (elapsedTime<zoomDuration)
        {
            camera.Lens.FieldOfView= Mathf.Lerp(StartFOV, targetFOV, elapsedTime/zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;  
        }

        camera.Lens.FieldOfView = targetFOV;
    }
}
