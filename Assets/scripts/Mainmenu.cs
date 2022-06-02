using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource MenuSound;
    public void playGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlsMenu()
    {
        SceneManager.LoadScene("Controls");
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("start");
    }

    public void bigFacts()
    {
        SceneManager.LoadScene("bigFacts");
    }
}