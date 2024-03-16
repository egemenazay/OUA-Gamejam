using StarterAssets;
using UnityEngine;

public class PlayerAnimatorParameters {
    public const string ATTACK = "Attack";
    public const string MOVE = "Move";
    public const string DRAW_WEAPON = "DrawWeapon";
    public const string SHEATH_WEAPON = "SheathWeapon";
}

public class PlayerCombat : MonoBehaviour {

    public bool WeaponDrawed;
    public bool Attacking;

    public GameObject WeaponHolder;
    public GameObject SheatHolder;

    StarterAssetsInputs _input;
    Animator _animator;
    ThirdPersonController _thirdPersonController;

    float _timePassed;
    float _clipLength;
    float _clipSpeed;

    void Start() {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    void Update() {
        if (!Attacking && _input.drawWeapon) {
            DrawWeapon();
        }

        if (_input.attack && WeaponDrawed && !Attacking) {
            Attack();
        }

        if (Attacking) {
            _timePassed += Time.deltaTime;
            AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(1);

            if (clipInfo?.Length > 0) {
                _clipLength = clipInfo[0].clip.length;
                _clipSpeed = _animator.GetCurrentAnimatorStateInfo(1).speed;

                //if (_timePassed >= _clipLength / _clipSpeed && _input.attack) {
                //    Attack();
                //}

                if (_timePassed >= _clipLength / _clipSpeed) {
                    _animator.applyRootMotion = false;
                    Attacking = false;
                    _animator.SetTrigger(PlayerAnimatorParameters.MOVE);
                    _thirdPersonController.canMove = true;
                }
            }
        }

        _input.attack = false;
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

    void Attack() {
        _input.attack = false;
        Attacking = true;
        _animator.applyRootMotion = true;
        _timePassed = 0f;
        _animator.SetTrigger(PlayerAnimatorParameters.ATTACK);
        _thirdPersonController.canMove = false;
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
