using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collided");
        if (col.gameObject.name == "Character")
        {
            Debug.Log("Kill PLayer");
            Destroy(col.gameObject);
        }
    }
}
