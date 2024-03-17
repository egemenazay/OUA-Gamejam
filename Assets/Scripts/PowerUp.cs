using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public int id;
    private int speedamount = 5;
    private int hpamount = 100;
    private int attackamount = 20;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (id == 0)
            {
                player.speed += speedamount;
                gameObject.SetActive(false);
            }
            else if (id == 1)
            {
                player.hp += hpamount;
                gameObject.SetActive(false);
            }
            else if (id == 2)
            {
                player.attack += attackamount;
                gameObject.SetActive(false);
            }
        }
    }
}

