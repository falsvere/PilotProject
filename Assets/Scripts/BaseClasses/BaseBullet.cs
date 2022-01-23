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
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.CompareTag("Barier"))
        {
            Debug.Log('a');
            Destroy(gameObject);
            collisionGameObject.GetComponent<IBarierBehavour>().ChangeColorOnHit();
        } else if(collisionGameObject != shooter && !collisionGameObject.CompareTag("Bullet"))
        {
            IHaveHealth healthInterface = collisionGameObject.GetComponent<IHaveHealth>();

            if (healthInterface != null && collisionGameObject != shooter)
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
