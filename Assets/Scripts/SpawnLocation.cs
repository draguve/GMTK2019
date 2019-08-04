using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.spawnLocation = transform.position;
    }
}
