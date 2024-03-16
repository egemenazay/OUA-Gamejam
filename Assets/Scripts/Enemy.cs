using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{
    public int healthPoints;
    public float moveSpeed;
    public int attackPoints;
    public int attackSpeed;
    public Player player;
    public int attackRange;
    public bool inAttackRange;
    public int hitRange;
    public bool inHitRange;
    public Vector3 randomPatrolPosition;
    public bool isAlive;
    public float distanceToPlayer;
    public CharacterController characterController;
    public Vector3 initialPosition;

    public Vector3 targetVector;
    public Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        initialPosition = transform.position;
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttack();
        Move();
    }

    private void CheckAttack()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (!inHitRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                inAttackRange = true;
            }
        }
    }

    private void Move()
    {
        if (inAttackRange)
        {
            if (distanceToPlayer > attackRange)
            {
                targetVector = initialPosition - transform.position;
                if (Vector3.Distance(transform.position, initialPosition) < 1)
                {
                    inAttackRange = false;
                }
            }
            else
            {
                targetVector = player.transform.position - transform.position;
                if (distanceToPlayer <= hitRange)
                {
                    inAttackRange = false;
                    inHitRange = true;
                }
            }
            var targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);   
            characterController.SimpleMove(transform.forward * moveSpeed);
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            if (inHitRange)
            {
                if (distanceToPlayer > hitRange)
                {
                    inHitRange = false;
                    inAttackRange = true;
                    yield return null;
                }
                player.TakeDamage(attackPoints);
                yield return new WaitForSeconds(5 / attackSpeed);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        isAlive = false;
        Destroy(gameObject, 3);
    }

    private void Patrol()
    {
        
    }
    // Patrol()
}
