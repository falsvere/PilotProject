using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour, IHaveHealth, ICanShoot
{
    private Rigidbody2D playerRB;

    [SerializeField] private GameObject bulletPF;

    [SerializeField]
    private float speedForce = 12f;
    [SerializeField]
    private float maxVelocityInJumpDivider = 2f;
    [SerializeField]
    private float speedForceInJumpDivider;
    [SerializeField]
    private float jumpForce = 60f;
    [SerializeField]
    private float maxVelocity = 20f;
    [SerializeField]
    private float maxAngularVelocity = 20f;
    [SerializeField]
    private float minAngularVelocity = 20f;

    private int health;

    private bool isOnFloor = false;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        InitHealth(100);
    }

    public void Shoot(Vector3 destination)
    {
        Vector3 direction = destination - gameObject.transform.position;
        Vector3 bulletPosition = transform.position + direction.normalized;
        Quaternion bulletRotation = Quaternion.LookRotation(direction, Vector3.forward) * Quaternion.Euler(90, 0, 0);

        GameObject bullet = Instantiate(bulletPF, bulletPosition, bulletRotation);
        PlayerBuletControl bulletScript = bullet.GetComponent<PlayerBuletControl>();
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        bulletRB.AddForce(direction * bulletScript.speedSetter, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }

    public void InitHealth(int healthPoints)
    {
        health = healthPoints;
    }

    public void MoveFunction(float horizontalInput)
    {
        float maxVelocityLocal = isOnFloor ? maxVelocity : maxVelocity / maxVelocityInJumpDivider;
        float speedForceLocal = isOnFloor ? speedForce : speedForce / speedForceInJumpDivider;


        // drop velocity if player change move direction in air to увеличить отзывчивость
        if(!isOnFloor)
        {
            if(playerRB.velocity.x < 0 && horizontalInput > 0 || playerRB.velocity.x > 0 && horizontalInput < 0)
            {
                playerRB.velocity *= 0.9f;
            }
        }

        if (playerRB.velocity.x < maxVelocityLocal && playerRB.velocity.x > -maxVelocityLocal)
        {
            playerRB.AddForce(horizontalInput * Vector2.right * speedForceLocal, ForceMode2D.Impulse);
            //ограничиваем сверху и снизу скорость вращения чтобы смотрелось нормально с скоростью передвижения
            playerRB.angularVelocity = Mathf.Clamp(Mathf.Abs(playerRB.angularVelocity), minAngularVelocity, maxAngularVelocity) * -horizontalInput;
        }
    }

    public void Jump()
    {
        if(isOnFloor)
        { 
            playerRB.velocity /= maxVelocityInJumpDivider;
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void DropVelocityOnKeysUp()
    {
        if (isOnFloor) {
            playerRB.velocity *= 0f;
            playerRB.angularVelocity *= 0f;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Floor")
        {
            isOnFloor = true;
            playerRB.velocity *= 0f;
            playerRB.angularVelocity *= 0f;
        }

        if (collision.collider.CompareTag("Platform"))
        {
            isOnFloor = true;
            playerRB.velocity *= 0f;
            playerRB.angularVelocity *= 0f;
        }

/*        if (collision.collider.name == "Border")
        {
            playerRB.velocity *= 0f;
        }*/
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Floor" || collision.collider.CompareTag("Platform"))
        {
            isOnFloor = false;
        }
    }
}
