using System;
using System.Threading.Tasks;
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
                    PickUpObject();
                }
            }
        });

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if ((Input.GetKeyUp(KeyCode.K)))
            {
                ThrowObject();
            }
        });
    }

    private void PickUpObject()
    {
        animator.Play(variables.PickUpHash);
        canPickUp = false;
        characterMovement.isAttackingWithWeapon = true;
        characterMovement.isRunningWithWeapon = true;
        characterMovement.isRunning = false;
        animator.SetBool(variables.IdleWithWeapon, true);
    }

    private async void ThrowObject()
    {
        animator.Play(variables.ThrowHash);
        await Task.Delay(200);
        ThrowItem?.Invoke();
        canPickUp = true;
        characterMovement.isAttacking = true;
        animator.SetBool(variables.IdleWithWeapon, false);
        animator.SetBool(variables.WalkingWithWeapon, false);
        characterMovement.isAttackingWithWeapon = false;
        characterMovement.isRunningWithWeapon = false;
        characterMovement.isRunning = true;
    }
}
