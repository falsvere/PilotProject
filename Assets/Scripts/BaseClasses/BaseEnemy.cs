using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IHaveHealth, IMovable
{
    private int health;

    [SerializeField] private float speed;
    [SerializeField] private float maxVelocity;
    [SerializeField] private int clashDamage;
    [SerializeField] private int attackDamage;
    private int moveDirection = 0;
    private PlayerControll playerControll;

    public float maxVelocityGetter
    {
        get
        {
            return maxVelocity;
        }
    }
    public float speedSetter
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    public int moveDirectionSetter
    {
        get
        {
            return moveDirection;
        }

        set
        {
            moveDirection = value;
        }
    }

    // methods
    public void InitHealth(int healthPoints)
    {
        health = healthPoints;
    }

    public abstract void Move(Vector3 targetPosition);

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log(gameObject.name + "dead");
        }else
        {
            Debug.Log(health);
        }
    }

    public void  DealAttack(GameObject playerGameobject)
    {
        if(playerControll == null)
        {
            playerControll = playerGameobject.GetComponent<PlayerControll>();
        }

        playerControll.TakeDamage(attackDamage);
    }

    public void Deal—lash(GameObject playerGameobject)
    {
        if (playerControll == null)
        {
            playerControll = playerGameobject.GetComponent<PlayerControll>();
        }

        playerControll.TakeDamage(clashDamage);
    }

}
