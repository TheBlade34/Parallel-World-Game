using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public GameObject player1; 
    public GameObject player2; 
    public Transform[] player1RespawnPoints; 
    public Transform[] player2RespawnPoints; 
    public float respawnYThreshold = -10f; 

    void Update()
    {

        if (player1.transform.position.y < respawnYThreshold)
        {
            RespawnPlayer(player1, player1RespawnPoints);
        }
        if (player2.transform.position.y < respawnYThreshold)
        {
            RespawnPlayer(player2, player2RespawnPoints);
        }
    }

    void RespawnPlayer(GameObject player, Transform[] respawnPoints)
    {
        if (respawnPoints.Length > 0)
        {
            Transform respawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;

            Debug.Log("Player " + player.name + " respawned at: " + respawnPoint.position);
        }
        else
        {
            Debug.LogError("No respawn points assigned for player: " + player.name);
        }
    }
}
