using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public EnemyPatrol[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.isPlayerInZone = true;
            }
            Debug.Log("Player entered the trigger zone!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.isPlayerInZone = false;
            }
            Debug.Log("Player exited the trigger zone!");
        }
    }
}