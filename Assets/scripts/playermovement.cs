using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour
{
    public Slider trashmeter;
    public int trash_collected, trash_divider, totaltrash;
    bool apressed, dpressed, spressed, wpressed;
    float speed = 25.0f;
    Rigidbody2D player;
    int lives = 3;
    public Image life1, life2, life3, o1, o2, o3, o4 , o5, o6;
    float cntdnw = 120;
    public GameObject winscreen,pause,lost;
    bool ispaused;
    // Start is called before the first frame update
    void Start()
    {
        winscreen.SetActive(false);
        pause.SetActive(false);
        lost.SetActive(false);
        ispaused = true;
        trashmeter.value = 0;
        player = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ispaused)
            {
                resume();
            }
            else
            {
                Pause();
            }
        }

       
        movement();
        lifechecker();
        counter();
        O2check();
        

    }
    private void FixedUpdate()
    {
        #region
        if (Input.GetKey(KeyCode.W))
        {
            wpressed = true;
        }
        else
        {
            wpressed = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            spressed = true;
        }
        else
        {
            spressed = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            apressed = true;
        }
        else
        {
            apressed = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dpressed = true;
        }
        else
        {
            dpressed = false;
        }
        #endregion

        if(trashmeter.value == 1)
        {
            winning_condition();
        }
    }

    void movement()
    {
        #region
        if (apressed)
        {
            player.AddForce(Vector3.left * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        
        
         
        
        if (dpressed)
        {
            player.AddForce(Vector3.right * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        if (wpressed)
        {
            player.AddForce(Vector3.up * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        if (spressed)
        {
            player.AddForce(Vector3.down * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        #endregion
    }

    void lifechecker()


    {
        if (lives == 2)
        {
            Destroy(life3);
        }
        else if (lives == 1)
        {
            Destroy(life2);
        }
        else if (lives == 0)
        {
            Destroy(life1);
            gameend();
        }


    }

    void counter()
    {
        if (cntdnw > 0)
        {
            cntdnw -= Time.deltaTime;
            Debug.Log(cntdnw);
            
        }
        
    }

    void O2check()
    {
        if(cntdnw <= 100)
        {
            var tea = o6.GetComponent<Image>();
            Color eta = tea.color;
            eta.a = 0;
            o6.color = eta;
        }
        if (cntdnw <= 80)
        {
            var tea = o5.GetComponent<Image>();
            Color eta = tea.color;
            eta.a = 0;
            o5.color = eta;
        }
        if (cntdnw <= 60)
        {
            var tea = o4.GetComponent<Image>();
            Color eta = tea.color;
            eta.a = 0;
            o4.color = eta;
        }
        if (cntdnw <= 40)
        {
            var tea = o3.GetComponent<Image>();
            Color eta = tea.color;
            eta.a = 0;
            o3.color = eta;
        }
        if (cntdnw <= 20)
        {
            var tea = o2.GetComponent<Image>();
            Color eta = tea.color;
            eta.a = 0;
            o2.color = eta;
        }
        if (cntdnw <= 0)
        {
            var tea = o1.GetComponent<Image>();
            Color eta = tea.color;
            eta.a = 0;
            o1.color = eta;
            gameend();
        }
    }

    void replenish()
    {
        cntdnw = 120;
        var tea = o6.GetComponent<Image>();
        Color eta = tea.color;
        eta.a = 1;
        o6.color = eta;

         tea = o5.GetComponent<Image>();
         eta = tea.color;
        eta.a = 1;
        o5.color = eta;

        tea = o4.GetComponent<Image>();
         eta = tea.color;
        eta.a = 1;
        o4.color = eta;

         tea = o3.GetComponent<Image>();
         eta = tea.color;
        eta.a = 1;
        o3.color = eta;

        tea = o2.GetComponent<Image>();
         eta = tea.color;
        eta.a = 1;
        o2.color = eta;

         tea = o1.GetComponent<Image>();
         eta = tea.color;
        eta.a = 1;
        o1.color = eta;

    }

    void trashcollection()
    {
        trashmeter.value+= 0.1f;
    }


    private void Pause()
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
        ispaused = true;
    }

    public void resume()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
        ispaused = false;
        
    }

    void winning_condition()
    {
        Time.timeScale = 0f;
        winscreen.SetActive(true);
    }

    void gameend()
    {
        Time.timeScale = 0f;
        lost.SetActive(true);
    }

    public void tryagain()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void exitapp()
    {
        Application.Quit();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            lives--;
            cntdnw -= 10;
            
        } 

        if(other.gameObject.tag == "oxygen")
        {
            replenish();
        }

        if (other.gameObject.tag == "collect") 
        {
            trashcollection();
        }
    }
}
