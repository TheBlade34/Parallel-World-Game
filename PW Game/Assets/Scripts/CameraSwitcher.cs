using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera orthoCamera;
    public Camera perspectiveCamera;
    public GameObject player1;
    public GameObject player2;
    public float switchDuration = 1.0f;
    private Camera currentCamera;
    private Camera targetCamera;
    private float switchTimer;
    private bool isSwitching;
    private Vector3 perspectiveDefaultPosition; 

    public enum CameraMode
    {
        Ortho,
        Perspective
    }

    void Start()
    {
        InitializeCameras();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isSwitching)
        {
            SwitchCameras();
        }

        if (isSwitching)
        {
            UpdateCameraPosition();
        }
    }

    void InitializeCameras()
    {
        currentCamera = orthoCamera;
        targetCamera = perspectiveCamera;
        perspectiveCamera.gameObject.SetActive(false);
        perspectiveDefaultPosition = perspectiveCamera.transform.position;
        transform.position = player1.transform.position;
    }

    void SwitchCameras()
    {
        isSwitching = true;
        switchTimer = 0f;
        currentCamera.gameObject.SetActive(false);
        targetCamera.gameObject.SetActive(true);
        Camera temp = currentCamera;
        currentCamera = targetCamera;
        targetCamera = temp;
    }

    void UpdateCameraPosition()
    {
        switchTimer += Time.deltaTime;
        float t = Mathf.Clamp01(switchTimer / switchDuration);
        transform.position = Vector3.Lerp(currentCamera.transform.position, targetCamera.transform.position, t);
        if (switchTimer >= switchDuration)
        {
            isSwitching = false;
        }
    }

    public CameraMode GetCurrentCameraMode()
    {
        return (currentCamera == orthoCamera) ? CameraMode.Ortho : CameraMode.Perspective;
    }
}
