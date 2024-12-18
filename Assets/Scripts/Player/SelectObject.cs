using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f;      //최대 거리
    public LayerMask layerMask;          //탐색할 레이어

    private GameObject currentObject;    //현재 감지된 오브젝트

    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if(GameManager.Instance.canPlayerInteract)
        {
            RayCasting();
            Click();
        }
    }

    private void RayCasting()
    {
        //바라보는 방향으로 레이캐스트 발사
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        //오브젝트 감지
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            //새로운 오브젝트에 Ray가 닿으면 테두리 적용
            if (hitObject != currentObject)
            {
                RemoveHighlight();  //이전 오브젝트의 테두리 제거
                SetHighlight(hitObject);    //현재 오브젝트에 테두리 적용
            }
        }
        //레이캐스트팅 실패 시 테두리 제거
        else
        {
            RemoveHighlight();
        }
    }

    //테두리 생성
    private void SetHighlight(GameObject obj)
    {
        Outline outline = obj.GetComponent<Outline>();

        if (outline == null)
        {
            obj.AddComponent<Outline>();
            outline = obj.GetComponent<Outline>();
        }

        outline.enabled = true;
        currentObject = obj;
    }

    //테두리 제거
    private void RemoveHighlight()
    {
        //이전 오브젝트가 있으면 테두리 제거
        if (currentObject != null)
        {
            Outline outline = currentObject.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
            currentObject = null;
        }
    }

    private void Click()
    {
        if(Input.GetMouseButtonDown(0) && currentObject != null)
        {
            string tag = currentObject.tag.ToLower();

            if(tag == "altar")
            {
                Altar altar = currentObject.GetComponent<Altar>();
                StartCoroutine(altar.Submit());
            }
            else if (tag == "daychange")    //테스트용(낮에 제물 공양 성공->밤, 밤에 침대 상호작용->낮 변경 예정)
            {
                SkyController skyController = GameManager.Instance.GetComponent<SkyController>();
                if (GameManager.Instance.isNoon)
                {
                    StartCoroutine(skyController.SetNight());
                }
                else
                {
                    StartCoroutine(skyController.SetNoon());
                    GameManager.Instance.NextDay();
                }
            }
            else if (tag == "bed")   //밤 -> 낮(다음날)
            {
                Bed bed = currentObject.GetComponent<Bed>();
                bed.Sleep();
                player.SetHoldingWeapon(HoldingWeapon.NONE);

                //웨이포인트
            }
            else if(tag == "corn" || tag == "pumpkin" || tag == "tomato")
            {
                GameManager.Instance.questManager.GetRequiredItem(tag);
            }
            else if(tag == "deer" || tag == "human")
            {
                GameManager.Instance.FadeIn();
                currentObject.SetActive(false);

                GameManager.Instance.questManager.GetRequiredItem(tag);
            }
            else if(tag == "arrow")
            {
                Arrow arrow = currentObject.GetComponent<Arrow>();
                arrow.PickUpArrow();
            }
            else if (tag == "moonaltar")
            {
                MoonAltar moonAltar = currentObject.GetComponent<MoonAltar>();
                StartCoroutine(moonAltar.SubmitArrow());
            }
            else if(tag == "bow")
            {
                player.SetHoldingWeapon(HoldingWeapon.BOW);
                currentObject.SetActive(false);
            }
            else if(tag == "dagger")
            {
                player.SetHoldingWeapon(HoldingWeapon.DAGGER);
                currentObject.SetActive(false);
            }
        }
    }
}
