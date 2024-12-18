using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonAltar : MonoBehaviour
{
    [SerializeField] private GameObject submittedArrow;
    [SerializeField] private GameObject bow;

    [SerializeField] private GameObject moonGlowParticle;
    private ParticleSystem particle;

    void Start()
    {
        particle = moonGlowParticle.GetComponent<ParticleSystem>();

        submittedArrow.SetActive(false);
        bow.SetActive(false);
    }

    public IEnumerator SubmitArrow()
    {
        if(GameManager.Instance.arrowAmount == 9)
        {
            submittedArrow.SetActive(true);
            GameManager.Instance.arrowAmount = 0;

            yield return new WaitForSeconds(1.5f);
            particle.Play();

            submittedArrow.SetActive(false);
            bow.SetActive(true);
        }


        yield return null;
    }

    //제출 파티클 -> 활
}
