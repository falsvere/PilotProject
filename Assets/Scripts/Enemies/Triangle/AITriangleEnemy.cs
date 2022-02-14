using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITriangleEnemy : MonoBehaviour
{
    private TriangleEnemyControll triangleEnemyControll;
    [SerializeField] private GameObject player;
    [SerializeField] private int attackDistance;
    [SerializeField] private int retreatDistance;
    [SerializeField] private float timeGapBetweenShoots;
    [SerializeField] float avoidJumpDelay;
    [SerializeField] float forceInJump;
    [SerializeField] float sideForceInAviodPlayer;
    [SerializeField] private float moveRadius;
    private LayerMask rayCastLayers;
    private float previousShootTime;
    private float previousAvoidJumpTime = 0f;
    private GameData gameData;
    private Vector2 spawnCoordinates;

    void Start()
    {
        spawnCoordinates = transform.position;
        gameData = GameManager.publicGameData;
        player = GameObject.FindGameObjectWithTag("Player");
        triangleEnemyControll = GetComponent<TriangleEnemyControll>();
        previousShootTime = Time.time;
        previousAvoidJumpTime = Time.time;
        rayCastLayers = LayerMask.GetMask("Enviroment", "Enemies");
    }

    private void Update()
    {
        AvoidPlayer();
    }

    private void FixedUpdate()
    {
        CheckRange(player.transform.position);
    }

    private void CheckRange(Vector2 targetPosition)
    {
        float distance = Vector2.Distance(transform.position, targetPosition);
        float rayForwardLength = 1f;
        int moveModule = transform.position.x > player.transform.position.x ? -1 : 1;
        string obstacleForwardTag;

        if (distance <= attackDistance)
        {
            ShootWhilePlayerInRange();
        }

        //ToDo доделать механику отклонения треугольника от точки спавна

        float playerDistanceFromSpawnPoint = Vector2.Distance(targetPosition, spawnCoordinates);

        if (distance > retreatDistance + 1.2f && playerDistanceFromSpawnPoint < moveRadius + attackDistance && (moveModule == gameData._playerMovementDirection || gameData._playerMovementDirection == 0))
        {
            if(triangleEnemyControll.isOnFloorGetter)
            {
                triangleEnemyControll.ChasePlayer(player.transform.position);
            }
            return;
        }

        obstacleForwardTag = DetectObstacles(moveModule, rayForwardLength, rayCastLayers);

        if (obstacleForwardTag.Contains("Border"))
        {
            return;
        }

        if (obstacleForwardTag.Contains("Enemy"))
        {
            triangleEnemyControll.Jump();
            triangleEnemyControll.triangleRB.AddForce(Vector2.left * moveModule * forceInJump, ForceMode2D.Impulse);
        }

        if (distance <= retreatDistance && triangleEnemyControll.isOnFloorGetter)
        {
            triangleEnemyControll.Move(player.transform.position);
            return;
        } 

        if(playerDistanceFromSpawnPoint >= moveRadius + attackDistance && triangleEnemyControll.isOnFloorGetter && Vector2.Distance(transform.position, spawnCoordinates) > 1.7f)
        {
            Debug.Log("asds");
            triangleEnemyControll.ChasePlayer(spawnCoordinates);
        }
    }

    private void ShootWhilePlayerInRange()
    {
        if (Time.time - previousShootTime >= timeGapBetweenShoots)
        {
            triangleEnemyControll.Shoot(player.transform.position);
            previousShootTime = Time.time;
        }
    }

    public string DetectObstacles(int moveModule, float rayLength, LayerMask layerMask)
    {
        RaycastHit2D detectObstacles = Physics2D.Raycast(transform.position + (new Vector3(-0.7f, 0) * moveModule), Vector2.left * moveModule, rayLength, layerMask); ;
        if (detectObstacles.transform == null)
        {
            return "";
        }
        Debug.Log(detectObstacles.transform.gameObject.name);
        Debug.DrawRay(transform.position + (new Vector3(-0.7f, 0) * moveModule), Vector2.left * moveModule * rayLength, Color.green);
        return detectObstacles.transform.gameObject.tag;
    }

    private void AvoidPlayer()
    {
        float rayBackwardLength = 3.8f;
        int moveModule = transform.position.x > player.transform.position.x ? -1 : 1;
        string obstacleBackwardTag = DetectObstacles(-moveModule, rayBackwardLength, LayerMask.GetMask("Player"));

        if (obstacleBackwardTag == "Player" && Time.time - previousAvoidJumpTime >= avoidJumpDelay && gameData._playerMovementDirection != 0f)
        {
            triangleEnemyControll.triangleRB.velocity *= 0f;
            triangleEnemyControll.Jump();
            triangleEnemyControll.triangleRB.AddForce(Vector2.right * sideForceInAviodPlayer * moveModule, ForceMode2D.Impulse);
            previousAvoidJumpTime = Time.time;
        }
    }
}
