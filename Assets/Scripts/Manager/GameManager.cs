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
            //�ν��Ͻ� �Ҵ�
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
        //�ν��Ͻ� ���� �� ���� ����
        else if (gmInstance != this)
        {
            Destroy(gameObject);
        }
        //�� ��ȯ �� �ν��Ͻ��� �ı� X
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
            //�Ͻ����� ����
            if(isPause)
            {
                GameBack();
            }
            //�Ͻ�����
            else
            {
                GamePause();
            }
        }
    }
    
    //�ڵ� ��ü ȭ�� ����
    private void InitFadeImageSize()
    {
        RectTransform rectTransform = fadeImage.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0, 0);    //���� �ϴ� �𼭸�
        rectTransform.anchorMax = new Vector2(1, 1);    //���� ��� �𼭸�

        rectTransform.offsetMin = Vector2.zero; //���� �ϴ� ������
        rectTransform.offsetMax = Vector2.zero; //���� ��� ������

        fadeImage.color = new Color(0, 0, 0, 0);
    }

    //���̵� �ƿ�
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

    //���̵� ��
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
        //��Ȱ��ȭ ������Ʈ ���� ��ȸ
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
        //�ɼ� UI Ȱ��ȭ
        //�Ͻ����� UI ��Ȱ��ȭ
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
