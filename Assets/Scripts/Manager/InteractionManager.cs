using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private bool noonAppearance;     //낮에 등장 여부
    [SerializeField] private bool nightAppearance;    //밤에 등장 여부

    [SerializeField, Range(1, 10)] private int appearanceStartDay;  //등장 시작일
    [SerializeField, Range(1, 10)] private int appearanceEndDay;    //등장 종료일

    [SerializeField, Range(1, 10)] private int minInteractableDay;  //최소 상호작용 가능 일
    [SerializeField, Range(1, 10)] private int maxInteractableDay;  //최대 상호작용 가능 일

    [SerializeField] private bool noonInteractable;     //낮 상호작용 가능
    [SerializeField] private bool nightInteractable;    //밤 상호작용 가능

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
        //일 별 상호작용 레이어 지급
        //낮일 때 낮 상호작용이 true라면, 밤일때 밤 상호작용이 true라면 지급
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
