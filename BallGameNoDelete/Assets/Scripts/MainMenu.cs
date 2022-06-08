using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;
    public CinemachineVirtualCamera vCam;
    bool rotating;

    void Start()
    {
        rotating = true;
        vCam.m_Lens.FieldOfView = 1f;
    }

    void Update()
    {
        if (rotating)
        {
            transform.Rotate(0f, .2f, 0f, Space.World);
        }
        if (!rotating)
        {
            if (vCam.m_Lens.FieldOfView <= 50f)
                vCam.m_Lens.FieldOfView += .5f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void StartGame()
    {
        rotating = false;
        Destroy(startButton);
    }
}