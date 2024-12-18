using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class QuestSheet : MonoBehaviour
{
    string sheetData;
    const string googleSheetURL = "https://docs.google.com/spreadsheets/d/13gtLgJOCaWOX75SroZ1zCL7ln2I8A4AAlKmvb4XgCLo/export?format=tsv&range=A1:D";

    QuestManager questManager;

    private void Start()
    {
        questManager = GetComponent<QuestManager>();
    }

    public IEnumerator Load()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(googleSheetURL))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                sheetData = www.downloadHandler.text;
                if (questManager.questItemStorage.Count == 0) questManager.InitQuestItemList();
                questManager.StartAlterQuest();
            }
        }

        Debug.Log(sheetData);
    }

    public String GetData(int row, int column)
    {
        string[] rows = sheetData.Split('\n');
        string[] columns = rows[row].Split('\t');

        return columns[column];
    }
}
