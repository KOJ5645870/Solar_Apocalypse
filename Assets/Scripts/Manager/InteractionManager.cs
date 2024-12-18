using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private bool noonAppearance;     //���� ���� ����
    [SerializeField] private bool nightAppearance;    //�㿡 ���� ����

    [SerializeField, Range(1, 10)] private int appearanceStartDay;  //���� ������
    [SerializeField, Range(1, 10)] private int appearanceEndDay;    //���� ������

    [SerializeField, Range(1, 10)] private int minInteractableDay;  //�ּ� ��ȣ�ۿ� ���� ��
    [SerializeField, Range(1, 10)] private int maxInteractableDay;  //�ִ� ��ȣ�ۿ� ���� ��

    [SerializeField] private bool noonInteractable;     //�� ��ȣ�ۿ� ����
    [SerializeField] private bool nightInteractable;    //�� ��ȣ�ۿ� ����

    private bool IsAppearanceTime(int day, bool isNoon)
    {
        return ((isNoon && noonAppearance) || (!isNoon && nightAppearance)) 
            && (appearanceStartDay <= day && day <= appearanceEndDay);
    }

    private bool IsInteractableTime(int day, bool isNoon)
    {
        return ((isNoon && noonInteractable) || (!isNoon && nightInteractable))
            && (minInteractableDay <= day && day <= maxInteractableDay);
    }

    public void UpdateInteractState(int day, bool isNoon)
    {
        //�� �� ��ȣ�ۿ� ���̾� ����
        //���� �� �� ��ȣ�ۿ��� true���, ���϶� �� ��ȣ�ۿ��� true��� ����
        if (IsAppearanceTime(day, isNoon))
        {
            gameObject.SetActive(true);

            if (IsInteractableTime(day, isNoon))
            {
                gameObject.layer = LayerMask.NameToLayer("Interaction");
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
        else gameObject.SetActive(false);

        

    }
}
