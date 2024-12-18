using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public enum HoldingWeapon
{
    NONE, DAGGER, BOW
}

public class Player : MonoBehaviour
{
    public Camera cameraObject;       //�÷��̾� �Ӹ� ��ġ

    public float moveSpeed;
    public float walkSpeed = 5f;            //�̵� �ӵ�
    public float runSpeed = 10f;            //�޸��� �ӵ�
    private float gravity = 2f;             //�߷�

    [SerializeField] private HoldingWeapon holdingWeapon = HoldingWeapon.NONE;

    CameraRotation cameraRotation;
    CharacterController characterController;

    [SerializeField] private Image holdingImage;
    [SerializeField] private Sprite daggerHoldingImage;
    [SerializeField] private Sprite bowHoldingImage;

    void Start()
    {
        cameraRotation = cameraObject.GetComponent<CameraRotation>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked; //ȭ�� �߾ӿ� Ŀ�� ����
    }

    private void Update()
    {
        if (GameManager.Instance.canPlayerInteract && !GameManager.Instance.isPause)
        {
            Move();
            UpdateHoldingWeaponState();
        }
    }

    //�÷��̾� �̵�
    private void Move()
    {
        //�̵� �Է�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //shift�Է� �� �޸���
        if (Input.GetKey(KeyCode.LeftShift)) moveSpeed = runSpeed;
        else moveSpeed = walkSpeed;

        //�¿� ȸ��
        transform.localEulerAngles = new Vector3(0, cameraRotation.mouseX, 0);

        //�̵�
        Vector3 move = new Vector3();
        if (characterController.isGrounded)
        {
            move = new Vector3(h, 0, v);
            move = characterController.transform.TransformDirection(move);
            //move = new Vector3(h, 0, v);
        }
        //�߷�
        else
        {
            move.y -= gravity;
        }

        //ĳ���� �̵� ó��
        //transform.Translate(move);
        characterController.Move(move * moveSpeed * Time.deltaTime);

    }

    public void SetHoldingWeapon(HoldingWeapon holdingWeapon)
    {
        this.holdingWeapon = holdingWeapon;
    }

    private void UpdateHoldingWeaponState()
    {
        if (holdingWeapon == HoldingWeapon.NONE)
        {
            holdingImage.color = new Color(1, 1, 1, 0);
            holdingImage.sprite = null;
        }
        else if (holdingWeapon == HoldingWeapon.DAGGER)
        {
            holdingImage.sprite = daggerHoldingImage;
            holdingImage.color = new Color(1, 1, 1, 1);
        }
        else if (holdingWeapon == HoldingWeapon.BOW)
        {
            holdingImage.sprite = bowHoldingImage;
            holdingImage.color = new Color(1, 1, 1, 1);
        }
    }
}
