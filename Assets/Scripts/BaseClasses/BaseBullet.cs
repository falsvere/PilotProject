using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject shooter;
    [SerializeField] private int damage;

    public float speedSetter
    {
        set
        {
            speed = value;
        }
        get
        {
            return speed;
        }
    }

    public GameObject shooterSetter {
        set
        {
            shooter = value;
        }
        get
        {
            return shooter;
        }
    }

    private readonly Dictionary<char, float> gameBorders = new Dictionary<char, float> {
        {'x', 30f },
        {'y', 15f },
    };

    // methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject != shooter)
        {
            IHaveHealth healthInterface = collision.gameObject.GetComponent<IHaveHealth>();

            if (healthInterface != null && collision.gameObject != shooter)
            {
                healthInterface.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    public void DestroyOutOfBorders()
    {
        if (IsOutOfBorders())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutOfBorders()
    {
        float positionX = Math.Abs(gameObject.transform.position.x);
        float positionY = Math.Abs(gameObject.transform.position.y);

        return (positionX >= gameBorders['x'] || positionY >= gameBorders['y']);
    } 
}
