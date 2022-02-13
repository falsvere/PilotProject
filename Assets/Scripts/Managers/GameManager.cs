using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameData publicGameData;
    void Awake()
    {
        publicGameData = new GameData();
    }
}

public class GameData
{
    private int playerMovementDirection = 0;
    public int _playerMovementDirection
    {
        get
        {
            return playerMovementDirection;
        }
    }

    public void SetMovementDirection(int direction)
    {
        playerMovementDirection = direction;
    }
}
