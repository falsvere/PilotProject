using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IHaveHealth, ICanAttack, IMovable
{
    private int health;

    public void InitHealth(int healthPoints)
    {
        health = healthPoints;
        Debug.Log(health);
    }

    public abstract void Move();


    public virtual void Attack()
    {

    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
