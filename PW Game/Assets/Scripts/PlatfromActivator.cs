using UnityEngine;

public class PlatformActivator : MonoBehaviour
{
    public GameObject movingPlatform;  
    public Material redMaterial;       
    public Material greenMaterial;     
    private MonoBehaviour platformScript;  
    private new Renderer renderer;             
    void Start()
    {
       
        platformScript = movingPlatform.GetComponent<MonoBehaviour>();        
        platformScript.enabled = false;
        renderer = GetComponent<Renderer>();      
        renderer.material = redMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2") || (other.CompareTag("Player1")))
        {
            platformScript.enabled = true;
            renderer.material = greenMaterial; 
        }
        if (other.CompareTag("Player2") && (other.CompareTag("Player1")))
        {
            platformScript.enabled = true;
            renderer.material = greenMaterial;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player2") || (other.CompareTag("Player1")))
        {
            platformScript.enabled = false;
            renderer.material = redMaterial;
        }
        if (other.CompareTag("Player2") && (other.CompareTag("Player1")))
        {
            platformScript.enabled = false;
            renderer.material = redMaterial;
        }
    }
}
