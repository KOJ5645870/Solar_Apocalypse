using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SkyController : MonoBehaviour
{
    public Light directionalLight;
    public Material noonSkyboxMaterial; //낮 스카이박스
    public Material nightSkyboxMaterial;     //밤 스카이박스

    void Start()
    {
        /*
        if (RenderSettings.skybox != null)
        {
            noonSkyboxMaterial = RenderSettings.skybox;
        }
        */
    }

    void Update()
    {
        int day = GameManager.Instance.day;

        if(GameManager.Instance.isNoon)
        {
            //태양 크기 조정
            if (noonSkyboxMaterial.HasProperty("_SunSize"))
            {
                noonSkyboxMaterial.SetFloat("_SunSize", GetSunScale(day));
            }
            //하늘 색상 조정
            if (noonSkyboxMaterial.HasProperty("_SkyTint"))
            {
                noonSkyboxMaterial.SetColor("_SkyTint", GetSunTint(day));
            }
        }
        else
        {
            
        }
    }

    private float GetSunScale(int day)
    {
        return 0.04f + 0.01f * (Mathf.Pow(day, 2) * 0.65f);
    }

    private Color GetSunTint(int day)
    {
        Color tint = new Color(0, 0.745f, 0.745f);

        if (day >= 7)
        {
            float t = ((float) day - 6f) / 4f; // 7~10을 0~1로 매핑
            tint.r = Mathf.Lerp(0, 1, t);
        }

        return tint;
    }

    public IEnumerator SetNoon()
    {
        GameManager.Instance.isNoon = true;

        GameManager.Instance.canPlayerInteract = false;

        float fadeDuration = GameManager.Instance.fadeDuration;

        StartCoroutine(GameManager.Instance.FadeOut());
        yield return new WaitForSeconds(fadeDuration);

        RenderSettings.skybox = noonSkyboxMaterial;    //스카이박스 변경
        directionalLight.color = new Color(1, 0.95f, 0.85f, 1);
        DynamicGI.UpdateEnvironment();  //빛 정보 업데이트
        yield return new WaitForSeconds(fadeDuration);

        StartCoroutine(GameManager.Instance.FadeIn());
        yield return new WaitForSeconds(fadeDuration);

        GameManager.Instance.canPlayerInteract = true;
        GameManager.Instance.UpdateInteractionState();  //상호작용 상태 업데이트
    }

    public IEnumerator SetNight()
    {
        GameManager.Instance.isNoon = false;

        GameManager.Instance.canPlayerInteract = false;

        float fadeDuration = GameManager.Instance.fadeDuration;

        StartCoroutine(GameManager.Instance.FadeOut());
        yield return new WaitForSeconds(fadeDuration);

        RenderSettings.skybox = nightSkyboxMaterial;    //스카이박스 변경
        directionalLight.color = new Color(0.5f, 0.45f, 0.4f, 1);
        DynamicGI.UpdateEnvironment();  //빛 정보 업데이트
        yield return new WaitForSeconds(fadeDuration);

        StartCoroutine(GameManager.Instance.FadeIn());
        yield return new WaitForSeconds(fadeDuration);

        GameManager.Instance.canPlayerInteract = true;
        GameManager.Instance.UpdateInteractionState();  //상호작용 상태 업데이트
    }
}
