using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftPiece : MonoBehaviour
{
    public EnemyPatrol[] enemies;

    public void NotifyEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.isPlayerInZone = true;
        }
    }
}