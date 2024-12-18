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
        //��ƼŬ �߻�
    }
    private void OnDisable()
    {
        particle.Stop();
        //��ƼŬ ����
    }


    public void PickUpArrow()
    {
        GameManager.Instance.arrowAmount++;
        gameObject.SetActive(false);
    }
}
