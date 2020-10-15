using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint_master : MonoBehaviour
{
    private static checkpoint_master instance;
    public Vector2 lastCheckPointPos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
       
    }
}
