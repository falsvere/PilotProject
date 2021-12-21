using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemyControll : BaseEnemy
{
    //states
        private bool isOnFloor = false;
        private bool isInAttack = false;
        private bool isPreparingForAttack = false;
    //states end

    private Rigidbody2D circleRB;

    [SerializeField] float jumpForce;
    [SerializeField] float distanceAttackForce;
    [SerializeField] float bounceForceOnPlayerCollision;

    private SpriteRenderer circleSprite;


    //methods
    void Start()
    {
        circleRB = gameObject.GetComponent<Rigidbody2D>();
        InitHealth(100);
        circleSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Move(Vector3 targetPosition)
    {
        if (!isPreparingForAttack && !isInAttack)
        {
            float maxVelocity = maxVelocityGetter;

            Vector2 force = new Vector2(targetPosition.normalized.x, 0);

            if (circleRB.velocity.x <= maxVelocity && circleRB.velocity.x >= -maxVelocity)
            {
                if(!isOnFloor)
                {
                    maxVelocity /= 3;
                }
                circleRB.AddForce(force.normalized * maxVelocity, ForceMode2D.Impulse);
            }
        }
    }

    public void Jump()
    {
        if (isOnFloor && !isPreparingForAttack && !isInAttack)
        {
            circleRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); ;
        }
    }

    public void SetAttackPreparationState(bool isInAttackPrep)
    {
        if (isInAttackPrep)
        {
            circleRB.velocity *= 0f;
            circleRB.angularVelocity *= 0f;
            isPreparingForAttack = true;
            circleSprite.color = new Color32(255, 255, 255, 255);
        } else
        {
            isPreparingForAttack = false;
            circleSprite.color = new Color32(238, 6, 6, 255);
        }
    }

    public void DistanceAttack(Vector3 destination)
    {
        SetAttackPreparationState(false);

        isInAttack = true;

        Vector3 direction = destination - gameObject.transform.position;

        circleRB.AddForce(direction.normalized * distanceAttackForce, ForceMode2D.Impulse);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameobject = collision.gameObject;

        if (collisionGameobject.CompareTag("Floor"))
        {
            isInAttack = false;
            isOnFloor = true;
        } else if (collisionGameobject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collisionGameobject.GetComponent<Rigidbody2D>();
            Vector3 forceDirection = collisionGameobject.transform.position - transform.position;
            circleRB.velocity *= 0f;
            circleRB.AddForce(-forceDirection.normalized * bounceForceOnPlayerCollision, ForceMode2D.Impulse);
            playerRB.AddForce(forceDirection.normalized * bounceForceOnPlayerCollision, ForceMode2D.Impulse);
        }
    }
}
