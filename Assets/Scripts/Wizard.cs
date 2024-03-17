using System;
using System.Collections;
using UnityEngine;

public class Wizard : MonoBehaviour, IDamageable
{
    // hp
    public int healthPoints;
    // speed
    public float speed;
    // attackOneDamage
    // attackTwoDamage
    // attack speed
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

    public Animator wizardAnimator;
    public static int attackOne = Animator.StringToHash("attackOne");
    public static int attackThree = Animator.StringToHash("attackThree");
    public static int attackTwo = Animator.StringToHash("attackTwo");
    public static int die = Animator.StringToHash("die");
    public static int inSpellRange = Animator.StringToHash("inSpellRange");
    public GameObject FloatingTextPrefab;
    public static Action Died;


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
        wizardAnimator.SetBool(inSpellRange, inHitRange);
        Move();
    }

    private IEnumerator SpellOneRoutine()
    {
        wizardAnimator.SetTrigger(attackOne);
        yield return new WaitForSeconds(1);
        var projectile = Instantiate(projectileSkillPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
    }

    private IEnumerator SpellTwoRoutine()
    {
        wizardAnimator.SetTrigger(attackTwo);
        yield return new WaitForSeconds(1);
        var areaOfEffect = Instantiate(areaSkillPrefab, player.transform.position, Quaternion.identity);
        Destroy(areaOfEffect, 5);
        yield return new WaitForSeconds(3);
        yield return new WaitForSeconds(2);
    }

    private IEnumerator SpellThreeRoutine()
    {
        wizardAnimator.SetTrigger(attackThree);
        yield return new WaitForSeconds(2);
        var skeletonOne = Instantiate(skeletonPrefab, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), Quaternion.identity);
        var skeletonTwo = Instantiate(skeletonPrefab, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(4);
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
        if (!isAlive) {
            return;
        }
        healthPoints -= damage;
        if (healthPoints <= 0) {
            Died.Invoke();
            Die();
        }

        GameObject floatingTextObj = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        FloatingText floatingText = floatingTextObj.GetComponent<FloatingText>();
        floatingText.SetText(damage.ToString());
    }

    private void Die()
    {
        isAlive = false;
        wizardAnimator.SetTrigger(die);
        Destroy(gameObject, 3);
    }
}
