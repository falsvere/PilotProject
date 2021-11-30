using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemyControll : BaseEnemy
{
    
    void Start()
    {
        InitHealth(100);
    }

    public override void Move()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
