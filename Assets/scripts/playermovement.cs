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
    public float speed = 50.0f;
    Rigidbody2D player;
    int lives = 3;
    public Image life1, life2, life3, o1, o2, o3, o4 , o5, o6;
    float cntdnw = 120;
    public GameObject winscreen,pause,lost;
    bool ispaused;
    public GameObject body,injury;
    public Image dirt;
    public Animator playerbody;
    float dirtint;
    public Slider oxybar;
    private SpriteRenderer playerSprite;

    public AudioSource breathingSound;
    public AudioSource jellyfishStingSound;
    public AudioSource kickingSound;
    public AudioSource pickUpTrashSound;


    bool ismoving;
    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        dirtint = 0.7f;
        ismoving = false;
        playerbody = GetComponent<Animator>();
        injury.SetActive(false);
        winscreen.SetActive(false);
        pause.SetActive(false);
        lost.SetActive(false);
        ispaused = true;
        trashmeter.value = 0;
        player = GetComponent<Rigidbody2D>();
        var tea = dirt.GetComponent<Image>();
        Color eta = tea.color;
        eta.a = dirtint;
        dirt.color = eta;
        //dirtint = dirt.color.a;
        

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
        if (ismoving)
        {
            playerbody.SetBool("ismoving", true);
        }
        else { playerbody.SetBool("ismoving", false); }
       
        movement();
        lifechecker();
        counter();
        O2check();
        lookingat();
        

    }


    void lookingat()
    {
        Vector3 mouseposition = getmouseworldposition();
        Vector3 aimdirection = (mouseposition - transform.position).normalized;
        float angle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public static Vector3 getmouseworldposition()// by code monkey: https://www.youtube.com/watch?v=fuGQFdhSPg4
    {
        Vector3 vec = getmouseworldpositionwithz(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 getmouseworldpositionwithz()
    {
        return getmouseworldpositionwithz(Input.mousePosition, Camera.main);
    }
    public static Vector3 getmouseworldpositionwithz(Camera worldcamera)
    {
        return getmouseworldpositionwithz(Input.mousePosition, worldcamera);
    }
    public static Vector3 getmouseworldpositionwithz(Vector3 screenposition, Camera worldcamera)
    {
        Vector3 worldposition = worldcamera.ScreenToWorldPoint(screenposition);

        return worldposition;
    }

    private void FixedUpdate()
    {

        //faceMouse();
        #region
        if (Input.GetKey(KeyCode.W))
        {
            kickingSound.Play();
            ismoving = true;
            wpressed = true;
           // body.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            ismoving = false;
            wpressed = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            kickingSound.Play();
            ismoving = true;
            spressed = true;
            //body.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            ismoving = false;
            spressed = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            kickingSound.Play();
            ismoving = true;
            apressed = true;
            //playerSprite.flipX = true;
            //body.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            ismoving = false;
            apressed = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
           kickingSound.Play();
            ismoving = true;
            dpressed = true;
            //playerSprite.flipX = false;
            //body.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            ismoving = false;
            dpressed = false;
        }
        #endregion

        if(trashmeter.value == 1)
        {
            winning_condition();
        }
    }

  /*  void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }*/

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
        if (Input.GetKey(KeyCode.Space))
        {
            player.AddForce(Vector3.up * 3 * Time.fixedDeltaTime, ForceMode2D.Impulse);
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

        oxybar.value = cntdnw / 120f;
        if(oxybar.value <= 0)
        {
            gameend();
        }
    }

    void replenish()
    {
        cntdnw += 10;
        
    }

    void trashcollection()
    {
        dirtint-=0.07f;
        var tea = dirt.GetComponent<Image>();
        Color eta = tea.color;
        eta.a = dirtint;
        dirt.color = eta;
        trashmeter.value+= 0.1f;
    }


    private void Pause()
    {
        
        
        Time.timeScale = 0f;
        pause.SetActive(true);
        ispaused = true;
        dirt.enabled = true;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            jellyfishStingSound.Play();
            lives--;
            cntdnw -= 10;
            StartCoroutine(getinjured());

        }

        if (other.gameObject.tag == "oxygen")
        {
            breathingSound.Play();
            replenish();
        }

        if (other.gameObject.tag == "collect")
        {
            pickUpTrashSound.Play();
            trashcollection();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            jellyfishStingSound.Play();
            lives--;
            cntdnw -= 10;
            StartCoroutine(getinjured());
            
        } 

        if(other.gameObject.tag == "oxygen")
        {
            breathingSound.Play();
            replenish();
        }

        if (other.gameObject.tag == "collect") 
        {
            pickUpTrashSound.Play();
            trashcollection();
        }
    }

    


    IEnumerator getinjured()
    {
        injury.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        injury.SetActive(false);
    }
}
