using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public int hp = 100;
    public float speed;
    public int attack;
    // attack speed
    // enemy

    // attack range
    public bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Input()
    //Move()
    //Attack() Raycast tag
    //Die()
    //PickUp()
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
    }
}
