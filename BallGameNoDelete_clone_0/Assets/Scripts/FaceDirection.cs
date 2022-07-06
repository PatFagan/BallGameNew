using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour
{
    public string stopRotationTag;
    public bool onUpdate;
    bool stopRotation;
    GameObject currentObject;
    public float rotationDelay;

    public float oppositeScalar = 1f;

    void Start()
    {
        currentObject = this.gameObject;
        StartCoroutine(CalculateRotation());
    }

    Vector3 moveDirection, oldPosition;
    IEnumerator CalculateRotation()
    {
        oldPosition = transform.position;
        yield return new WaitForSeconds(rotationDelay);
        moveDirection = transform.position - oldPosition;
        //print(moveDirection);
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(currentObject.transform.rotation, Quaternion.LookRotation(oppositeScalar * new Vector3(moveDirection.x, moveDirection.y, moveDirection.z)), Time.deltaTime * 40f);
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (onUpdate && stopRotation == false)
            StartCoroutine(CalculateRotation());
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == stopRotationTag)
        {
            stopRotation = true;
            gameObject.transform.SetParent(collider.gameObject.transform);
        }
    }
}