using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICircleEnemy : MonoBehaviour
{
    private CircleEnemyControll cirlceControll;
    private GameObject player;
    private float attackRange = 8f;

    private bool isPlayerDontMoveActive = false;
    private Coroutine playerDontMove;

    private Vector3 playerPreviousPosition;

    //mehods
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPreviousPosition = player.transform.position;
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
        yield return new WaitForSeconds(1);

        Debug.Log("Attack preparation");
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRange )
        {
            Vector3 playerNewPosition = player.transform.position;
            float xDifference = Mathf.Max(playerPreviousPosition.x, playerNewPosition.x) - Mathf.Min(playerPreviousPosition.x, playerNewPosition.x);
            float yDifference = Mathf.Max(playerPreviousPosition.y, playerNewPosition.y) - Mathf.Min(playerPreviousPosition.y, playerNewPosition.y);

            bool isPlayerMoveAlot = xDifference >= 1 || yDifference >= 1;
            playerPreviousPosition = playerNewPosition;

            if (isPlayerMoveAlot) {
                if(isPlayerDontMoveActive)
                {
                    StopCoroutine(playerDontMove);
                }
            } else {
                    playerDontMove = StartCoroutine(OneSecDelayBeforeAttackPrep());
            }

        } else
        {
            if (isPlayerDontMoveActive)
            {
                StopCoroutine(playerDontMove);
            }
        }
    }
}
