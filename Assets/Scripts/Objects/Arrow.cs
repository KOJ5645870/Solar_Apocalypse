using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject glowParticle;
    private ParticleSystem particle;

    void Awake()
    {
        particle = glowParticle.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        particle.Play();
        //파티클 발생
    }
    private void OnDisable()
    {
        particle.Stop();
        //파티클 종료
    }


    public void PickUpArrow()
    {
        GameManager.Instance.arrowAmount++;
        gameObject.SetActive(false);
    }
}
