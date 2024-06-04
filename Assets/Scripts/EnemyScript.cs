
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject Winner;
    
    public Transform pos;
    
    public Animator anim;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public int health;
    public int maxHealth = 300;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

   public bool isDead = false;
    private void Start()
    {
        health = maxHealth;
    }
    private void Awake()
    {
        
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        if (isDead == false)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();

            
        }
       
        if (health <= 0)
        {
            isDead = true;
            anim.SetBool("dead", true);
            
            Winner.SetActive(true);

        }
    }
    
    
    
    private void Patrolling()
    {
        if (isDead == false)
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
            anim.SetBool("idle", false);
        }
    }

    private void SearchWalkPoint()
    {
        if (isDead == false)
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        if (isDead == false)
        {
            agent.SetDestination(player.position);
            anim.SetBool("idle", false);
        }
    }

    private void AttackPlayer()
    {
        if (isDead == false)
        {
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            anim.SetBool("idle", true);






            if (!alreadyAttacked)
            {
                Rigidbody rb = Instantiate(projectile, pos.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);





                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void takeDamage(int damageAmount)
    {
        health = maxHealth + damageAmount;
        
            

        
    }

    
}












