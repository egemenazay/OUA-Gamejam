using StarterAssets;
using UnityEngine;

public class PlayerAnimatorParameters {
    public const string DRAW_WEAPON = "DrawWeapon";
    public const string SHEATH_WEAPON = "SheathWeapon";
}

public class PlayerCombat : MonoBehaviour {

    public bool CanAttack => WeaponDrawed && !Attacking;

    public bool WeaponDrawed;
    public bool Attacking;

    public GameObject WeaponHolder;
    public GameObject SheatHolder;

    StarterAssetsInputs _input;
    Animator _animator;


    void Start() {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
    }

    void Update() {
        if (!Attacking && _input.drawWeapon) {
            DrawWeapon();
        }
    }

    void DrawWeapon() {
        _input.drawWeapon = false;

        WeaponDrawed = !WeaponDrawed;

        if (WeaponDrawed) {
            _animator.SetTrigger(PlayerAnimatorParameters.DRAW_WEAPON);
        }
        else {
            _animator.SetTrigger(PlayerAnimatorParameters.SHEATH_WEAPON);
        }
    }

    #region Animation Events

    public void OnGrabWeapon() {
        WeaponHolder.SetActive(true);
        SheatHolder.SetActive(false);
    }

    public void OnSheatWeapon() {
        WeaponHolder.SetActive(false);
        SheatHolder.SetActive(true);
    }

    #endregion

}
