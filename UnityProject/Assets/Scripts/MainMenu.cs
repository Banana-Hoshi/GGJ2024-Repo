using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string GameSceneName = "Game";

    void Start()
    {
        // Set the cursor to be visible
        Cursor.visible = true;
    }

    public void GameStart()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
