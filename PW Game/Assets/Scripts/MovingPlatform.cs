using UnityEngine;

public class ReusableMovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;       
    public float speed = 2f;            
    public float waitTime = 1f;        
    private int currentWaypointIndex = 0;   
    private bool isMoving = true;           

    void Start()
    {
        transform.position = waypoints[currentWaypointIndex].position;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsWaypoint();
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                isMoving = false;
                Invoke("StartMoving", waitTime);
            }
        }
    }

    void MoveTowardsWaypoint()
    {
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void StartMoving()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        isMoving = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.transform.parent = null;
        }
    }
}
