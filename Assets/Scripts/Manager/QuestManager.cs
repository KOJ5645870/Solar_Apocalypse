using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QuestManager : MonoBehaviour
{
    //����Ʈ ����ü
    public class Quest
    {
        public string itemName;     //������ �̸�
        public string itemTag;     //���� ��� ������Ʈ �±�
        public int requiredAmount;  //��ǥ ������
        public int currentAmount;   //���� ������

        public Quest(string itemName, string itemTag = "empty", int requireAmount = 0)
        {
            this.itemName = itemName;   
            this.itemTag = itemTag;
            this.requiredAmount = requireAmount;
            this.currentAmount = 0;
        }
    }

    public Dictionary<int, List<Quest>> questItemStorage = new Dictionary<int, List<Quest>>();

    private Dictionary<string, string> translationDictionary = new Dictionary<string, string>()
    {
        { "corn", "������" },
        { "pumpkin", "ȣ��" },
        { "tomato", "�丶��" },
        { "deer", "�罿" },
        { "human", "�ΰ�" }
    };

    [SerializeField] private TMP_Text questName;        //����Ʈ �̸�
    [SerializeField] private TMP_Text questDemand;    //����Ʈ �䱸 ����

    private QuestSheet questSheet;

    void Start()
    {
        questSheet = GetComponent<QuestSheet>();

        StartCoroutine(questSheet.Load());
    }

    void Update()
    {
        
    }

    //����Ʈ ����Ʈ
    public void InitQuestItemList()
    {
        questItemStorage.Add(1, new List<Quest> { CreateQuest(1) });

        questItemStorage.Add(2, new List<Quest> { CreateQuest(2) });

        questItemStorage.Add(3, new List<Quest> { CreateQuest(3), CreateQuest(4) });

        questItemStorage.Add(4, new List<Quest> { CreateQuest(5), CreateQuest(6) });

        questItemStorage.Add(5, new List<Quest> { CreateQuest(7), CreateQuest(8), CreateQuest(9) });

        questItemStorage.Add(6, new List<Quest> { CreateQuest(10), CreateQuest(11), CreateQuest(12) });

        questItemStorage.Add(7, new List<Quest> { CreateQuest(13) });

        questItemStorage.Add(8, new List<Quest> { CreateQuest(14) });

        questItemStorage.Add(9, new List<Quest> { CreateQuest(15) });

        questItemStorage.Add(10, new List<Quest> { CreateQuest(16) });
    }

    private Quest CreateQuest(int row)
    {
        string itemName = questSheet.GetData(row, 1);
        string itemTag = questSheet.GetData(row, 2);
        int requiredAmount = int.Parse(questSheet.GetData(row, 3));

        return new Quest(itemName, itemTag, requiredAmount);
    }

    private List<Quest> GetQuestItemList(int day)
    {
        return questItemStorage[day];
    }

    //������ ȹ��
    public void GetRequiredItem(string itemTag)
    {
        int day = GameManager.Instance.day;

        List<Quest> questItemList = GetQuestItemList(day);

        foreach(Quest quest in questItemList) 
        {
            if (quest.itemTag == itemTag)
            {
                if (quest.currentAmount < quest.requiredAmount)
                {
                    quest.currentAmount++;
                    UpdateItemQuestText();
                }
                break;
            }
        }
    }

    public void UpdateItemQuestText()
    {
        int day = GameManager.Instance.day;

        string demand = "";

        foreach(Quest quest in GetQuestItemList(day))
        {
            demand += TranslateItem(quest.itemTag) + " " + quest.currentAmount + "/" + quest.requiredAmount + "�� \n";
        }

        StartCoroutine(SetQuestText(demand, 0f));
    }

    private string TranslateItem(string itemTag)
    {
        if (translationDictionary.ContainsKey(itemTag)) return translationDictionary[itemTag];
        else return "";
    }

    public IEnumerator SetQuestText(string questDemandText, float delay)
    {
        yield return new WaitForSeconds(delay);
        questDemand.text = questDemandText;
    }

    public IEnumerator SetWayPoint(Vector3 location, int delay)
    {
        yield return new WaitForSeconds(delay);

    }

    public bool IsCompleteRequire()
    {
        foreach(Quest quest in GetQuestItemList(GameManager.Instance.day))
        {
            if (quest.requiredAmount != quest.currentAmount) return false;
        }

        return true;
    }

    public void StartAlterQuest()
    {
        StartCoroutine(SetQuestText("�������� ����\n��Ź�� ��������", 0f));
    }
}
