using StarterAssets;
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
                ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
                thirdPersonController.IncreaseSpeed(speedamount, speedamount * 1.5f);
            }
            else if (id == 1)
            {
                player.hp += hpamount;
            }
            else if (id == 2)
            {
                Weapon weapon = player.GetComponentInChildren<Weapon>(true);
                weapon.DamageCurve.constantMin += attackamount;
                weapon.DamageCurve.constantMax += attackamount;
            }

            player.ResetHp();
            gameObject.SetActive(false);
        }
    }
}

