using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int hp = 100;
    public float speed;
    public int attack;
    // enemy

    public bool isAlive;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Die()
    //PickUp()
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
    }
}
