using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PickObjectsTest : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Animator animator;
    readonly GlobalStringVariables variables = new();
    public static event Action ThrowItem;
    [SerializeField] public bool canPickUp;
    void Start()
    {
        playerCollider.OnTriggerStay2DAsObservable().Subscribe(_ =>
        {
            if (!canPickUp) { return; }
            if (Input.GetKey(KeyCode.E) && _.CompareTag("PickObject"))
            {
                if (_.TryGetComponent(out WeaponScriptTest weaponScript))
                {
                    weaponScript.PickUp();
                    canPickUp = false;
                    characterMovement.isAttackingWithWeapon = true;
                    characterMovement.isRunningWithWeapon = true;
                    characterMovement.isRunning = false;
                    animator.SetBool(variables.IdleWithWeapon, true);
                }
            }
        });

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if ((Input.GetKeyUp(KeyCode.K)))
            {               
                ThrowItem?.Invoke();
                canPickUp = true;
                characterMovement.isAttacking = true;
                animator.SetBool(variables.IdleWithWeapon, false);
                characterMovement.isAttackingWithWeapon = false;
                characterMovement.isRunningWithWeapon = false;
                characterMovement.isRunning = true;
            }
        });
    }
}
