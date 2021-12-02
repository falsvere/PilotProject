using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly Dictionary<char, float> gameBorders = new Dictionary<char, float> {
        {'x', 30f },
        {'y', 15f },
    }; 

    public  Dictionary<char, float> gameBordersPublic
    {
        get
        {
            return gameBorders;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
