using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICircleEnemy : MonoBehaviour
{
    private CircleEnemyControll cirlceControll;
    private GameObject player;
    private float attackRange = 8f;

    private Coroutine playerDontMoveCoroutine;
    private Coroutine checkPlayerMovesCoroutine;

    //mehods
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        cirlceControll = gameObject.GetComponent<CircleEnemyControll>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.C)) {
            cirlceControll.moveDirectionSetter = -1;
        } else if(Input.GetKey(KeyCode.V)) {
            cirlceControll.moveDirectionSetter = 1;
        } else
        {
            cirlceControll.moveDirectionSetter = 0;
        }

        if(cirlceControll.moveDirectionSetter != 0)
        {
            cirlceControll.Move();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            cirlceControll.Jump();
        }
    }

    private IEnumerator OneSecDelayBeforeAttackPrep()
    {
        yield return new WaitForSeconds(3);

        Debug.Log("Corouutinestrrt");



        Debug.Log("Attack preparation");

        checkPlayerMovesCoroutine = null;
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

    private void FixedUpdate()
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
            }
        }
    }
}
