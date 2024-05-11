using UnityEngine;

public class PlayerActivation : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public CameraSwitcher cameraSwitcher;
    private void Update()
    {
        CameraSwitcher.CameraMode currentMode = cameraSwitcher.GetCurrentCameraMode();
        switch (currentMode)
        {
            case CameraSwitcher.CameraMode.Ortho:
                SetPlayerScriptsEnabled(player1, true);
                SetPlayerScriptsEnabled(player2, false);
                break;
            case CameraSwitcher.CameraMode.Perspective:
                SetPlayerScriptsEnabled(player1, false);
                SetPlayerScriptsEnabled(player2, true);
                break;
        }
    }

    private void SetPlayerScriptsEnabled(GameObject player, bool isEnabled)
    {
        MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = isEnabled;
        }
    }
}
