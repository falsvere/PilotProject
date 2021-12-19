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

    //mehods
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        cirlceControll = gameObject.GetComponent<CircleEnemyControll>();
        circleSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            cirlceControll.Jump();
        }
    }

    private void FixedUpdate()
    {
        isPlayerInAttackArea();
        PusruePlayer();
    }

    private void PusruePlayer()
    {
        Vector3 moveDirection = player.transform.position - transform.position;
        cirlceControll.Move(moveDirection);
    }

    private IEnumerator OneSecDelayBeforeAttackPrep()
    {
        cirlceControll.SetAttackPreparationState(true);

        yield return new WaitForSeconds(3);

        cirlceControll.DistanceAttack(player.transform.position);

        if (checkPlayerMovesCoroutine != null)
        {
            StopCoroutine(checkPlayerMovesCoroutine);
            checkPlayerMovesCoroutine = null;
        }
        playerDontMoveCoroutine = null;
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

            Debug.Log(isPlayerMoveAlot);

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
                    playerDontMoveCoroutine = StartCoroutine(OneSecDelayBeforeAttackPrep());
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
}
