using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Weapon : MonoBehaviour {

    public MinMaxCurve DamageCurve;

    private int _damage => Mathf.CeilToInt(DamageCurve.Evaluate(Random.value));

    private void Reset() {
        DamageCurve.mode = ParticleSystemCurveMode.Curve;
    }

    private void OnTriggerEnter(Collider other) {
        IDamageable damageable = null;
        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Enemy")) {
            damageable = (IDamageable)other.gameObject.GetComponent<Enemy>();
        }
        if (gameObject.CompareTag("Enemy") && other.gameObject.CompareTag("Player")) {
            damageable = (IDamageable)other.gameObject.GetComponent<Player>();
        }
        if (damageable != null) {
            damageable.TakeDamage(_damage);
        }
    }

}
