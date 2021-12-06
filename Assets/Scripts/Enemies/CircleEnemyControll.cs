using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemyControll : BaseEnemy
{
    //states
        private bool isOnFloor = true;
    /*    private bool isInAttack = false;
        private bool isPreparingForAttack = false;*/
    //states end

    private Rigidbody2D circleRB;

    [SerializeField] float jumpForce;


    //methods
    void Start()
    {
        circleRB = gameObject.GetComponent<Rigidbody2D>();
        InitHealth(100);
    }

    public override void Move()
    {
        Vector2 force = new Vector2(moveDirectionSetter, 0);

        if(circleRB.velocity.x < maxVelocityGetter && circleRB.velocity.x > -maxVelocityGetter)
        {
            circleRB.AddForce(force * speedSetter, ForceMode2D.Force);
        }
    }

    public void Jump()
    {
        if (isOnFloor)
        {
            circleRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); ;
        }
    }
}
