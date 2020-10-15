using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private checkpoint_master gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<checkpoint_master>();
    }
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
        }
    }
}
