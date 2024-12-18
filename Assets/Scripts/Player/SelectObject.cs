using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f;      //�ִ� �Ÿ�
    public LayerMask layerMask;          //Ž���� ���̾�

    private GameObject currentObject;    //���� ������ ������Ʈ

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
        //�ٶ󺸴� �������� ����ĳ��Ʈ �߻�
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        //������Ʈ ����
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            //���ο� ������Ʈ�� Ray�� ������ �׵θ� ����
            if (hitObject != currentObject)
            {
                RemoveHighlight();  //���� ������Ʈ�� �׵θ� ����
                SetHighlight(hitObject);    //���� ������Ʈ�� �׵θ� ����
            }
        }
        //����ĳ��Ʈ�� ���� �� �׵θ� ����
        else
        {
            RemoveHighlight();
        }
    }

    //�׵θ� ����
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

    //�׵θ� ����
    private void RemoveHighlight()
    {
        //���� ������Ʈ�� ������ �׵θ� ����
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
            else if (tag == "daychange")    //�׽�Ʈ��(���� ���� ���� ����->��, �㿡 ħ�� ��ȣ�ۿ�->�� ���� ����)
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
            else if (tag == "bed")   //�� -> ��(������)
            {
                Bed bed = currentObject.GetComponent<Bed>();
                bed.Sleep();
                player.SetHoldingWeapon(HoldingWeapon.NONE);

                //��������Ʈ
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
