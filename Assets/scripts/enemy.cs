using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    CentralController cont;

    public GameObject cont1;
    public Image life1, life2, life3;
    
    // Start is called before the first frame update
    void Start()
    {
        cont = cont1.GetComponent<CentralController>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "player")
        {
            Destroy(gameObject);
            
        }
    }
}
