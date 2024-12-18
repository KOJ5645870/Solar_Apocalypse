using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitive = 10f; //���콺 �ΰ���
    [HideInInspector] public float mouseX = 0f;  //�¿� ȸ��
    [HideInInspector] public float mouseY = 0f;  //���� ȸ��

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

