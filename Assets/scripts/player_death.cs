using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_death : MonoBehaviour
{
    [SerializeField] Transform checkpoint1;

    void OnCollisionEnter2D(Collision2D col1)
    {
        if (col1.transform.CompareTag("Player"))
            col1.transform.position = checkpoint1.position;
    }
   
}
