using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleParticle : MonoBehaviour
{
    private Rigidbody2D particleRB;
    [SerializeField] Collider2D[] neibs = new Collider2D[2];
    [SerializeField] Collider2D floorCollider;

    private void Start()
    {
        particleRB = GetComponent<Rigidbody2D>();
    }

    public bool isNeibsOnFloor()
    {
        foreach (Collider2D item in neibs)
        {
            item.IsTouching(floorCollider);
        }

        return true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        Debug.Log("dasd");
        if (collision.gameObject.CompareTag("Floor") && !isNeibsOnFloor())
        {
            Debug.Log("dasd");
            //particleRB.AddForce(Vector2.down * 100f);
            Debug.DrawRay(transform.position, Vector2.down, Color.red);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("dasd");
        if (collision.gameObject.CompareTag("Floor") && !isNeibsOnFloor())
        {
            Debug.Log("dasd");
            //particleRB.AddForce(Vector2.down * 100f);
            Debug.DrawRay(transform.position, Vector2.down, Color.red);
        }
    }
}
