using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuletControl : BaseBullet
{
    private void Update()
    {
        DestroyOutOfBorders();
    }
}
