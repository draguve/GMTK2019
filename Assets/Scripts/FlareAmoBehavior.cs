using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareAmoBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.FlarePickedUp();
            Destroy(gameObject);
        }
    }
}
