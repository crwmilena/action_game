using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour
{
    public void ToGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void ToExit()
    {
        Application.Quit();
    }
}

