using UnityEngine;

public class Weapon : MonoBehaviour {

    public int Damage = 5;

    private void OnTriggerEnter(Collider other) {
        IDamageable damageable = null;
        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Enemy")) {
            damageable = (IDamageable)other.gameObject.GetComponent<Enemy>();
        }
        if (gameObject.CompareTag("Enemy") && other.gameObject.CompareTag("Player")) {
            damageable = (IDamageable)other.gameObject.GetComponent<Player>();
        }
        if (damageable != null) {
            damageable.TakeDamage(Damage);
        }
    }

}
