using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string player1Tag = "Player1";
    public string player2Tag = "Player2";
    public TMP_Text messageText;
    private bool player1Inside;
    private bool player2Inside;
    private void Start()
    {
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player1Tag))
        {
            player1Inside = true;
        }
        else if (other.CompareTag(player2Tag))
        {
            player2Inside = true;
        }

        CheckPlayers();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(player1Tag))
        {
            player1Inside = false;
        }
        else if (other.CompareTag(player2Tag))
        {
            player2Inside = false;
        }

        CheckPlayers();
    }

    private void CheckPlayers()
    {
        if (player1Inside && player2Inside)
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            messageText.gameObject.SetActive(player1Inside || player2Inside);
            messageText.text = "Both Players are required to proceed";
        }
    }
}
