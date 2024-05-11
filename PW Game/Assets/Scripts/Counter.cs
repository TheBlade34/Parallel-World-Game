using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public static Counter Instance; // Singleton instance

    public TMP_Text counterText; // Reference to the counter text UI (TextMeshProUGUI)

    private int totalCoins = 0; // Total number of collected coins

    void Awake()
    {
        // Set up the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to collect coins and update the counter
    public void Collect(int coinValue)
    {
        totalCoins += coinValue;
        UpdateCounter();
    }

    // Method to update the counter text UI
    void UpdateCounter()
    {
        if (counterText != null)
        {
            counterText.text = "Coins: " + totalCoins;
        }
    }
}
