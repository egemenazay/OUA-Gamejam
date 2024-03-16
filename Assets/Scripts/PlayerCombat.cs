using StarterAssets;
using UnityEngine;

public class PlayerAnimationLayers {
    public const int COMBAT = 1;
}

public class PlayerAnimatorParameters {
    public const string DRAW_WEAPON = "DrawWeapon";
    public const string SHEATH_WEAPON = "SheathWeapon";
}

public class PlayerCombat : MonoBehaviour {

    public bool CanAttack => WeaponDrawed && _canDrawWeapon && !Attacking;

    public bool WeaponDrawed;
    public bool Attacking;

    public GameObject WeaponHolder;
    public GameObject SheatHolder;

    StarterAssetsInputs _input;
    Animator _animator;

    bool _canDrawWeapon = true;


    void Start() {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
    }

    void Update() {
        if (!Attacking && _canDrawWeapon && _input.drawWeapon) {
            DrawWeapon();
        }

        if (!CanAttack) {
            return;
        }
    }

    void DrawWeapon() {
        _canDrawWeapon = false;
        _input.drawWeapon = false;

        WeaponDrawed = !WeaponDrawed;

        if (WeaponDrawed) {
            _animator.SetLayerWeight(PlayerAnimationLayers.COMBAT, 1);
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

    public void OnDrawWeaponEnd() {
        _canDrawWeapon = true;
    }

    public void OnSheatWeapon() {
        WeaponHolder.SetActive(false);
        SheatHolder.SetActive(true);
    }

    public void OnSheathWeaponEnd() {
        _animator.SetLayerWeight(PlayerAnimationLayers.COMBAT, 0);
        _canDrawWeapon = true;
    }

    #endregion

}
