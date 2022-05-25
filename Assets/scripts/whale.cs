using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whale : MonoBehaviour
{
    float whalecount;
    bool ismoving;

    // Start is called before the first frame update
    void Start()
    {
        ismoving = false;
       whalecount =  Random.Range(0f, 15f);
        StartCoroutine(whaleenter());
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().AddForce(Vector3.left * 3 * Time.fixedDeltaTime, ForceMode2D.Impulse);
        if (ismoving)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.left *0.1f* Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    IEnumerator whaleenter()
    {
        ismoving = true;
        yield return new WaitForSeconds(5f);

    }
}
