using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anothermenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Startgame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void exitapplication()
    {
        Application.Quit();
    }

    public void controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Resume()
    {
        
    }
}
