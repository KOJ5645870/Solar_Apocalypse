using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUIController : MonoBehaviour
{
    public List<Image> arrowImageList = new List<Image>();

    void Start()
    {
    }

    void Update()
    {
        int amount = GameManager.Instance.arrowAmount;

        for(int i = 0; i < arrowImageList.Count; i++) 
        {
            if(i < amount) arrowImageList[i].enabled = true;
            else arrowImageList[i].enabled = false;
        }
    }
}
