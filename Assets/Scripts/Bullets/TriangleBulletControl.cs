using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBulletControl : BaseBullet
{
    void Update()
    {
        DestroyOutOfBorders();
    }
}
