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

    private SpriteRenderer circleSprite;

    private Vector2 collisionEnterVelocity;

    //methods
    void Start()
    {
        circleRB = gameObject.GetComponent<Rigidbody2D>();
        InitHealth(100);
        circleSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Move(Vector3 targetPosition)
    {
        if (!isPreparingForAttack && !isInAttack && isOnFloor)
        {
            Vector2 force = new Vector2(targetPosition.normalized.x, 0);

            if (circleRB.velocity.x <= maxVelocityGetter)
            {
                circleRB.AddForce(force * speedSetter, ForceMode2D.Impulse);
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
            isOnFloor = true;
        } else if (collisionGameobject.CompareTag("Player"))
        {
            Vector3 forceDirection = collisionGameobject.transform.position - transform.position;
            collisionGameobject.GetComponent<Rigidbody2D>().AddForce(forceDirection.normalized * 2, ForceMode2D.Impulse);
        }
    }
}
