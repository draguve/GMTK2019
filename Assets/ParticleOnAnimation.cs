﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Tools;
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
    public float FlareIntensityMax = 12;
    public MMFeedback onShoot;
    public MMFeedback onSmoke;

    // Start is called before the first frame update
    void Start()
    {
        smokeParticles = FlareSmoke.GetComponent<ParticleSystem>();
        FlareLight = Light.GetComponent<Light2D>();
        if (FlareLight == null)
        {
            Debug.Log("FlareLight null");
        }
        onShoot.Initialization();
        onSmoke.Initialization();
    }

    // Update is called once per frame
    void PlaySmoke()
    {
        FlareSmoke.SetActive(true);
        smokeParticles.Play();
        onSmoke.Play(transform.position);
    }

    void PlayLight()
    {
        Debug.Log("PLaying Flare");
        onShoot.Play(transform.position);
        //FlareLight.intensity = 1;
        DOTween.To(x => FlareLight.intensity = x, 0,FlareIntensityMax, FlareDuration).SetEase(lightCurve);
    }
}
