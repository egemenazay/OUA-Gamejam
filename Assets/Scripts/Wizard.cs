using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    // hp
    public int healthPoints;
    // speed
    public float speed;
    // attackOneDamage
    // attackTwoDamage
    // attack speed
    public float attackSpeed;
    // player
    public Player player;
    // attack range
    public int hitRange;
    public bool inHitRange;
    // skeleton
    public GameObject skeletonPrefab;
    public GameObject projectileSkillPrefab;
    public GameObject areaSkillPrefab;
    public bool isAlive;
    public CharacterController characterController;
    public float distanceToPlayer;

    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        StartCoroutine(AttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        inHitRange = distanceToPlayer < 20;
    }

    private IEnumerator SpellOneRoutine()
    {
        yield return new WaitForSeconds(3);
        // animasyon geçişi
        var projectile = Instantiate(projectileSkillPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = transform.forward;
    }

    private IEnumerator SpellTwoRoutine()
    {
        yield return new WaitForSeconds(5);
        // animasyon geçişi
        var areaOfEffect = Instantiate(areaSkillPrefab, player.transform.position, Quaternion.identity);
    }

    private IEnumerator SpellThreeRoutine()
    {
        yield return new WaitForSeconds(4);
        // animasyon geçişi
        var skeletonOne = Instantiate(skeletonPrefab, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), Quaternion.identity);
        var skeletonTwo = Instantiate(skeletonPrefab, new Vector3(transform.position.x - 5, transform.position.y, transform.position.z), Quaternion.identity);
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (isAlive && inHitRange)
            {
                yield return SpellOneRoutine();
                yield return SpellTwoRoutine();
                yield return SpellThreeRoutine();
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Move()
    {
        if (!inHitRange)
        {
            var targetVector = player.transform.position - transform.position;
            var targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);
            characterController.SimpleMove(transform.forward * speed);
            if (distanceToPlayer <= hitRange)
            {
                inHitRange = true;
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
        // olum animasyonu
        Destroy(gameObject, 3);
    }
}
