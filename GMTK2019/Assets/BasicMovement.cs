using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float horizontalMove = 0f;

    public float runSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove=Input.GetAxisRaw("Horizontal")*runSpeed;

    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime,false,false);
        
    }
}
