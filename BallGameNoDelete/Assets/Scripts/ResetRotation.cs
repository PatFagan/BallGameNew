using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    bool playerIsOn;
    bool reset;

    // Start is called before the first frame update
    void Start()
    {
        playerIsOn = false;
        reset = false;
    }

    void Update()
    {
        if (reset)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 5f);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsOn = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsOn = false;
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        print("off");
        if (playerIsOn == false)
        {
            reset = true;
        }
        yield return new WaitForSeconds(3f);
        reset = false;
    }
}