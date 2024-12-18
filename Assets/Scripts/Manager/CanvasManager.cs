using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private CanvasScaler canvasScaler;

    private void Awake()
    {

    }

    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();   
    }

    private void Update()
    {
        SetCanvasScale();
    }

    private void SetCanvasScale()
    {
        float fixedAspectRatio = 1080f / 1920f;

        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        if (currentAspectRatio > fixedAspectRatio) canvasScaler.matchWidthOrHeight = 1;
        else if (currentAspectRatio < fixedAspectRatio) canvasScaler.matchWidthOrHeight = 0;
    }
}
