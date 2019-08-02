using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using SnSMovement.Character;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public MMCooldown jumpCooldown;
    public float jumpVelocity;
    
    private CharacterMotor characterMotor;
    
    // Start is called before the first frame update
    void Start()
    {
        jumpCooldown.Initialization();
        characterMotor = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpCooldown.Update();
    }

    
    /// <summary>
    /// this function will be called by the input manager , when the jump button is pressed by the user
    /// </summary>
    public void DoJump()
    {
        if (jumpCooldown.Ready())
        {
            jumpCooldown.Start();
            characterMotor.Jump(jumpVelocity);
        }
    }
}
