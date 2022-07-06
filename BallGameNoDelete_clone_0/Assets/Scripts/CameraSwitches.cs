using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitches : MonoBehaviour
{
    public Transform camera;
    Quaternion targetAngle, previousAngle, nextAngle;

    // Start is called before the first frame update
    void Start()
    {
        targetAngle = camera.transform.rotation;
        previousAngle = targetAngle;
    }

    // Update is called once per frame
    void Update()
    {
        // dampen towards the target angle
        if (camera.transform.rotation != targetAngle)
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, targetAngle, Time.deltaTime * 5f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "TopDown")
        {
            // set new angle
            nextAngle = Quaternion.Euler(90f, 0, 0);

            // if the next angle is same as the current angle, then set the angle to the previous angle
            if (nextAngle == targetAngle)
                targetAngle = previousAngle;
            else // if the next angle is different from the previous angle, set the angle to the next angle and save the previous
            {
                previousAngle = targetAngle; // save previous angle
                targetAngle = nextAngle;
            }
        }
        else if (collision.gameObject.tag == "Isometric")
        {
            // set new angle
            nextAngle = Quaternion.Euler(35f, 0, 0);

            if (nextAngle == targetAngle)
                targetAngle = previousAngle;
            else
            {
                previousAngle = targetAngle; // save previous angle
                targetAngle = nextAngle;
            }
        }
        else if (collision.gameObject.tag == "Sidescroller")
        {
            // set new angle
            nextAngle = Quaternion.Euler(0, 0, 0);

            if (nextAngle == targetAngle)
                targetAngle = previousAngle;
            else
            {
                previousAngle = targetAngle; // save previous angle
                targetAngle = nextAngle;
            }
        }
        else if (collision.gameObject.tag == "SidescrollerFlip")
        {
            // set new angle
            nextAngle = Quaternion.Euler(0, -180f, 0);

            if (nextAngle == targetAngle)
                targetAngle = previousAngle;
            else
            {
                previousAngle = targetAngle; // save previous angle
                targetAngle = nextAngle;
            }
        }
    }
}