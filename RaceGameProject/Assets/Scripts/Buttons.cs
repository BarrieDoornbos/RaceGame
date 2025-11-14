using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject EscapeMenu;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_menu");
    }

    public void PlayMap1()
    {
        SceneManager.LoadScene("Racetrack_1");
    }

    public void PlayMap2()
    {
        SceneManager.LoadScene("Racetrack_2");
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        EscapeMenu.SetActive(false);
    }
}
