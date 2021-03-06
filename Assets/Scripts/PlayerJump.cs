﻿using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using SnSMovement.Character;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpVelocity;
    public float rayCastDistance = 2f;
    public int maxWallJumps = 2;
    
    public LayerMask collisionMask;
    [HideInInspector]
    public bool isJumping;
    
    private CharacterMotor characterMotor;
    private Collider2D _collider;
    private int wallJump=0;

    private bool _jumpSound;
    private float _trackTIme=0;
    
    public MMFeedback JumpFeedback;
    
    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        characterMotor = GetComponent<CharacterMotor>();
        _collider = GetComponentInChildren<Collider2D>();
        wallJump = 0;
        _jumpSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
        {
            RaycastHit2D hit = Physics2D.Raycast((transform.position),Vector2.down, rayCastDistance, collisionMask);
            if (hit)
            {
                if (Physics2D.IsTouching(_collider, hit.collider))
                {
                    isJumping = false;
                    wallJump = 0;
                }
            }
        }

        _trackTIme += Time.deltaTime;
        

    }

    
    /// <summary>
    /// this function will be called by the input manager , when the jump button is pressed by the user
    /// </summary>
    public void DoJump()
    {
        if (!isJumping)
        {
            characterMotor.Jump(jumpVelocity);
            isJumping = true;
            if (_trackTIme>1f)
            {
                JumpFeedback.Play(this.transform.position);
                _jumpSound = false;
                _trackTIme = 0;
            }
            
        }

        //Cant move left hence wall there
        if (!characterMotor._canMoveLeft && isJumping && wallJump <= maxWallJumps)
        {
            characterMotor.Jump(Vector2.up + Vector2.right,jumpVelocity);
            wallJump += 1;
            if (_trackTIme>1f)
            {
                JumpFeedback.Play(this.transform.position);
                _jumpSound = false;
                _trackTIme = 0;
            }
        }
        
        if (!characterMotor._canMoveRight && isJumping && wallJump <= maxWallJumps)
        {
            characterMotor.Jump(Vector2.up + Vector2.left,jumpVelocity);
            wallJump += 1;
            if (_trackTIme>1f)
            {
                JumpFeedback.Play(this.transform.position);
                _jumpSound = false;
                _trackTIme = 0;
            }
        }
        
    }
}
