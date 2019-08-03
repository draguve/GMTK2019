using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ParticleOnAnimation : MonoBehaviour
{
    public GameObject FlareSmoke;
    private ParticleSystem smokeParticles;
    public GameObject Light;
    private Light2D FlareLight;
    public AnimationCurve lightCurve;
    public float FlareDuration;

    // Start is called before the first frame update
    void Start()
    {
        smokeParticles = FlareSmoke.GetComponent<ParticleSystem>();
        FlareLight = Light.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void PlaySmoke()
    {
        FlareSmoke.SetActive(true);
        smokeParticles.Play();
        Debug.Log("Smoke played");
    }

    void PlayLight()
    {
        DOTween.To(x => FlareLight.intensity = x, 0, 12, FlareDuration).SetEase(lightCurve);
        //FlareLight.intensity = 1;
        Debug.Log("PLayed Light");
    }
}
