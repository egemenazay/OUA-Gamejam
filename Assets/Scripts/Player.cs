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
        isAlive = true;
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

    public void ResetHp() {
        if (hp < 100 && isAlive) {
            hp = 100;
        }
    }
}
