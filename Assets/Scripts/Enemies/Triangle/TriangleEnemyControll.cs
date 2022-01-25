using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemyControll : BaseEnemy
{
    Rigidbody2D triangleRB;
    private bool isOnFloor = false;
    [SerializeField] GameObject bulletPF;
    [SerializeField] float bulletTorque;
    [SerializeField] int baseHealth;
    [SerializeField] Collider2D barierCollider;
    private LayerMask rayCastLayers;
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
        rayCastLayers = LayerMask.GetMask("Enviroment", "Enemies");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
            triangleRB.AddForce(Vector2.down * 60f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }

    public override void Move(Vector3 targetPosition)
    {
        float rayLength = 2f;
        int moveModule = transform.position.x > targetPosition.x ? -1 : 1;
        string obstacleTag = DetectObstacles(moveModule, rayLength, rayCastLayers);

        Debug.Log(obstacleTag);

        if (obstacleTag.Contains("Border"))
        {
            return;
        }

        if (triangleRB.angularVelocity < maxVelocityGetter )
        {
            triangleRB.AddTorque(speedSetter * moveModule);
        }
    }

    public string DetectObstacles(int moveModule, float rayLength, LayerMask layerMask )
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
