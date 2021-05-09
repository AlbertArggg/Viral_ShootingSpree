using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCamera : MonoBehaviour
{
    public GameObject thisCamera;
    public GameObject MainCamera;
    public Animator anim;

    private void Awake()
    {
        MainCamera.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void activateMain()
    {
        thisCamera.SetActive(false);
        MainCamera.SetActive(true);
    }

}
