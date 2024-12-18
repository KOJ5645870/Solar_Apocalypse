using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] private List<GameObject> submitObjects = new List<GameObject>();

    public bool hasReveal = false;  //���� ����Ʈ(���) ����

    [SerializeField] private Material[] glowingMaterial;
    [SerializeField] private float glowingDuration = 3.0f;

    [SerializeField] private GameObject glowParticle;
    private ParticleSystem particle;

    void Start()
    {
        particle = glowParticle.GetComponent<ParticleSystem>();
    }


    void Update()
    {
        
    }

    public IEnumerator Submit()     //���� ����
    {
        if(hasReveal)
        {
            if(GameManager.Instance.questManager.IsCompleteRequire()) 
            {
                //������ ������Ʈ �Ϻ� Ȱ��ȭ->�� ���� �� ��Ȱ��ȭ
                //GameObject SubmitObject = SubmitObjects[GameManager.Instance.day];
                //SubmitObject.SetActive(true);

                GameObject submitObject = submitObjects[GameManager.Instance.day - 1];
                submitObject.SetActive(true);
                particle.Play();

                yield return new WaitForSeconds(glowingDuration);

                SkyController skyController = GameManager.Instance.GetComponent<SkyController>();
                StartCoroutine(skyController.SetNight());

                //���̵� �ƿ��Ǵ� ���ȱ��� �������� ����
                yield return new WaitForSeconds(GameManager.Instance.fadeDuration);
                submitObject.SetActive(false);

                hasReveal = false;
                StartCoroutine(GameManager.Instance.questManager.SetQuestText("������ ���� �޽��� ���ϼ���", GameManager.Instance.fadeDuration));
                
                //��������Ʈ
            }
            else
            {
                Debug.Log("������ �����մϴ�");
            }
        }
        else
        {
            hasReveal = true;
            GameManager.Instance.questManager.UpdateItemQuestText();
        }
    }
}
