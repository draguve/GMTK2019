using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    public float rotationSpeed;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.respawnPlayer();
        }
    }

    private void Update()
    {
        transform.Rotate (Vector3.forward * -rotationSpeed * Time.deltaTime);
//        transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
    }
}
