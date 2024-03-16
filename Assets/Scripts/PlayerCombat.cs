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

    private ThirdPersonController _thirdPersonController;

    void Start() {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
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
        SetCanDrawWeapon(false);
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

    private void SetCanDrawWeapon(bool canDrawWeapon) {
        _canDrawWeapon = canDrawWeapon;
        _thirdPersonController.SetCanMove(canDrawWeapon);
    }

    #region Animation Events

    public void OnGrabWeapon() {
        WeaponHolder.SetActive(true);
        SheatHolder.SetActive(false);
    }

    public void OnDrawWeaponEnd() {
        SetCanDrawWeapon(true);
    }

    public void OnSheatWeapon() {
        WeaponHolder.SetActive(false);
        SheatHolder.SetActive(true);
    }

    public void OnSheathWeaponEnd() {
        _animator.SetLayerWeight(PlayerAnimationLayers.COMBAT, 0);
        SetCanDrawWeapon(true);
    }

    #endregion

}
