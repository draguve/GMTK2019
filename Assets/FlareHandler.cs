﻿using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class FlareHandler : MonoBehaviour
{
    public GameObject Flare;
    public bool FlareAvailable = true;
    public MMFeedback flareShotStartFeedback;
    public MMFeedback flareCannotBeShotFeedback;

    private Animator _flareAnimator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _flareAnimator = Flare.GetComponent<Animator>();
        flareShotStartFeedback.Initialization();
        flareCannotBeShotFeedback.Initialization();
    }

    // Update is called once per frame
    public void FlareAnim()
    {
        if (FlareAvailable)
        {
            _flareAnimator.SetTrigger("ShootFlare");
            flareShotStartFeedback.Play(transform.position);
        }
        else
        {
            flareCannotBeShotFeedback.Play(transform.position);
        }
    }
}
