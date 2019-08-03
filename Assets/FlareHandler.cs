using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareHandler : MonoBehaviour
{
    public GameObject Flare;

    private Animator _flareAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _flareAnimator = Flare.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void FlareAnim()
    {
        _flareAnimator.SetTrigger("ShootFlare");
    }
}
