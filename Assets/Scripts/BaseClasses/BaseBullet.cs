using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    public float speed;
    [SerializeField] private int damage;
    public GameObject shooter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHaveHealth healthInterface = collision.gameObject.GetComponent<IHaveHealth>();

        if(healthInterface != null && collision.gameObject != shooter)
        {
            healthInterface.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
