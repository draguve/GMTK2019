using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnAnimation : MonoBehaviour
{
    public GameObject FlareSmoke;
    private ParticleSystem smokeParticles;
    // Start is called before the first frame update
    void Start()
    {
        smokeParticles = FlareSmoke.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void PlaySmoke()
    {
        FlareSmoke.SetActive(true);
        smokeParticles.Play();
        Debug.Log("Smoke played");
    }
}
