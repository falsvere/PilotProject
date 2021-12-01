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


    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
