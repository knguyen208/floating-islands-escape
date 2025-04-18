using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    private NavMeshAgent agent;
    public Transform player;

    public float detectionRange = 15f;
    public float shootRange = 10f;
    public float shootingCooldown = 1f;
    private float lastShotTime;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public bool isPlayerInZone = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (patrolPoints.Length > 0)
        {
            currentPatrolIndex = 0;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void Update()
    {
        if (isPlayerInZone && Vector3.Distance(player.position, transform.position) <= detectionRange)
        {
            ChaseAndShootPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            currentPatrolIndex = Random.Range(0, patrolPoints.Length);
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void ChaseAndShootPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer > shootRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
            LookAtPlayer();
            ShootIfReady();
        }
    }

    private void ShootIfReady()
    {
        if (Time.time > lastShotTime + shootingCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Bullet prefab or fire point is not assigned!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.speed = 30f;
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Bullet script!");
        }

        Debug.Log("Enemy shoots the player!");
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        Vector3 leftOffset = Quaternion.Euler(0, -5f, 0) * direction;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(leftOffset.x, 0, leftOffset.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}