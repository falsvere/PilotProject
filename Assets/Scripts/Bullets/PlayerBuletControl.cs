using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuletControl : BaseBullet
{
    void Start()
    {
        Move(12f, new Vector3(1f,1f,1f));
    }
}
