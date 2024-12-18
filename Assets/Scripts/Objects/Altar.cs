using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] private List<GameObject> submitObjects = new List<GameObject>();

    public bool hasReveal = false;  //제물 퀘스트(계시) 여부

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

    public IEnumerator Submit()     //제물 제출
    {
        if(hasReveal)
        {
            if(GameManager.Instance.questManager.IsCompleteRequire()) 
            {
                //제출한 오브젝트 일별 활성화->빛 방출 후 비활성화
                //GameObject SubmitObject = SubmitObjects[GameManager.Instance.day];
                //SubmitObject.SetActive(true);

                GameObject submitObject = submitObjects[GameManager.Instance.day - 1];
                submitObject.SetActive(true);
                particle.Play();

                yield return new WaitForSeconds(glowingDuration);

                SkyController skyController = GameManager.Instance.GetComponent<SkyController>();
                StartCoroutine(skyController.SetNight());

                //페이드 아웃되는 동안까지 보여지기 위함
                yield return new WaitForSeconds(GameManager.Instance.fadeDuration);
                submitObject.SetActive(false);

                hasReveal = false;
                StartCoroutine(GameManager.Instance.questManager.SetQuestText("집으로 가서 휴식을 취하세요", GameManager.Instance.fadeDuration));
                
                //웨이포인트
            }
            else
            {
                Debug.Log("제물이 부족합니다");
            }
        }
        else
        {
            hasReveal = true;
            GameManager.Instance.questManager.UpdateItemQuestText();
        }
    }
}
