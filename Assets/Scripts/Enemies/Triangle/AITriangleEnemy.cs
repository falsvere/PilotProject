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
        if(IsInRange(player.transform.position, attackDistance) && Time.time - previousShootTime >= timeGapBetweenShoots)
        {
            triangleEnemyControll.Shoot(player.transform.position);
            previousShootTime = Time.time;
        }
/*        if(triangleEnemyControll.isOnFloorGetter)
        {
            triangleEnemyControll.Move(player.transform.position);
        }*/
    }

    private bool IsInRange(Vector2 targetPosition, int range)
    {
        return Vector2.Distance(transform.position, targetPosition) <= range;
    }
}
