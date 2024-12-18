using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    SkyController skyController;

    void Start()
    {
        skyController = GameManager.Instance.GetComponent<SkyController>();
    }

    void Update()
    {
        
    }

    public void Sleep()
    {
        if (!GameManager.Instance.isNoon)
        {
            StartCoroutine(skyController.SetNoon());
            GameManager.Instance.NextDay();
            GameManager.Instance.questManager.StartAlterQuest();
        }
    }
}
