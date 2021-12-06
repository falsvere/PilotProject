using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IHaveHealth, IMovable
{
    private int health;

    [SerializeField] private float speed;
    [SerializeField] private float maxVelocity;
    private int moveDirection = 0;

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

    public abstract void Move();

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

}
