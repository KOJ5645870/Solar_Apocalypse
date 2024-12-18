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
        //게임 종료

        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenOption()
    {
        //옵션 UI 활성화
        //일시정지 UI 비활성화
    }
}
