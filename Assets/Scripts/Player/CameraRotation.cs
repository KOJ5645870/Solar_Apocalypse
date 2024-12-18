using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitive = 10f; //마우스 민감도
    [HideInInspector] public float mouseX = 0f;  //좌우 회전
    [HideInInspector] public float mouseY = 0f;  //상하 회전

    void Start()
    {
        
    }


    void Update()
    {
        if(GameManager.Instance.canPlayerInteract && !GameManager.Instance.isPause)
        {
            Rotate();
        }
    }

    void Rotate()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitive;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitive;

        mouseY = Mathf.Clamp(mouseY, -90, 90);

        transform.localEulerAngles = new Vector3(mouseY, 0, 0);
    }
}

