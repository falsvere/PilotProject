using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParticles : MonoBehaviour
{
    private Vector3 startPosition;


    private DefaultPlatform _parent;
    public DefaultPlatform parent
    {
        get
        {
            return _parent;
        }

        set
        {
            _parent = value;
        }
    }

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
            if(!_parent.isCircleEnemyInteractionActive)
            {
                StartCoroutine(_parent.CircleEnemyInteraction());
                _parent.platformBoxCollider.enabled = false;
            }
        }
    }
}
