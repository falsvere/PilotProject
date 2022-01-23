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
    private float previousShootTime;

    void Start()
    {
        triangleEnemyControll = GetComponent<TriangleEnemyControll>();
        previousShootTime = Time.time;
    }

    private void FixedUpdate()
    {
        CheckRange(player.transform.position);
    }

    private void CheckRange(Vector2 targetPosition)
    {
        float distance = Vector2.Distance(transform.position, targetPosition);

        if(distance <= attackDistance)
        {
            ShootWhilePlayerInRange();
        }

        if(distance <= retreatDistance && triangleEnemyControll.isOnFloorGetter)
        {
            triangleEnemyControll.Move(player.transform.position);
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
}
