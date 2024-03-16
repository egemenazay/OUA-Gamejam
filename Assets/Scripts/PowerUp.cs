using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Player player;
    public int id;
    public int speedamount;
    public int hpamount;
    public int attackamount;
    public GameObject model1;
    public GameObject model2;
    public GameObject model3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (id == 0)
        {
            model1.SetActive(true);
        }
        else if (id == 1)
        {
            model2.SetActive(true);
        }
        else if (id == 2)
        {
            model3.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (id == 0)
            {
                player.speed += speedamount;
            }
            else if (id == 1)
            {
                player.hp += hpamount;
            }
            else if (id == 2)
            {
                player.attack += attackamount;
            }
        }
    }
}

