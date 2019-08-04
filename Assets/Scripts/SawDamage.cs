using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    public float rotationSpeed;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Character")
        {
            Destroy(col.gameObject);
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
    }
}
