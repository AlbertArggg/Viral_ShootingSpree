using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Camera Cam;
    public Vector2 cursorPos;

    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Cursor.visible = false;
        cursorPos = Cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
