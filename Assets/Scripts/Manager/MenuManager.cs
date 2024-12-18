using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameStart()
    {
        LoadScene("MainField");
    }

    public void GameExit()
    {
        //���� ����

        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenOption()
    {
        //�ɼ� UI Ȱ��ȭ
        //�Ͻ����� UI ��Ȱ��ȭ
    }
}