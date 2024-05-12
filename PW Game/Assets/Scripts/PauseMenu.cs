using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public string pauseMenuSceneName = "PauseMenu";
    public GameObject pauseMenuUI;
    bool isPaused = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        SceneManager.LoadScene(pauseMenuSceneName, LoadSceneMode.Additive);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
        isPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetSceneByName(pauseMenuSceneName).isLoaded && !isPaused)
        {
            SceneManager.UnloadSceneAsync(pauseMenuSceneName);
            Cursor.visible = false;
        }
    }
}
