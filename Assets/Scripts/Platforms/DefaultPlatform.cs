using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlatform : BasePlatform
{
    [SerializeField]
    private int timeBeforeDestroy = 6;
    [SerializeField]
    private int timeBeforeAppear = 6;

    private void Start()
    {
        SetGameManager();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RunPlatformStateCoroutine(collision, timeBeforeDestroy, timeBeforeAppear);
    }

}
