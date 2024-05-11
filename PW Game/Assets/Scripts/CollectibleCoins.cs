using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    public int coinValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Counter.Instance.Collect(coinValue);
            Destroy(gameObject);
        }
    }
}
