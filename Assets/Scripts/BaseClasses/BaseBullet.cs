using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float force;
    public virtual void Move( float force, Vector3 destination)
    {
        Debug.Log("Shoot");
    }
}
