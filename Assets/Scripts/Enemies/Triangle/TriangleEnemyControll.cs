using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemyControll : BaseEnemy
{
    public Rigidbody2D triangleRB;
    [SerializeField] private bool isOnFloor = false;
    [SerializeField] private GameObject bulletPF;
    [SerializeField] private float bulletTorque;
    [SerializeField] private float jumpForce;
    [SerializeField] private int baseHealth;
    [SerializeField] private Collider2D barierCollider;
    [SerializeField] private GameObject wrapper;
    private float spawnXPoint = -100000;
    private bool isInJump;
    public bool isOnFloorGetter {
        get
        {
            return isOnFloor;
        }
    }

    void Start()
    {
        triangleRB = GetComponent<Rigidbody2D>();
        InitHealth(baseHealth);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
            if(!isInJump)
            {
                triangleRB.AddForce(Vector2.down * 60f, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Platform"))
        {
            if(collision.gameObject.transform.position.y < transform.position.y)
            {
                isOnFloor = true;
                isInJump = false;
            }

            if(spawnXPoint == -100000f)
            {
                spawnXPoint = transform.position.x;
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(wrapper);
    }

    public override void Move(Vector3 targetPosition)
    {
        int moveModule = transform.position.x > targetPosition.x ? -1 : 1;

        if (Mathf.Abs(triangleRB.angularVelocity) < maxVelocityGetter && isOnFloor)
        {
            triangleRB.AddTorque(speedSetter * moveModule);
        }
    }

    public void GoToPosition(Vector3 targetPosition)
    {
        int moveModule = transform.position.x > targetPosition.x ? 1 : -1;

        if (Mathf.Abs(triangleRB.angularVelocity) < maxVelocityGetter && isOnFloor)
        {
            triangleRB.AddTorque(speedSetter * moveModule);
        }
    }

    public void Jump()
    {
        if (!isInJump)
        {
            Debug.Log("Jumped");
            triangleRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        isInJump = true;
        isOnFloor = false;
    }

    public string DetectObstacles(int moveModule, float rayLength, LayerMask layerMask)
    {
        RaycastHit2D detectObstacles = Physics2D.Raycast(transform.position + (new Vector3(-0.7f, 0) * moveModule), Vector2.left * moveModule, rayLength, layerMask); ;
        if(detectObstacles.transform == null )
        {
            return "";
        }
        Debug.Log(detectObstacles.transform.gameObject.name);
        Debug.DrawRay(transform.position + (new Vector3(-0.7f, 0) * moveModule), Vector2.left * moveModule  * rayLength, Color.green);
        return detectObstacles.transform.gameObject.tag;
    }

    public void Shoot(Vector3 destination)
    {
        Vector3 direction = destination - gameObject.transform.position;
        Vector3 bulletPosition = transform.position + direction.normalized;

        GameObject bullet = Instantiate(bulletPF, bulletPosition, Quaternion.identity);
        TriangleBulletControl bulletScript = bullet.GetComponent<TriangleBulletControl>();
        bulletScript.shooterSetter = gameObject;
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        Collider2D buletCollider = bullet.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(buletCollider, barierCollider, true);

        bulletRB.AddForce(direction * bulletScript.speedSetter, ForceMode2D.Impulse);
        bulletRB.AddTorque(bulletTorque);
    }
}
