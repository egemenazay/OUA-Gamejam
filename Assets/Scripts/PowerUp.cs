using StarterAssets;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public int id;
    [SerializeField] public GameObject wall1;
    [SerializeField] public GameObject wall2;
    private healthBar maxHP;
    private float speedamount = 4.5f;
    private int hpamount = 100;
    private int attackamount = 20;
    public GameObject FloatingTextPrefab;
    private bool _collected = false;
    public Transform FloatingTextSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        maxHP =  FindObjectOfType<healthBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collected)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            _collected = true;
            player.ResetHp();
            if (id == 0)
            {
                ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
                thirdPersonController.SetSpeed(speedamount, speedamount * 2);
            }
            else if (id == 1)
            {
                player.hp += hpamount;
                wall2.SetActive(false);
                maxHP.healthSlider.maxValue = 200;
            }
            else if (id == 2)
            {
                Weapon weapon = player.GetComponentInChildren<Weapon>(true);
                weapon.MinDamage += attackamount;
                weapon.MaxDamage += attackamount;
                wall1.SetActive(false);
            }
            player.powerUpsCollected++;

            if (ShowFloatingText()) {
                return;
            }

            DeActivate();
        }
    }

    bool ShowFloatingText() {
        string text = GetFloatingText();
        if (string.IsNullOrWhiteSpace(text)) {
            return false;
        }
        GameObject floatingTextObj = Instantiate(FloatingTextPrefab, FloatingTextSpawnPos.position, Quaternion.identity, FloatingTextSpawnPos.parent);
        floatingTextObj.transform.position = FloatingTextSpawnPos.position;
        FloatingText floatingText = floatingTextObj.GetComponent<FloatingText>();
        floatingText.Offset = Vector3.zero;
        floatingText.RandomizeIntensity = Vector3.zero;
        floatingText._textMeshProUGUI.fontSize /= 2;
        floatingText.SetText(text);
        floatingText.SetColor(Color.white);
        Invoke(nameof(DeActivate), floatingText.DestroyTime);
        return true;
    }

    void DeActivate() {
        gameObject.SetActive(false);
    }

    string GetFloatingText() {
        return id switch {
            0 => "+Speed up",
            1 => "+" + hpamount + " health",
            2 => "+" + attackamount + " damage",
            _ => string.Empty,
        };
    }
}

