using StarterAssets;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public int id;
    [SerializeField] public GameObject wall1;
    [SerializeField] public GameObject wall2;
    private int speedamount = 2;
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
                wall2.SetActive(false);
            }
            else if (id == 2)
            {
                Weapon weapon = player.GetComponentInChildren<Weapon>(true);
                weapon.MinDamage += attackamount;
                weapon.MaxDamage += attackamount;
                wall1.SetActive(false);
            }

            player.ResetHp();
            gameObject.SetActive(false);
            player.powerUpsCollected++;
        }
    }
}

