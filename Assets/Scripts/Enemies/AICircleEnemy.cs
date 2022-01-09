using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICircleEnemy : MonoBehaviour
{
    private CircleEnemyControll cirlceControll;
    private GameObject player;
    private float attackRange = 8f;
    private SpriteRenderer circleSprite;

    private Coroutine playerDontMoveCoroutine;
    private Coroutine checkPlayerMovesCoroutine;
    private LayerMask rayCastLayer;

    //mehods
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        cirlceControll = gameObject.GetComponent<CircleEnemyControll>();
        circleSprite = gameObject.GetComponent<SpriteRenderer>();

        rayCastLayer = LayerMask.GetMask("Enemies");
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!cirlceControll.isKnocked)
        {
            isPlayerInAttackArea();
            Vector3 moveDirection = player.transform.position - transform.position;
            cirlceControll.Move(player.transform.position);

            if (CastObstacleDetectionRay(moveDirection))
            {
                cirlceControll.Jump();
            }
        }
    }

    private IEnumerator OneSecDelayBeforeAttack()
    {
        cirlceControll.SetAttackPreparationState(true);

        yield return new WaitForSeconds(1);

        Vector3 moveDirection = player.transform.position - transform.position;

        if (!CastObstacleDetectionRay(moveDirection, 7.4f))
        {
            cirlceControll.DistanceAttack(player.transform.position);
        }
        cirlceControll.SetAttackPreparationState(false);

        if (checkPlayerMovesCoroutine != null)
        {
            StopCoroutine(checkPlayerMovesCoroutine);
            checkPlayerMovesCoroutine = null;
        }
        playerDontMoveCoroutine = null;
    }

    private bool CastObstacleDetectionRay(Vector3 moveDirection, float rayLength = 2f)
    {
        Vector2 reyDirection = new Vector2(moveDirection.normalized.x, 0);
        RaycastHit2D detectObstacles = Physics2D.Raycast(transform.position, reyDirection.normalized, rayLength, rayCastLayer);
        Debug.DrawRay(transform.position, reyDirection.normalized * rayLength, Color.green);
        if(detectObstacles.transform != null) 
        {
            return true;
        } else
        {
            return false;
        }
    }

    private IEnumerator checkPlayerMoves()
    {
        while(true)
        {
            Vector3 playerPreviousPosition = player.transform.position;
            bool isPlayerMoveAlot = false;

            yield return new WaitForSeconds(1);

            Vector3 playerNewPosition = player.transform.position;
            float xDifference = Mathf.Max(playerPreviousPosition.x, playerNewPosition.x) - Mathf.Min(playerPreviousPosition.x, playerNewPosition.x);
            float yDifference = Mathf.Max(playerPreviousPosition.y, playerNewPosition.y) - Mathf.Min(playerPreviousPosition.y, playerNewPosition.y);

            isPlayerMoveAlot = xDifference >= 1 || yDifference >= 1;


            if (isPlayerMoveAlot)
            {
                if (playerDontMoveCoroutine != null)
                {
                    StopCoroutine(playerDontMoveCoroutine);
                    playerDontMoveCoroutine = null;
                    cirlceControll.SetAttackPreparationState(false);
                }
            }
            else
            {
                if (playerDontMoveCoroutine == null)
                {
                    playerDontMoveCoroutine = StartCoroutine(OneSecDelayBeforeAttack());
                }
            }

        }
    }

    private void isPlayerInAttackArea()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            if (checkPlayerMovesCoroutine == null)
            {
                checkPlayerMovesCoroutine = StartCoroutine(checkPlayerMoves());
            }
        }
        else
        {
            if (checkPlayerMovesCoroutine != null)
            {
                StopCoroutine(checkPlayerMovesCoroutine);
                checkPlayerMovesCoroutine = null;
            }

            if (playerDontMoveCoroutine != null)
            {
                StopCoroutine(playerDontMoveCoroutine);
                playerDontMoveCoroutine = null;
                cirlceControll.SetAttackPreparationState(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameobject = collision.gameObject;

        if (collisionGameobject.CompareTag("Player"))
        {
            if (checkPlayerMovesCoroutine != null)
            {
                StopCoroutine(checkPlayerMovesCoroutine);
                checkPlayerMovesCoroutine = null;
            }

            if (playerDontMoveCoroutine != null)
            {
                StopCoroutine(playerDontMoveCoroutine);
                playerDontMoveCoroutine = null;
            }
        }
    }
}
