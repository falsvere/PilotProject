using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlatform : BasePlatform
{

    public DefaultPlatform script;

    private void Start()
    {
        SetGameManager();
        SetChildrensScriptComponent();
        script = GetComponent<DefaultPlatform>();
        SetParentScriptForChildrens();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RunPlatformStateCoroutine(collision, timeBeforeDestroy, timeBeforeAppear);
    }

    public override void SetParentScriptForChildrens()
    {
        foreach (PlatformParticles childScript in childsScript)
        {
            childScript.parent = script;
        }
    }

}
