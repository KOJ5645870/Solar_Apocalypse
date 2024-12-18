using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowing : MonoBehaviour
{
    private ParticleSystem glowParticle;

    private void Start()
    {
        glowParticle = GetComponent<ParticleSystem>();
        glowParticle.Stop();
    }

    public void StartGlowing(float glowingDuration)
    {
        glowParticle.Play();
        Invoke("StopParticle", glowingDuration);
    }

    private void StopParticle()
    {
        glowParticle.Stop();
    }
}
