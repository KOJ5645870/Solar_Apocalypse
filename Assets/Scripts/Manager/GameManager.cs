using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [HideInInspector] public float fadeDuration = 1.0f;

    [SerializeField] private GameObject questUI;
    [SerializeField] private GameObject pauseUI;


    [Range(1, 10)]
    public int day = 1;
    public bool isNoon = true;

    [HideInInspector] public bool canPlayerInteract = true;

    [Range(0, 9)]
    public int arrowAmount = 0;

    private static GameManager gmInstance;

    [HideInInspector] public QuestManager questManager;

    public bool isPause = false;

    public static GameManager Instance
    {
        get
        {
            //인스턴스 할당
            if (!gmInstance)
            {
                gmInstance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return gmInstance;
        }
    }

    private void Awake()
    {
        if (gmInstance == null)
        {
            gmInstance = this;
        }
        //인스턴스 존재 시 기존 유지
        else if (gmInstance != this)
        {
            Destroy(gameObject);
        }
        //씬 전환 시 인스턴스가 파괴 X
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        questManager = GetComponent<QuestManager>();

        InitFadeImageSize();
        UpdateInteractionState();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //일시정지 해제
            if(isPause)
            {
                GameBack();
            }
            //일시정지
            else
            {
                GamePause();
            }
        }
    }
    
    //자동 전체 화면 맞춤
    private void InitFadeImageSize()
    {
        RectTransform rectTransform = fadeImage.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0, 0);    //좌측 하단 모서리
        rectTransform.anchorMax = new Vector2(1, 1);    //우측 상단 모서리

        rectTransform.offsetMin = Vector2.zero; //좌측 하단 오프셋
        rectTransform.offsetMax = Vector2.zero; //우측 상단 오프셋

        fadeImage.color = new Color(0, 0, 0, 0);
    }

    //페이드 아웃
    public IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }
    }

    //페이드 인
    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;

            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    public void NextDay()
    {
        day++;
    }

    public void UpdateInteractionState()
    {
        //비활성화 오브젝트 포함 순회
        foreach (GameObject obj in FindObjectsOfType<GameObject>(true))
        {
            InteractionManager interactionManager = obj.GetComponent<InteractionManager>();

            if (interactionManager != null)
            {
                interactionManager.UpdateInteractState(day, isNoon);
            }
        }
    }

    //--------------------------------------------------------------

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameStart()
    {
        LoadScene("MainField");
    }

    public void GamePause()
    {
        isPause = true;

        //Time.timeScale = 0;
        pauseUI.SetActive(true);

        questUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
    }

    public void GameBack()
    {
        isPause = false;

        //Time.timeScale = 1;
        pauseUI.SetActive(false);

        questUI.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameExit()
    {
        isPause = false;

        //Time.timeScale = 1;
        LoadScene("MainMenu");

        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenOption()
    {
        //옵션 UI 활성화
        //일시정지 UI 비활성화
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
