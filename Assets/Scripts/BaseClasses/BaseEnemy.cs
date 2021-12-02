using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IHaveHealth, IMovable
{
    private int health;

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
