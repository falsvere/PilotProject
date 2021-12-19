using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParticles : MonoBehaviour
{
    private Vector3 startPosition;

    public DefaultPlatform parent;

    void Start()
    {
        startPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(!parent.isCircleEnemyInteractionActive)
            {
                StartCoroutine(parent.CircleEnemyInteraction());
            }
        }
    }
}
