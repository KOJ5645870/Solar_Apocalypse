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
    public Camera cameraObject;       //플레이어 머리 위치

    public float moveSpeed;
    public float walkSpeed = 5f;            //이동 속도
    public float runSpeed = 10f;            //달리기 속도
    private float gravity = 2f;             //중력

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

        Cursor.lockState = CursorLockMode.Locked; //화면 중앙에 커서 고정
    }

    private void Update()
    {
        if (GameManager.Instance.canPlayerInteract && !GameManager.Instance.isPause)
        {
            Move();
            UpdateHoldingWeaponState();
        }
    }

    //플레이어 이동
    private void Move()
    {
        //이동 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //shift입력 시 달리기
        if (Input.GetKey(KeyCode.LeftShift)) moveSpeed = runSpeed;
        else moveSpeed = walkSpeed;

        //좌우 회전
        transform.localEulerAngles = new Vector3(0, cameraRotation.mouseX, 0);

        //이동
        Vector3 move = new Vector3();
        if (characterController.isGrounded)
        {
            move = new Vector3(h, 0, v);
            move = characterController.transform.TransformDirection(move);
            //move = new Vector3(h, 0, v);
        }
        //중력
        else
        {
            move.y -= gravity;
        }

        //캐릭터 이동 처리
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
