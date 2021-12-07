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

    private Vector3 playerPreviousPosition = new Vector3(0,0,-999);

    //mehods
    void Start()
    {
        Debug.Log(playerDontMove);
        playerDontMove = StartCoroutine(OneSecDelayBeforeAttackPrep());
        Debug.Log(playerDontMove);
        StopCoroutine(playerDontMove);
        Debug.Log(playerDontMove);


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
        yield return new WaitForSeconds(5);
        /*        Debug.Log("Corouutinestrrt");



                Debug.Log("Attack preparation");

                isPlayerDontMoveActive = false;*/
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

/*        if (distanceToPlayer <= attackRange )
        {
            bool isPlayerMoveAlot = true;
            if (playerPreviousPosition.z == -999)
            {
                playerPreviousPosition = player.transform.position;
            } else
            {
                Vector3 playerNewPosition = player.transform.position;
                float xDifference = Mathf.Max(playerPreviousPosition.x, playerNewPosition.x) - Mathf.Min(playerPreviousPosition.x, playerNewPosition.x);
                float yDifference = Mathf.Max(playerPreviousPosition.y, playerNewPosition.y) - Mathf.Min(playerPreviousPosition.y, playerNewPosition.y);

                isPlayerMoveAlot = xDifference >= 1 || yDifference >= 1;
                playerPreviousPosition = playerNewPosition;
            }
            Debug.Log(isPlayerMoveAlot);

            if (isPlayerMoveAlot) {
                if(isPlayerDontMoveActive)
                {
                    StopCoroutine(playerDontMove);
                    Debug.Log(playerDontMove);
                    isPlayerDontMoveActive = false;
                }
            } else {
                if (!isPlayerDontMoveActive)
                {
                    playerDontMove = StartCoroutine(OneSecDelayBeforeAttackPrep());
                    isPlayerDontMoveActive = true;
                }
            }

        } else
        {
            playerPreviousPosition.z = -999;
            if (isPlayerDontMoveActive)
            {
                StopCoroutine(playerDontMove);
                isPlayerDontMoveActive = false;
            }
        }*/
    }
}
