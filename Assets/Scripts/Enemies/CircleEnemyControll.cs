using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemyControll : BaseEnemy
{
    //states
        private bool isOnFloor = true;
        private bool isInAttack = false;
        private bool isPreparingForAttack = false;
    //states end

    private Rigidbody2D circleRB;

    [SerializeField] float jumpForce;
    [SerializeField] float distanceAttackForce;

    private SpriteRenderer circleSprite;

    //methods
    void Start()
    {
        circleRB = gameObject.GetComponent<Rigidbody2D>();
        InitHealth(100);
        circleSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Move()
    {
        if (!isPreparingForAttack && !isInAttack)
        {
            Vector2 force = new Vector2(moveDirectionSetter, 0);

            if (circleRB.velocity.x < maxVelocityGetter && circleRB.velocity.x > -maxVelocityGetter)
            {
                circleRB.AddForce(force * speedSetter, ForceMode2D.Force);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            collision.gameObject.SetActive(false);
            Debug.Log(circleRB.velocity);
        }
    }
}
