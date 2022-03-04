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
    private Rigidbody2D playerRB;

    void Start()
    {
        spawnCoordinates = transform.position;
        gameData = GameManager.publicGameData;
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        triangleEnemyControll = GetComponent<TriangleEnemyControll>();
        previousShootTime = Time.time;
        previousAvoidJumpTime = Time.time;
        rayCastLayers = LayerMask.GetMask("Enviroment", "Enemies");
    }

    private void Update()
    {
        JumpOnPlayerGetsClose();
    }

    private void FixedUpdate()
    {
        CheckRange(player.transform.position);
    }

    private string ObstaclesDetection(int jumpMoveModule)
    {
        float rayForwardLength = 1f;
        RaycastHit2D obstacleForwardTag = DetectObstacles(jumpMoveModule, rayForwardLength, rayCastLayers);

        if(obstacleForwardTag.transform == null)
        {
            return "none";
            Debug.Log("null");
        }

        if (obstacleForwardTag.transform.gameObject.tag.Contains("Border"))
        {
            return "border";
        }

        //ToDo deal with problem when triangle is very close to enemy and cant jump(now there is a problem when triangle is close to border)
        if (obstacleForwardTag.transform.gameObject.tag.Contains("Enemy"))
        {
            if(obstacleForwardTag.distance < 1f)
            {
                return "enemyClose";
            } else
            {
                return "enemy";
            }
        }

        return "none";
    }

    private void jumpOverObstacle(int jumpMoveModule)
    {
        triangleEnemyControll.triangleRB.velocity *= 0f;
        triangleEnemyControll.triangleRB.angularVelocity *= 0f;
        triangleEnemyControll.Jump();
        triangleEnemyControll.triangleRB.AddForce(Vector2.left * jumpMoveModule * forceInJump, ForceMode2D.Impulse);
    }

    private void MovementsBehaivor(Vector2 targetPosition, float distance, int moveModule)
    {
        float playerDistanceFromSpawnPoint = Vector2.Distance(targetPosition, spawnCoordinates);


        if (distance <= retreatDistance && triangleEnemyControll.isOnFloorGetter)
        {
            triangleEnemyControll.Move(player.transform.position);

        } else if (distance > retreatDistance + 1.2f && playerDistanceFromSpawnPoint < moveRadius + attackDistance && (moveModule == gameData._playerMovementDirection || gameData._playerMovementDirection == 0))
        {
           triangleEnemyControll.GoToPosition(player.transform.position);
        } else if (playerDistanceFromSpawnPoint >= moveRadius + attackDistance && triangleEnemyControll.isOnFloorGetter && Vector2.Distance(transform.position, spawnCoordinates) > 1.7f)
        {
           triangleEnemyControll.GoToPosition(spawnCoordinates);
        }
    }

    private void CheckRange(Vector2 targetPosition)
    {
        float distance = Vector2.Distance(transform.position, targetPosition);
        int moveModule = transform.position.x > player.transform.position.x ? -1 : 1;
        int jumpMoveModule = (int)(triangleEnemyControll.triangleRB.angularVelocity / Mathf.Abs(triangleEnemyControll.triangleRB.angularVelocity));

        if (distance <= attackDistance)
        {
            Shoot();
        }

        string obstacleDetectionResults = ObstaclesDetection(jumpMoveModule);

        Debug.Log(obstacleDetectionResults);

        if (obstacleDetectionResults == "border")
        {
            return;
        } else if (obstacleDetectionResults == "enemyClose") {
            MovementsBehaivor(targetPosition, distance, -moveModule);
        } else
        {
            MovementsBehaivor(targetPosition, distance, moveModule);
        }
    }

    private void Shoot()
    {
        if (Time.time - previousShootTime >= timeGapBetweenShoots)
        {
            triangleEnemyControll.Shoot(player.transform.position);
            previousShootTime = Time.time;
        }
    }

    public RaycastHit2D DetectObstacles(int moveModule, float rayLength, LayerMask layerMask)
    {
        RaycastHit2D detectObstacles = Physics2D.Raycast(transform.position + (new Vector3(-0.7f, 0) * moveModule), Vector2.left * moveModule, rayLength, layerMask);
        Debug.DrawRay(transform.position + (new Vector3(-0.7f, 0) * moveModule), Vector2.left * moveModule, Color.red);
        return detectObstacles;
    }

    private void JumpOnPlayerGetsClose()
    {
        float playerVelocityDivider = 3.8f;
        int moveModule = transform.position.x > player.transform.position.x ? -1 : 1;
        RaycastHit2D detectPlayer = Physics2D.Raycast(transform.position, Vector2.right * moveModule, 10f, LayerMask.GetMask("Player"));
 
        // distance to player in which triangle should jump calculates from player velocity, the slower player is - the shorter distanse gets 
        bool isPlayerClose = detectPlayer.distance < Mathf.Abs(playerRB.velocity.x/playerVelocityDivider) && detectPlayer.collider != null;
        bool triangleCanJump = Time.time - previousAvoidJumpTime >= avoidJumpDelay;
        bool isPlayerMoves = gameData._playerMovementDirection != 0f;

        if (isPlayerClose && triangleCanJump && isPlayerMoves)
        {
            triangleEnemyControll.triangleRB.velocity *= 0f;
            triangleEnemyControll.triangleRB.angularVelocity *= 0f;
            triangleEnemyControll.Jump();
            triangleEnemyControll.triangleRB.AddForce(Vector2.right * sideForceInAviodPlayer * moveModule, ForceMode2D.Impulse);
            previousAvoidJumpTime = Time.time;
        }
    }
}
