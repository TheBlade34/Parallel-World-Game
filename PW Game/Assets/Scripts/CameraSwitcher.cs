using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera orthoCamera; // Reference to the orthographic camera
    public Camera perspectiveCamera; // Reference to the perspective camera
    public GameObject playerBall; // Reference to the player ball
    public float switchDuration = 1.0f; // Duration of the camera switch animation

    private Camera currentCamera;
    private Camera targetCamera;
    private float switchTimer;
    private bool isSwitching;

    void Start()
    {
        // Initialize current and target cameras
        currentCamera = orthoCamera;
        targetCamera = perspectiveCamera;

        // Disable the perspective camera initially
        perspectiveCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // Update camera position to match player ball
        transform.position = playerBall.transform.position;

        // Check for input to switch cameras
        if (Input.GetKeyDown(KeyCode.R) && !isSwitching)
        {
            SwitchCameras();
        }

        // If a switch is in progress, update the transition
        if (isSwitching)
        {
            switchTimer += Time.deltaTime;

            // Calculate interpolation factor based on time
            float t = Mathf.Clamp01(switchTimer / switchDuration);

            // Interpolate camera position
            transform.position = Vector3.Lerp(currentCamera.transform.position, targetCamera.transform.position, t);

            // If the switch is complete, finish the transition
            if (switchTimer >= switchDuration)
            {
                isSwitching = false;
                currentCamera.gameObject.SetActive(false);
                switchTimer = 0f;
            }
        }
    }

    void SwitchCameras()
    {
        // Toggle between current and target cameras
        Camera temp = currentCamera;
        currentCamera = targetCamera;
        targetCamera = temp;

        // Activate the target camera and start the switch
        targetCamera.gameObject.SetActive(true);
        isSwitching = true;
    }
}