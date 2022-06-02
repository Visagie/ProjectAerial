using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whale : MonoBehaviour
{
    float whalecount;
    bool ismoving;
    public bool turn;
    float speed;
    public float rotatex;
    public float rotatez;
    public float rotatey;

    // Start is called before the first frame update
    void Start()
    {
        turn = true;
        ismoving = true;
        speed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().AddForce(Vector3.left * 3 * Time.fixedDeltaTime, ForceMode2D.Impulse);
        if (ismoving)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.left *speed* Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "wall" && turn)
        {
            speed = speed * 1;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (other.gameObject.tag == "wall2" && turn)
        {
            speed = speed * -1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "wall" && turn)
        {
            speed = -0.1f;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
        if (other.gameObject.tag == "wall2" && turn)
        {
            speed = 0.1f;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }
}
